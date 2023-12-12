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
}
