using System.Collections.Generic;


namespace IotHubHost.Data
{
    internal static class LocationsAreaMapper
    {
        private static readonly Dictionary<string, string> locationsInAreas = new Dictionary<string, string>
        {
            {"1", "North Wing"},
            {"2", "North Wing"},
            {"3", "South Wing"},
            {"4", "South Wing"}
        };

        public static string GetLocationName(string locationNumber)
        {
            if (locationsInAreas.ContainsKey(locationNumber))
                return locationsInAreas[locationNumber];

            return string.Empty;
        }
    }
}
