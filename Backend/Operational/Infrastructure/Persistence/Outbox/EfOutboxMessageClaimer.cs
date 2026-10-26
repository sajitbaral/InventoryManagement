using Microsoft.EntityFrameworkCore;
using Operational.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Transactions;


namespace Operational.Infrastructure.Persistence.Outbox
{
    public sealed class EfOutboxMessageClaimer : IOutboxMessageClaimer
    {
        private readonly OperationalDbContext _dbContext;
        public EfOutboxMessageClaimer(OperationalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ClaimedOutboxMessage?> ClaimNextAsync(CancellationToken cancellationToken)
        {
            var connection = _dbContext.Database.GetDbConnection();
            var openedHere = connection.State != ConnectionState.Open;



            try
            {
                if (openedHere)
                {
                    await connection.OpenAsync(cancellationToken);
                }

                await using var transaction = await connection.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted, cancellationToken);     /* System.Data.IsolationLevel.RepeatableRead is used beacause read_committed_snapshot(RCSI) is on in our operationaldb, which makes the use of READPAST hint useless. SQL Server restricts the use of READPAST under RCSI */
                try
                {
                    await using var command = connection.CreateCommand();
                    command.Transaction = transaction;

                    command.CommandText = """

                        ;WITH NextMessage AS
                        (
                            SELECT TOP(1) *
                            FROM dbo.OutboxMessages
                                WITH (UPDLOCK, READPAST, READCOMMITTEDLOCK)
                            WHERE
                                (
                                    Status = 0
                                    AND NextAttemptAtUtc <= @now
                                )
                                OR
                                (
                                    Status = 1
                                    AND LockedUntilUtc <= @now
                                )
                            ORDER BY CreatedAtUtc, Id

                        )

                        UPDATE NextMessage
                        SET
                            Status = 1,
                            LockToken = @lockToken,
                            LockedUntilUtc = @lockedUntilUtc
                        OUTPUT
                            INSERTED.Id,
                            INSERTED.Type,
                            INSERTED.Payload,
                            INSERTED.LockToken;

                        """;

                    var now = DateTime.UtcNow;
                    var lockToken = Guid.NewGuid();
                    var lockedUntilUtc = now.AddMinutes(2);

                    AddParameter(command, "@now", now);
                    AddParameter(command, "@lockToken", lockToken);
                    AddParameter(command, "@lockedUntilUtc", lockedUntilUtc);

                    ClaimedOutboxMessage? claimedMessage = null;

                    await using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                    {
                        if (await reader.ReadAsync(cancellationToken))
                        {
                            claimedMessage = new ClaimedOutboxMessage(
                                reader.GetGuid(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetGuid(3)
                                );
                        }
                    }
                    await transaction.CommitAsync(cancellationToken);
                    return claimedMessage;

                }
                catch
                {
                    try
                    {
                        await transaction.RollbackAsync(CancellationToken.None);
                    }
                    catch
                    {
                        // Preserve the original exception.
                    }

                    throw;
                }

            }

            finally
            {
                if (openedHere && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
        }


        private static void AddParameter(
            DbCommand command,
            string name,
            object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }
    }
}
