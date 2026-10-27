using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Operational.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

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
                
                    var message = await claimer.ClaimNextAsync(cancellationToken);

                    if (message != null)
                    {
                        if(message.Type == OutboxMessageTypes.ReserveStockForSale)
                        {

                        }
                    }
                }    

                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }
    }
}
