using UnityEngine;
using UnityEngine.EventSystems;

namespace Screens.ThirdScreen
{
    public class GameObjectDrop : MonoBehaviour, IDropHandler
    {
        private ThirdScreenManager thirdScreenManager;
        private string isNameRight;
        private string isNameWrong;

        private void Start()
        {
            thirdScreenManager = FindObjectOfType<ThirdScreenManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            isNameWrong = "";

            if (other.gameObject.name.Equals(gameObject.name) && isNameRight != other.gameObject.name)
            {
                thirdScreenManager.SetRight();

                isNameRight = other.gameObject.name;
            }

            if (other.gameObject.name != gameObject.name && isNameWrong != other.gameObject.name)
            {
                thirdScreenManager.SetWrong();
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            }
        }

    }
}
