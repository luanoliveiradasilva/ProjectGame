using UnityEngine;
using UnityEngine.EventSystems;

public class GameObjectDrop : MonoBehaviour, IDropHandler
{
    private ThirdScreenManager correctObject;

    private void Start()
    {
        correctObject = FindObjectOfType<ThirdScreenManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool correct = other.gameObject.name == gameObject.name;

        correctObject.ValuesCorrect(correct);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

}
