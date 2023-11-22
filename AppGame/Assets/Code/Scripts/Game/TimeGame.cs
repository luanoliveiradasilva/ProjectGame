using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game
{
    public class TimeGame : MonoBehaviour
    {
        [Tooltip("Button to start time game")]
        [SerializeField] private Button startTime;

        public bool isExecute;
        private float timeExecute;

        private void Update()
        {
            if (isExecute)
            {
                timeExecute += Time.deltaTime;
            }
        }

        public void StartTimeGame(bool isRunTime)
        {
            isExecute = isRunTime;
        }

        public void StopTimeGame(bool isRunTime)
        {
            isExecute = isRunTime;
            float newTimeScore = timeExecute;
            PlayerPrefs.SetFloat("Time", newTimeScore);

            Debug.Log(PlayerPrefs.GetFloat("Time"));
        }
    }
}
