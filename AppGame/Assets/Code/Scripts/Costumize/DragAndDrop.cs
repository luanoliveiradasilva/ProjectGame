using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Costumize
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Canvas canvas;
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        private readonly string nameUI = "Customize UI";

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();

            canvas = GameObject.Find(nameUI).GetComponent<Canvas>();
        }

        private void Start()
        {
           var getRigidbody = GetComponent<Rigidbody2D>();
           getRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
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
    }
}
