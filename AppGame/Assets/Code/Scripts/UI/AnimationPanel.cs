using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class AnimationPanel : MonoBehaviour
    {
        private Animator animator;

        [Header("Time the animation")]
        [SerializeField] private float waitAnimation = 1.25f;

        [Header("Buttons")]
        [SerializeField] private Button showServer;
        [SerializeField] private Button showCena;


        [Header("Panels")]
        [Tooltip("Player login Panel")]
        [SerializeField] private GameObject LoginPanel;

        [Tooltip("Paineis")]
        [SerializeField] private GameObject serverPanel;

        [SerializeField] private GameObject cenaPanel;

        private readonly int HideButton = Animator.StringToHash("Show");


        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            OnUpPanel();
        }

        public void OnUpPanel()
        {
            showServer.onClick.AddListener(ShowServer);

            showCena.onClick.AddListener(ShowCena);
        }

        private void ShowServer()
        {
            animator.SetTrigger(HideButton);
            StartCoroutine("ShowServerPanel");
        }

        private void ShowCena()
        {
            animator.SetTrigger(HideButton);
            StartCoroutine("ShowCenaPanel");
        }

        private IEnumerator ShowServerPanel()
        {
            yield return new WaitForSeconds(waitAnimation);
            LoginPanel.SetActive(false);
            serverPanel.SetActive(true);
        }

        private IEnumerator ShowCenaPanel()
        {
            yield return new WaitForSeconds(waitAnimation);
            LoginPanel.SetActive(false);
            cenaPanel.SetActive(true);
        }

    }
}
