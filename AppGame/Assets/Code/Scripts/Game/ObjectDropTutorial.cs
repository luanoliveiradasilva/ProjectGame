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
        if (other.CompareTag("Tutorial") && nameObject != other.name)
        {
            onTriggerEnterProduct++;

            tutorialScreen.OnTriggerEnterProduct(onTriggerEnterProduct);

            nameObject = other.name;
        }

        if (other.name.Equals(gameObject.name) && other.CompareTag("Tutorial Screen 3"))
        {
            onTriggerEnterProduct++;
            tutorialScreen.OnTriggerEnterEquals(onTriggerEnterProduct);
        }
    }
}
