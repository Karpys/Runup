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

        public Action<float> OnDistanceTravelled = null;
        private Loop m_AskUserPositionLoop = null;

        private LocationInfo m_PreviousLocationInfo;
        private LocationInfo m_CurrentLocationInfo;
        
        private void Start()
        {
            m_AskUserPositionLoop = new Loop(m_UserPositionRate, AskPosition);
        }
        
        private void Update()
        {
            m_AskUserPositionLoop?.Update();
            
            if(m_RefreshTime)
                m_RefreshTime.text = "Refresh Time : " + m_AskUserPositionLoop?.LoopTimer;
            
            if (Input.location.status == LocationServiceStatus.Running)
            {
                if(m_LivePosition)
                    m_LivePosition.text = "Live Position : " + String.Concat("Latitude : ", Input.location.lastData.latitude
                    , " | ", "Longitude ", Input.location.lastData.longitude);
            }
            else
            {
                if(m_UserPosition)
                    m_UserPosition.text = "Wait for initialization";
            }
        }

        private void AskPosition()
        {
            if (Input.location.status != LocationServiceStatus.Running)
            {
                return;
            }else if (m_PreviousLocationInfo.longitude == 0)
            {
                m_PreviousLocationInfo = Input.location.lastData;
            }
            
            m_CurrentLocationInfo = Input.location.lastData;

            float distanceTravelledBetween = DistanceUtils.GetDistanceInMeters(m_PreviousLocationInfo,m_CurrentLocationInfo);


            if (CanSaveDistance(distanceTravelledBetween))
            {
                if(m_DistanceTravelledBetweenTwoRefresh)
                    m_DistanceTravelledBetweenTwoRefresh.text = distanceTravelledBetween.ToString("F2") + "m";
                OnDistanceTravelled?.Invoke(distanceTravelledBetween);
                m_PreviousLocationInfo = Input.location.lastData;
            }
        }

        private bool CanSaveDistance(float distanceTravelled)
        {
            if(distanceTravelled < 3)
                return false;

            return true;
        }
    }
}
