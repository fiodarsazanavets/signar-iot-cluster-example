using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace IotDeviceApp
{
    class Program
    {
        private static bool holdOffWork = false;
        private static readonly int timeoutSeconds = 60;

        static async Task Main()
        {
            Console.WriteLine("Please provide device identifier.");
            var identifier = Console.ReadLine();

            Console.WriteLine("Please provide the area name for the device.");
            var areaName = Console.ReadLine();

            Console.WriteLine("Please provide the location identifier for the device to be positioned at.");
            var gateNumber = Console.ReadLine();

            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:57100/devicesHub")
                .Build();

            connection.On("ReceiveWork", DoWork);
            connection.On<bool>("ReceiveWorkStatus", (working) => holdOffWork = working);

            await connection.StartAsync();
            await connection.InvokeAsync("ReceiveDeviceConnected", identifier, areaName, gateNumber);

            async Task DoWork()
            {
                var receiveTime = DateTimeOffset.Now;

                while (holdOffWork)
                {
                    Console.WriteLine("Other device is doing work. Waiting...");
                    if (DateTimeOffset.Now.AddSeconds(-timeoutSeconds) > receiveTime)
                        holdOffWork = false;

                    await Task.Delay(1000);
                }

                await connection.InvokeAsync("BroadcastWorkStatus", areaName, true);
                Console.WriteLine("Work Started");
                await Task.Delay(60000);
                Console.WriteLine("Work Finished");
                await connection.InvokeAsync("BroadcastWorkStatus", areaName, false);
            }
        }
    }
}
