
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDrop : MonoBehaviour, IDropHandler
{
    [Header("Count products")]

    [Tooltip("Sum of correct products")]
    [SerializeField] private int correctProduct;

    [Tooltip("Sum of incorrect products")]
    [SerializeField] private int incorrectProduct;

    private bool isRight;

    private readonly int quantityProducts = 4;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && isRight)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Right"))
        {
            correctProduct++;

            SetPlayerPrefs();

            isRight = true;
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
        }
    }
}
