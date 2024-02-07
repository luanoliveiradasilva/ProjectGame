using Scripts.Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Screens.FirstScreen;
using DG.Tweening;

namespace Screens.FirstScreen
{
    public class TutorialScreen : MonoBehaviour
    {

        private TimeGame timeGame;

        [Tooltip("Button to start time game")]
        [SerializeField] private Button startTime;

        [SerializeField] private GameObject tutorial;

        [SerializeField] private bool isTutotial;

        [SerializeField] private Button enableButton;

        private AnimationTutorial animationTutorial;
        public int valueOnTrigger;

        private void Awake()
        {
            animationTutorial = FindObjectOfType<AnimationTutorial>();
            timeGame = FindObjectOfType<TimeGame>();
        }

        void Start()
        {
         
            enableButton.transform.localScale = Vector3.zero;
            if (tutorial.CompareTag("Basket Tutorial") || tutorial.name.Equals("Tutorial Screen 3"))
            {
                isTutotial = true;
            }

            startTime.onClick.AddListener(StartTimeGame);
        }

        private void StartTimeGame()
        {
            timeGame.isExecute = true;
        }

        public void OnTriggerEnterProduct(int onTriggerEnterProduct)
        {
            if (onTriggerEnterProduct >= 3)
            {
                InteractableButton();
            }
        }

        public void OnTriggerEnterEquals(int onTriggerEnterProduct)
        {
            valueOnTrigger += onTriggerEnterProduct;

            if (valueOnTrigger >= 3)
            {
                InteractableButton();
            }
        }

        public void OnClickedButtonIsRight()
        {
            InteractableButton();
        }

        public bool IsTutorialEnabled()
        {
            return isTutotial;
        }

        private void InteractableButton()
        {
            
            enableButton.transform.DOScale(1.0f, 2).SetEase(Ease.OutSine);
        }
    }
}
