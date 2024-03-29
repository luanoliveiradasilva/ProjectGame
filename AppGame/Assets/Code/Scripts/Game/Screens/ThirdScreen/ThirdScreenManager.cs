using System.Collections;
using Scripts.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Screens.ThirdScreen
{
    public class ThirdScreenManager : MonoBehaviour
    {
        [SerializeField] private GameObject products;
        [SerializeField] private Transform[] placeHolderObjects;
        [SerializeField] private Transform[] placeHoldersGameObject;
        [SerializeField] private int correctObject;
        [SerializeField] private int incorrectObject;
        [SerializeField] private GameObject screenVictory;
        [SerializeField] private GameObject screenLevel;
        private GameManager gameManager;

        private TimeGame timeGame;
        private bool isVictory;
        private readonly int quantityProducts = 4;

        private readonly string tagRight = "Right";


        private void Awake()
        {
            timeGame = FindObjectOfType<TimeGame>();
            gameManager = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            int child = products.transform.childCount;

            int j = 0;

            for (int index = 0; index < child; index++)
            {
                var prod = products.transform.GetChild(index).gameObject;

                if (prod.CompareTag(tagRight))
                {
                    InstantiateGameObject(j, prod);

                    InstatiateObject(j, prod);

                    j++;
                }
            }
        }

        private void InstatiateObject(int j, GameObject prod)
        {
            var getProd = Instantiate(prod);

            RemoveComponent(getProd);

            int child = getProd.transform.childCount;

            DestroyObject(child, getProd);

            SetParent(getProd, placeHolderObjects[j]);
        }

        private void InstantiateGameObject(int j, GameObject prod)
        {
            var getProd = Instantiate(prod);

            RemoveComponent(getProd);

            int child = getProd.transform.childCount;

            DestroyObject(child, getProd);

            var getImage = getProd.GetComponent<Image>();
            getImage.color = new Color32(0, 0, 0, 255);

            SetParent(getProd, placeHoldersGameObject[j]);
        }

        private void SetParent(GameObject setProd, Transform transform)
        {
            setProd.transform.position = transform.position;
            setProd.transform.SetParent(transform);
        }

        private void DestroyObject(int child, GameObject getProd)
        {
            for (int i = 0; i < child; i++)
            {
                var getValueToRemove = getProd.transform.GetChild(i).gameObject;

                if (getValueToRemove.name.Equals("Value Text (TMP)") || getValueToRemove.name.Equals("Euro Text (TMP)"))
                {
                    Destroy(getValueToRemove);
                }
            }
        }

        private void RemoveComponent(GameObject getObject)
        {
            var removeComponentDragAndDrop = getObject.GetComponent<DragAndDropObject>();
            var removeComponentRigid = getObject.GetComponent<Rigidbody2D>();
            var removeComponentCollider = getObject.GetComponent<BoxCollider2D>();
            var removeGroupCanvas = getObject.GetComponent<CanvasGroup>();
            var transforRect = getObject.GetComponent<RectTransform>();

            transforRect.anchorMin = new Vector2(0.5f, 0.5f);
            transforRect.anchorMax = new Vector2(0.5f, 0.5f);
            transforRect.pivot = new Vector2(0.5f, 0.5f);

            Destroy(removeComponentDragAndDrop);
            Destroy(removeComponentRigid);
            Destroy(removeComponentCollider);
            Destroy(removeGroupCanvas);
        }

        public void SetRight()
        {
            correctObject++;

            if (correctObject.Equals(quantityProducts) && gameManager.NameGame != "Level 2")
            {
                SetPlayerPrefs(correctObject);
            }

            if (gameManager.NameGame.Equals("Level 2") && correctObject.Equals(6))
            {
                SetPlayerPrefs(correctObject);
            }
        }

        public void SetWrong()
        {
            incorrectObject++;
        }

        private void SetPlayerPrefs(int correctObject)
        {
            PlayerPrefs.SetString("Screen", "Tela 3");
            PlayerPrefs.SetInt("Right", correctObject);
            PlayerPrefs.SetInt("Wrong", incorrectObject);

            timeGame.StopTimeGame(false);

            isVictory = true;

            StartCoroutine(WaitActiveScene(isVictory));
        }

        IEnumerator WaitActiveScene(bool isVictory)
        {
            yield return new WaitForSeconds(1f);

            if (isVictory)
            {
                screenLevel.SetActive(false);
                screenVictory.SetActive(true);
            }
        }
    }
}
