using System.Collections.Generic;

namespace IotHubHost.Data
{
    public static class LocationMappings
    {
        private static readonly Dictionary<string, string> locationMappings = new Dictionary<string, string>();

        public static void MapDeviceToLocation(string locationNumber, string connectionId)
        {
            locationMappings[locationNumber] = connectionId;
        }

        public static string GetConnectionId(string locationNumber)
        {
            if (!locationMappings.ContainsKey(locationNumber))
                return string.Empty;

            return locationMappings[locationNumber];
        }
    }
}
