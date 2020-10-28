using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using IotHubHost.Data;
using IotHubHost.Hubs;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace IotHubHost
{
    internal class EventScheduler : IHostedService, IDisposable
    {
        private readonly IHubContext<DevicesHub> _hubContext;

        private readonly string receiveWorkMethodName = "ReceiveWork";

        public EventScheduler(
            IHubContext<DevicesHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void Dispose()
        {
            
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var locationsList = new List<string> { "1", "2", "3", "4" };

            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var location in locationsList)
                {
                    var connectionId = LocationMappings.GetConnectionId(location);

                    if (!string.IsNullOrWhiteSpace(connectionId))
                        await _hubContext.Clients.Client(connectionId).SendAsync(receiveWorkMethodName);
                    else
                        await _hubContext.Clients.Groups(LocationsAreaMapper.GetLocationName(location)).SendAsync(receiveWorkMethodName);

                    await Task.Delay(30000);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
