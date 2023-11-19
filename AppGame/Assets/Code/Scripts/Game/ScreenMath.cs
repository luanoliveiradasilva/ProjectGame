using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenMath : MonoBehaviour
{

    [Header("Text value")]
    [Tooltip("Text value to sum")]
    [SerializeField] private TextMeshProUGUI[] textValue;
    [SerializeField] private TextMeshProUGUI textValueButton;

    [Header("Buttons value")]
    [Tooltip("Text value to sum")]
    [SerializeField] private List<Button> buttonsMath = new();

    private int sumRightButton;
    private int sumWrongButton;


    private void Awake()
    {
        VerifyButtons();
    }

    void Start()
    {
        for (var i = 0; i < textValue.Length; i++)
        {
            textValue[i].text = GameManager.instance.screenMemory[i].ToString();
        }

        textValueButton.text = GameManager.instance.screenMemory.AsQueryable().Sum().ToString();
    }

    private void VerifyButtons()
    {
        foreach (var item in buttonsMath)
        {
            bool isRight = item.CompareTag("Right");
            bool isWrong = item.CompareTag("Wrong");

            if (isRight)
                item.onClick.AddListener(RightFunction);

            if (isWrong)
                item.onClick.AddListener(WrongFunction);
        }
    }
    //TODO verificar a possibilidade de não somar, mas sim só adicionar o valor 1 e adicionar ao playerprefs. Verificar também se o player prefs é afetado pela somando valores constamente mesmo depois de enviar na tela anterior.
    private void RightFunction()
    {
        sumRightButton++;
        PlayerPrefs.SetInt("Right", sumRightButton);
        PlayerPrefs.SetInt("Wrong", sumWrongButton);
    }

    private void WrongFunction()
    {
        sumWrongButton++;
    }
}
