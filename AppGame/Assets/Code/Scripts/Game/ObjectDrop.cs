using Scripts.Game;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDrop : MonoBehaviour, IDropHandler
{
    [Header("Count products")]

    [Tooltip("Sum of correct products")]
    [SerializeField] private int correctProduct;

    [Tooltip("Sum of incorrect products")]
    [SerializeField] private int incorrectProduct;

    [SerializeField] private GameObject screenVictory;
    [SerializeField] private GameObject screenLevel;
    private TimeGame timeGame;
    private bool isVictory;
    private readonly int quantityProducts = 4;
    private bool isRight;
    private string isName;


    private void Start()
    {
        timeGame = FindObjectOfType<TimeGame>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && isRight)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Right") && isName != other.name)
        {
            correctProduct++;

            SetPlayerPrefs();

            isRight = true;

            isName = other.name;
        }

        if (other.CompareTag("Wrong"))
        {
            incorrectProduct++;
            isRight = false;
        }        
    }

    private void SetPlayerPrefs()
    {
        if (correctProduct.Equals(quantityProducts))
        {
            PlayerPrefs.SetInt("Right", correctProduct);
            PlayerPrefs.SetInt("Wrong", incorrectProduct);

            timeGame.StopTimeGame(false);

            isVictory = true;

            ActiveVictory(isVictory);
        }
    }

    private void ActiveVictory(bool isVictory)
    {
        if (isVictory)
        {
            screenLevel.SetActive(false);
            screenVictory.SetActive(true);
        }
    }
}
