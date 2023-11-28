using Scripts.Game;
using UnityEngine;

public class ThirdScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject products;
    [SerializeField] private Transform[] placeHolderObjects;
    [SerializeField] private Transform[] placeHoldersGameObject;
    [SerializeField] private int correctObject;
    [SerializeField] private int incorrectObject;

    private TimeGame timeGame;

    private readonly int quantityProducts = 4;

    private readonly string tagRight = "Right";


    private void Awake()
    {
        timeGame = FindObjectOfType<TimeGame>();
    }

    private void Start()
    {
        InstanceGameObject();
        InstanceObject();
    }

    private void InstanceGameObject()
    {
        int child = products.transform.childCount;

        int j = 0;

        for (int i = 0; i < child; i++)
        {
            var prod = products.transform.GetChild(i).gameObject;

            if (prod.CompareTag(tagRight))
            {

                var getProd = Instantiate(prod);

                var removeComponentDragAndDrop = getProd.GetComponent<DragAndDropObject>();
                var removeComponentRigid = getProd.GetComponent<Rigidbody2D>();
                var removeComponentCollider = getProd.GetComponent<BoxCollider2D>();

                Destroy(removeComponentDragAndDrop);
                Destroy(removeComponentRigid);
                Destroy(removeComponentCollider);

                getProd.transform.position = placeHoldersGameObject[j].position;
                getProd.transform.SetParent(placeHoldersGameObject[j]);

                j++;
            }
        }
    }

    private void InstanceObject()
    {
        int child = products.transform.childCount;

        int j = 0;

        for (int i = 0; i < child; i++)
        {
            var prod = products.transform.GetChild(i).gameObject;

            if (prod.CompareTag(tagRight))
            {

                var getProd = Instantiate(prod);

                var removeComponentDragAndDrop = getProd.GetComponent<DragAndDropObject>();
                var removeComponentRigid = getProd.GetComponent<Rigidbody2D>();
                var removeComponentCollider = getProd.GetComponent<BoxCollider2D>();

                Destroy(removeComponentDragAndDrop);
                Destroy(removeComponentRigid);
                Destroy(removeComponentCollider);

                getProd.transform.position = placeHolderObjects[j].position;
                getProd.transform.SetParent(placeHolderObjects[j]);

                j++;
            }
        }
    }

    public void ValuesCorrect(bool correct)
    {
        if (correct)
        {
            correctObject++;
            SetPlayerPrefs(correctObject);
        }
        else
        {
            incorrectObject++;
        }
    }

    private void SetPlayerPrefs(int correctObject)
    {
        if (correctObject.Equals(quantityProducts))
        {
            PlayerPrefs.SetInt("Right", correctObject);
            PlayerPrefs.SetInt("Wrong", incorrectObject);
            
            timeGame.StopTimeGame(false);
        }
    }
}
