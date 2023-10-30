using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game
{
    public class TimeGame : MonoBehaviour
    {
        [Header("Time the game")]
        [Tooltip("Time de game to account")]
        [SerializeField] TextMeshProUGUI timeGame;

        [Tooltip("Button to stop time game")]
        [SerializeField] Button stopTime;

        [Tooltip("Keep the games running")]
        private bool isExecute = false;

        [Tooltip("Time Runtime Value")]
        private float timeExecute;

        void Start()
        {
            isExecute = true;
        }

        void Update()
        {
            if (isExecute)
            {
                timeExecute += Time.deltaTime;
                DisplayTimeGame(timeExecute);
            }
        }

        private void DisplayTimeGame(float timeToDisplay)
        {
            if (timeToDisplay > 0.0f)
            {
                float minutes = Mathf.FloorToInt(timeToDisplay / 60);
                float seconts = Mathf.FloorToInt(timeToDisplay % 60);

                timeGame.text = string.Format("{0:00}:{1:00}", minutes, seconts);
            }
        }

        public void DisplayTimeGameToLeadboardUI()
        {
            isExecute = false;
            float newTimeScore = timeExecute;
            PlayerPrefs.SetFloat("Score", newTimeScore);
        }
    }
}
