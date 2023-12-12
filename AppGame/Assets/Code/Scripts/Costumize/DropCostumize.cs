using UnityEngine;
using UnityEngine.EventSystems;

public class DropCostumize : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        bool isTagWith = gameObject.tag == other.tag;

        Debug.Log("Debug "+isTagWith);

        if (isTagWith)
        {
            Debug.Log("É Iqual");
        }
    }
}
