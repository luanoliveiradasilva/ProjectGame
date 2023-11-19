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


    [Header("Manager Products")]
    [Tooltip("Object to manage products")]
    [SerializeField] private GameObject products;

    private readonly List<GameObject> product = new();
    private readonly List<TextMeshProUGUI> valueProducts = new();

    private readonly string valueProdutName = "Value Text (TMP)";


    private void Start()
    {
        GetAllProducts();

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
            bool isRight = item.CompareTag("Right");

            if (isRight)
            {
                var valueProductRight = item.transform.Find(valueProdutName).gameObject;

                var getText = valueProductRight.GetComponent<TextMeshProUGUI>();

                GameManager.instance.SetValueMemory(int.Parse(getText.text));
            }
        }
    }
}
