using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Operational.Application.Abstractions.Messaging;
using Operational.Application.DTOs.Inventory;
using Operational.Infrastructure.Clients;
using System.Text.Json;
using Operational.Application.Interfaces;

namespace Operational.Infrastructure.Persistence.Outbox
{
    public sealed class OutboxProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public OutboxProcessor(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())  //CreateScope() creates a temporary scope.
                {
                    var claimer = scope.ServiceProvider.GetRequiredService<IOutboxMessageClaimer>();  //ServiceProvider comes from CreateScope(). The .NET dependency injection system provides it. ServiceProvider gives us access to registered services. GetRequiredService<T>() asks for a specific service.
                    
                    var inventoryClient = scope.ServiceProvider.GetRequiredService<IInventoryClient>();

                    var updater = scope.ServiceProvider.GetRequiredService<IOutboxMessageUpdater>();

                    var message = await claimer.ClaimNextAsync(cancellationToken);

                    if (message != null)
                    {
                        try
                        {
                            if (message.Type == OutboxMessageTypes.ReserveStockForSale)
                            {
                                var saleMessage = JsonSerializer.Deserialize<ReserveStockForSaleMessage>(
                                    message.Payload,
                                    new JsonSerializerOptions(JsonSerializerDefaults.Web))
                                    ?? throw new InvalidOperationException("Invalid stock reservation message.");

                                var request = new ReserveStockForSaleRequest
                                {
                                    SaleId = saleMessage.saleId,
                                    OperationId = message.Id,
                                    Items = saleMessage.Items.Select(item => new ReserveStockItemRequest
                                    {
                                        ProductId = item.ProductId,
                                        Quantity = item.Quantity
                                    }).ToList()
                                };

                                await inventoryClient.ReserveStockForSale(request, cancellationToken);

                                await updater.MarkAsProcessedAsync(
                                    message.Id,
                                    message.LockToken,
                                    cancellationToken);
                            }
                            else
                            {
                                throw new InvalidOperationException(
                                    $"Unsupported outbox message type: {message.Type}");
                            }
                        }
                        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                        {
                            throw;
                        }
                        catch (Exception ex)
                        {
                            var retryDelaySeconds = Math.Min(
                                5 * Math.Pow(2, message.AttemptCount), 300);

                            var nextAttemptAtUtc = DateTime.UtcNow.AddSeconds(retryDelaySeconds);

                            await updater.MarkAsFailedAsync(
                                message.Id,
                                message.LockToken,
                                ex.Message,
                                nextAttemptAtUtc,
                                5,
                                cancellationToken);
                        }
                    }
                }    

                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }
    }
}
