using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDropTutorial : MonoBehaviour, IDropHandler
{

    public int onTriggerEnterProduct;

    private string nameObject;

    private TutorialScreen tutorialScreen;

    private void Start()
    {
        tutorialScreen = FindObjectOfType<TutorialScreen>();
    }

     public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && tutorialScreen.IsTutorialEnabled())
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tutorial"))
        {
            onTriggerEnterProduct++;

            tutorialScreen.OnTriggerEnterProduct(onTriggerEnterProduct);

            nameObject = other.name;
        }
    }
}
