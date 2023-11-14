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

//TODO verificar a possibilidade de concluir o jogo so quando terminar alguma cena, ao invés de ter o botão para, além disso, deve enviar para o servidor antes de mudar de cena ou painel.
        public void StopTimeGame()
        {
            stopTime.onClick.AddListener(DisplayTimeGameToLeadboardUI);
            isExecute = false;
        }

        private void BeginTimeGame()
        {
            if (isExecute == true)
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

        private void DisplayTimeGameToLeadboardUI()
        {
            isExecute = false;
            float newTimeScore = timeExecute;
            PlayerPrefs.SetFloat("Score", newTimeScore);
        }
    }
}
