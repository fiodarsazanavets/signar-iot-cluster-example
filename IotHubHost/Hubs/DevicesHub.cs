using Microsoft.AspNetCore.SignalR;
using IotHubHost.Data;
using System;
using System.Threading.Tasks;

namespace IotHubHost.Hubs
{
    public class DevicesHub : Hub
    {
        public async Task ReceiveDeviceConnected(string deviceId, string areaName, string locationNumber)
        {
            UserMappings.AddDeviceConnected(deviceId, Context.ConnectionId);           
            LocationMappings.MapDeviceToLocation(locationNumber, Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, areaName);
        }

        public async Task BroadcastWorkStatus(string areaName, bool working)
        {
            await Clients.Groups(areaName).SendAsync("ReceiveWorkStatus", working);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            UserMappings.RemoveDeviceConnected(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
