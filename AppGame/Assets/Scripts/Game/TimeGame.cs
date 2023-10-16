using TMPro;
using UnityEngine;


namespace Scripts.Game
{
    public class TimeGame : MonoBehaviour
    {
        private float timeExecute;

        [SerializeField] TextMeshProUGUI timeGame;
        private bool isExecute = false;
        
        void Start()
        {
            isExecute = true;
        }

        void Update()
        {
            if (isExecute)
                timeExecute += Time.deltaTime;

            DisplayTimeGame(timeExecute);
        }

        private void DisplayTimeGame(float timeToDisplay)
        {

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconts = Mathf.FloorToInt(timeToDisplay % 60);

            Debug.Log("Debug minutes: "+minutes+" Debug seconts: "+seconts);

            timeGame.text = string.Format("{0:00}:{1:00}", minutes, seconts);
        }
    }
}
