namespace KarpysDev.Runup
{
    using System;
    using KarpysUtils;
    using TMPro;
    using UnityEngine;
    using Utils;

    public class UserPosition : MonoBehaviour
    {
        [SerializeField] private float m_UserPositionRate = 1f;
        [SerializeField] private TMP_Text m_UserPosition = null;
        [SerializeField] private TMP_Text m_RefreshTime = null;
        [SerializeField] private TMP_Text m_DistanceTravelledBetweenTwoRefresh = null;
        [SerializeField] private TMP_Text m_LivePosition = null;

        private Loop m_AskUserPositionLoop = null;

        private float m_LastLatitude = 0;
        private float m_LastLongitude = 0;
        private void Start()
        {
            m_AskUserPositionLoop = new Loop(m_UserPositionRate, AskPosition);
        }
        
        private void Update()
        {
            m_AskUserPositionLoop?.Update();
            m_RefreshTime.text = "Refresh Time : " + m_AskUserPositionLoop?.LoopTimer;
            
            if (Input.location.status == LocationServiceStatus.Running)
            {
                m_LivePosition.text = "Live Position : " + String.Concat("Latitude : ", Input.location.lastData.latitude
                    , " | ", "Longitude ", Input.location.lastData.longitude);
            }
            else
            {
                m_UserPosition.text = "Wait for initialization";
            }
        }

        private void AskPosition()
        {
            if (Input.location.status != LocationServiceStatus.Running)
            {
                return;
            }

            float currentLatitude = Input.location.lastData.latitude;
            float currentLongitude = Input.location.lastData.longitude;
            
            string location = String.Concat("Latitude : ", currentLatitude.ToString("F2"), " | ", "Longitude ",
                currentLongitude.ToString("F2"));
            m_UserPosition.text = "Position : " + location;

            if (m_LastLatitude == 0 && m_LastLongitude == 0)
            {
                m_DistanceTravelledBetweenTwoRefresh.text = "0";
            }
            else
            {
                float distanceTravelledBetween = DistanceUtils.HaversineDistance(m_LastLatitude, m_LastLongitude, currentLatitude, currentLongitude);
                m_DistanceTravelledBetweenTwoRefresh.text = distanceTravelledBetween.ToString("F2") + "m";
            }
            
            m_LastLatitude = Input.location.lastData.latitude;
            m_LastLongitude = Input.location.lastData.longitude;
        }
    }
}
