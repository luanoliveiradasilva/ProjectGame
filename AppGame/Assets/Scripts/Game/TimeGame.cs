using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Scripts.Game
{
    public class TimeGame : MonoBehaviour
    {
        private float timeExecute;

        [Header("Time the game")]
        [Tooltip("Time de game to account")]
        [SerializeField] TextMeshProUGUI timeGame;
        [Tooltip("Button to stop time game")]
        [SerializeField] Button stopTime;

        private bool isExecute = false;

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
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconts = Mathf.FloorToInt(timeToDisplay % 60);

            timeGame.text = string.Format("{0:00}:{1:00}", minutes, seconts);
        }

        public void ClickedStopTime()
        {
            isExecute = false;

            TimeMessage msg = new TimeMessage { timePlayerGame = timeExecute };

            NetworkClient.Send(msg);
        }
    }

    public struct TimeMessage : NetworkMessage
    {
        public float timePlayerGame;
    }
}
