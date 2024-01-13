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

        //TODO adicionar condicao para retornar ao lugar quando errar.

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            }
        }
    }
}
