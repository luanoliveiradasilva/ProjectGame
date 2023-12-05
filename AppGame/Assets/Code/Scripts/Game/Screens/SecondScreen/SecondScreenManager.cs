using System.Collections.Generic;
using System.Linq;
using Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Screens.SecondScreen
{
    public class SecondScreenManager : MonoBehaviour
    {

        [Header("Range Value")]

        [Tooltip("Minimum range for product values.")]
        [SerializeField] private int minRange = 0;

        [Tooltip("Maximum range for product values.")]
        [SerializeField] private int maxRange = 100;

        [Header("Text value")]
        [Tooltip("Text value to sum")]
        [SerializeField] private TextMeshProUGUI[] textValue;

        [Header("Buttons value")]
        [Tooltip("Text value to sum")]
        [SerializeField] private List<Button> buttonsMath = new();

        [SerializeField] private int maxRangeToTag = 0;

        [SerializeField] private GameObject screenVictory;
        [SerializeField] private GameObject screenLevel;

        private LevelManager saveMemory;

        private TimeGame timeGame;

        private int sumRightButton;
        private int sumWrongButton;
        private bool isVictory;
        private readonly string tagRight = "Right";

        private readonly string tagWrong = "Wrong";
        private readonly string tagUntagged = "Untagged";


        private void Awake()
        {
            saveMemory = FindObjectOfType<LevelManager>();
            timeGame = FindObjectOfType<TimeGame>();

            SetTagInbutton();
        }

        void Start()
        {

            SetValueInText();

            SetValueInButton();

            VerifyButtons();
        }

        private void SetValueInText()
        {
            for (var i = 0; i < textValue.Length; i++)
            {
                textValue[i].text = saveMemory.setValueToSecondScreen[i].ToString();
            }
        }

        private void SetTagInbutton()
        {
            int indexRandom = Random.Range(0, buttonsMath.Count);

            buttonsMath[indexRandom].tag = tagRight;

            foreach (var item in buttonsMath)
            {
                if (item.CompareTag(tagUntagged))
                {
                    item.tag = tagWrong;
                }
            }
        }

        private void SetValueInButton()
        {
            foreach (var item in buttonsMath)
            {
                bool isRight = item.CompareTag(tagRight);

                if (isRight)
                {
                    var getText = item.GetComponentInChildren<TextMeshProUGUI>();

                    getText.text = saveMemory.setValueToSecondScreen.AsQueryable().Sum().ToString();
                }
                else
                {
                    var getText = item.GetComponentInChildren<TextMeshProUGUI>();

                    getText.text = Random.Range(minRange, maxRange).ToString();
                }
            }
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

        private void RightFunction()
        {
            sumRightButton++;
        
            PlayerPrefs.SetString("Screen", "Tela 2");
            PlayerPrefs.SetInt("Right", sumRightButton);
            PlayerPrefs.SetInt("Wrong", sumWrongButton);

            timeGame.StopTimeGame(false);

            isVictory = true;

            ActiveVictory(isVictory);
        }

        private void WrongFunction()
        {
            sumWrongButton++;
        }

        private void ActiveVictory(bool isVictory)
        {
            if (isVictory)
            {
                screenLevel.SetActive(false);
                screenVictory.SetActive(true);
            }
        }
    }
}