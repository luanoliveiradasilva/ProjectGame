using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game
{
    public class TimeGame : MonoBehaviour
    {
        [Header("Time the game")]
        [Tooltip("Time de game to account")]
        [SerializeField] private TextMeshProUGUI timeGame;

        [Tooltip("Button to start time game")]
        [SerializeField] private Button startTime;

        [Tooltip("Button to stop time game")]
        [SerializeField] private Button stopTime;

        private bool isExecute;
        private float timeExecute;

        private void Update()
        {
            BeginTimeGame();
        }

        public void StartTimeGame()
        {
            startTime.onClick.AddListener(BeginTimeGame);
            isExecute = true;
        }

        public void StopTimeGame()
        {
            DisplayTimeGameToLeadboardUI();
        }

        private void BeginTimeGame()
        {
            if (isExecute == true)
            {
                timeExecute += Time.deltaTime;
                DisplayTimeGame(timeExecute);
            }
        }

        //TODO Tirar o time do UI.
        private void DisplayTimeGame(float timeToDisplay)
        {
            if (timeToDisplay > 0.0f)
            {
                float minutes = Mathf.FloorToInt(timeToDisplay / 60);
                float seconts = Mathf.FloorToInt(timeToDisplay % 60);

                timeGame.text = string.Format("{0:00}:{1:00}", minutes, seconts);
            }
        }

        private void DisplayTimeGameToLeadboardUI()
        {
            isExecute = false;
            float newTimeScore = timeExecute;
            PlayerPrefs.SetFloat("Score", newTimeScore);
        }
    }
}
