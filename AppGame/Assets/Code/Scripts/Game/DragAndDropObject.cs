
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas LevelCanvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private float fadeTime = 1f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / LevelCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
