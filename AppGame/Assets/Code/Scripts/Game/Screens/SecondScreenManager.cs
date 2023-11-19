using TMPro;
using UnityEngine;

public class SecondScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject ValueProducts;


    //TODO adicionar os valores na tela, sem precisar criar text mesh de forma manual, mas sim de forma autmatica.
    private void Start()
    {
        for (var i = 0; i < GameManager.instance.screenMemory.Count; i++)
        {
            TextMesh textMesh = ValueProducts.AddComponent<TextMesh>();
            ValueProducts.transform.parent = transform;
        }
    }

}
