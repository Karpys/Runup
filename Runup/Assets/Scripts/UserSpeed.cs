namespace KarpysDev.Runup
{
    using System;
    using TMPro;
    using UnityEngine;

    public class UserSpeed : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_TotalDistanceTravelledText = null;
        [SerializeField] private TMP_Text m_TimeText = null;
        [SerializeField] private TMP_Text m_SpeedText = null;
        [SerializeField] private TMP_Text m_DistanceTravelledBetween = null;
        [SerializeField] private UserPosition m_UserPosition = null;

        private float m_StartTime = 0;
        private float m_TotalDistance = 0;
        private bool m_InRecord = false;

        public void Start()
        {
            m_UserPosition.OnDistanceTravelled += UpdateDistance;
        }
        
        public void StartRecord()
        {
            m_InRecord = true;
            m_StartTime = Time.time;
            m_TotalDistance = 0;
        }

        public void StopRecord()
        {
            m_InRecord = false;
        }

        private void UpdateDistance(float distance)
        {
            if(!m_InRecord)
                return;

            m_DistanceTravelledBetween.text = "Distance Between :" + distance + "m";
            m_TotalDistance += distance;

            m_TotalDistanceTravelledText.text = "Distance : " + m_TotalDistance + "m";
            m_TimeText.text = "Time : " + (Time.time - m_StartTime) + " sec";

            float meterPerSec = m_TotalDistance / (Time.time - m_StartTime);
            m_SpeedText.text = "Speed : " + meterPerSec * 3.6f + " km/h";
        }
    }
}