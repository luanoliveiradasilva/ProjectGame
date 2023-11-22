using Scripts.Game;
using UnityEngine;
using UnityEngine.UI;


public class TutorialScreen : MonoBehaviour
{

    private TimeGame timeGame;
    
    [Tooltip("Button to start time game")]
    [SerializeField] private Button startTime;


    private void Awake()
    {
        timeGame = FindObjectOfType<TimeGame>();
    }
    // Start is called before the first frame update
    void Start()
    {
         startTime.onClick.AddListener(StartTimeGame);
    }

    private void StartTimeGame()
    {
       timeGame.isExecute = true;
    }
}
