namespace KarpysDev.Runup
{
    using TMPro;
    using UnityEngine;

    public class LocationInitializer : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_LocationStatus = null;
        private void Start()
        {
            Input.location.Start(1,1);
        }

        private void Update()
        {
            m_LocationStatus.text = "Status : " + Input.location.status;
        }
    }
}