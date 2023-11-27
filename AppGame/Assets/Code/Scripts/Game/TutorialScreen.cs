using Scripts.Game;
using UnityEngine;
using UnityEngine.UI;


public class TutorialScreen : MonoBehaviour
{

    private TimeGame timeGame;

    [Tooltip("Button to start time game")]
    [SerializeField] private Button startTime;

    [SerializeField] private GameObject tutorial;

    [SerializeField] private bool isTutotial;

    [SerializeField] private Button enableButton;


    private void Awake()
    {
        timeGame = FindObjectOfType<TimeGame>();
    }

    void Start()
    {
        isTutotial = tutorial.CompareTag("Tutorial");

        startTime.onClick.AddListener(StartTimeGame);
    }

    private void StartTimeGame()
    {
        timeGame.isExecute = true;
    }

    public void OnTriggerEnterProduct(int onTriggerEnterProduct)
    {
        if (onTriggerEnterProduct >= 3)
        {
            enableButton.interactable = true;
        }
    }

    public bool IsTutorialEnabled()
    {
        return isTutotial;
    }
}
