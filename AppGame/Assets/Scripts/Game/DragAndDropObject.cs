using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject product;
    [SerializeField] private TextMeshProUGUI valueProductText;

    [SerializeField] private int minRange = 0;
    [SerializeField] private int maxRange = 100;

    private int valuesProduct;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        valueProductText.text = Random.Range(minRange, maxRange).ToString();
        OnSetValues();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }


    public void OnSetValues()
    {
        bool isRight = gameObject.tag == "Right";

        if (isRight)
        {
            valuesProduct = int.Parse(valueProductText.text);

            GameManager.instance.SetValueMemory(valuesProduct);
        }
    }
}
