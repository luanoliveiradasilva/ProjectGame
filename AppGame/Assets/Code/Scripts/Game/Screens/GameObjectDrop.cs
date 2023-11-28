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
        bool isNameIqualsToGameobject = other.gameObject.name == gameObject.name;

        correctObject.ValuesCorrect(isNameIqualsToGameobject);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

}
