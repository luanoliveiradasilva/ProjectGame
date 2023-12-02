using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class FirstScreenManager : MonoBehaviour
{

    [Header("Range Value")]

    [Tooltip("Minimum range for product values.")]
    [SerializeField] private int minRange = 0;

    [Tooltip("Maximum range for product values.")]
    [SerializeField] private int maxRange = 100;

    [Tooltip("This range is used to add the tag randomly to products.")]
    [SerializeField] private int maxRangeToTag = 5;


    [Header("Manager Products")]
    [Tooltip("Object to manage products")]
    [SerializeField] private GameObject products;

    [Header("Manager List")]
    [Tooltip("Object to manage list of products to remember.")]
    [SerializeField] private GameObject listProducts;

    private SaveMemory saveMemory;
    private GameManager gameManager;

    private readonly List<GameObject> product = new();
    private readonly List<TextMeshProUGUI> valueProducts = new();

    private readonly string valueProdutName = "Value Text (TMP)";
    private readonly string tagRight = "Right";
    private readonly string tagWrong = "Wrong";
    private readonly string tagUntagged = "Untagged";
    private readonly string nameLevel = "Level 3";


    private void Awake()
    {
        saveMemory = FindObjectOfType<SaveMemory>();

        gameManager = FindObjectOfType<GameManager>();
    }


    private void Start()
    {
        GetAllProducts();

        SetTagInProduct();

        GetListProduct();

        SearchValuesText();

        AddValuesToProductTexts();

        OnSetValues();
    }

    private void GetAllProducts()
    {
        int child = products.transform.childCount;

        for (int i = 0; i < child; i++)
        {
            var prod = products.transform.GetChild(i).gameObject;

            product.Add(prod);
        }
    }

    private void SetTagInProduct()
    {
        if (gameManager.NameGame.Equals(nameLevel))
        {
            ShuffleList(product);

            for (int i = 0; i < Mathf.Min(maxRangeToTag, product.Count); i++)
            {
                product[i].tag = tagRight;
            }
        }

        foreach (var item in product)
        {
            if (item.CompareTag(tagUntagged))
            {
                item.tag = tagWrong;
            }
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[j], list[i]) = (list[i], list[j]);
        }
    }

    private void GetListProduct()
    {
        int index = 0;

        foreach (var item in product)
        {
            bool isRight = item.CompareTag(tagRight);

            if (isRight)
            {
                var nameProdList = listProducts.transform.GetChild(index).gameObject;

                var getText = nameProdList.GetComponent<TextMeshProUGUI>();

                getText.text = item.name;

                index++;
            }
        }
    }

    private void SearchValuesText()
    {
        foreach (var item in product)
        {
            var valueProduct = item.transform.Find(valueProdutName).gameObject;

            var getText = valueProduct.GetComponent<TextMeshProUGUI>();

            valueProducts.Add(getText);
        }
    }

    private void AddValuesToProductTexts()
    {
        for (int i = 0; i < valueProducts.Count; i++)
        {
            valueProducts[i].text = Random.Range(minRange, maxRange).ToString();
        }
    }

    private void OnSetValues()
    {
        foreach (var item in product)
        {
            bool isRight = item.CompareTag(tagRight);

            if (isRight)
            {
                var valueProductRight = item.transform.Find(valueProdutName).gameObject;

                var getText = valueProductRight.GetComponent<TextMeshProUGUI>();

                saveMemory.SetValueMemory(int.Parse(getText.text));
            }
        }
    }
}
