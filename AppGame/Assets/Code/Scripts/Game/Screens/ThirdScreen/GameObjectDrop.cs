using UnityEngine;
using UnityEngine.EventSystems;

namespace Screens.ThirdScreen
{
    public class GameObjectDrop : MonoBehaviour, IDropHandler
    {

        [SerializeField] GameObject[] getPosObjects;
        private ThirdScreenManager thirdScreenManager;
        private DragAndDropObject dragAndDropObject;
        private string isNameRight;
        private string isNameWrong;
        private Vector3 getPos;
        private bool isWrong;

        private void Awake()
        {
            thirdScreenManager = FindObjectOfType<ThirdScreenManager>();
            dragAndDropObject = FindObjectOfType<DragAndDropObject>();
        }

       /*  private void OnTriggerEnter2D(Collider2D other)
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
        } */

        public void OnDrop(PointerEventData eventData)
        {
            isNameWrong = "";
            
            if (eventData.pointerDrag != null && eventData.pointerDrag.name.Equals(gameObject.name) && isNameRight != eventData.pointerDrag.name)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;

                thirdScreenManager.SetRight();

                isNameRight = eventData.pointerDrag.name;
            }

            if (eventData.pointerDrag.name != gameObject.name && isNameWrong != eventData.pointerDrag.name)
            {
                thirdScreenManager.SetWrong();
            }
        }
    }
}
