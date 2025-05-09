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
    }
}