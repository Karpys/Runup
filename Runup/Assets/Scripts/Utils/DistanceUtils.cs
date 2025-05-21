namespace KarpysDev.Runup.Utils
{
    using UnityEngine;

    public static class DistanceUtils
    {
        public static float HaversineDistance(float lat1, float lon1, float lat2, float lon2)
        {
            float R = 6371000f;
            float dLat = Mathf.Deg2Rad * (lat2 - lat1);
            float dLon = Mathf.Deg2Rad * (lon2 - lon1);

            float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                      Mathf.Cos(Mathf.Deg2Rad * lat1) * Mathf.Cos(Mathf.Deg2Rad * lat2) *
                      Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);

            float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
            return R * c;
        }
        
        public static float GetDistanceInMeters(LocationInfo a, LocationInfo b)
        {
            var R = 6371000f;
            var dLat = Mathf.Deg2Rad * (b.latitude - a.latitude);
            var dLon = Mathf.Deg2Rad * (b.longitude - a.longitude);

            var lat1 = Mathf.Deg2Rad * a.latitude;
            var lat2 = Mathf.Deg2Rad * b.latitude;

            var x = dLat / 2;
            var y = dLon / 2;

            var h = Mathf.Sin(x) * Mathf.Sin(x) + Mathf.Cos(lat1) * Mathf.Cos(lat2) * Mathf.Sin(y) * Mathf.Sin(y);
            var c = 2 * Mathf.Atan2(Mathf.Sqrt(h), Mathf.Sqrt(1 - h));

            return R * c;
        }
    }
}