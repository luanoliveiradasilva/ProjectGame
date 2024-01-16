using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class AnimationPanel : MonoBehaviour
    {
        private Animator animator;

        [Header("Time the animation")]
        [SerializeField] private float waitAnimation = 1.25f;

        [Header("Buttons")]
        [SerializeField] private Button returnLogin;
        [SerializeField] private Button showCena;

        [SerializeField] private GameObject cenaPanel;
        [SerializeField] private GameObject mainMenuPanel;

        private readonly int HideButton = Animator.StringToHash("Show");

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            OnUpPanel();
        }

        private void OnUpPanel()
        {
            returnLogin.onClick.AddListener(ReturnLogin);
            showCena.onClick.AddListener(ShowCena);
        }

        private void ReturnLogin()
        {
            animator.SetTrigger(HideButton);
            StartCoroutine(nameof(ReturnLoginMenu));
        }

        private void ShowCena()
        {
            animator.SetTrigger(HideButton);
            StartCoroutine(nameof(ShowCenaPanel));
        }

        private IEnumerator ReturnLoginMenu()
        {
            yield return new WaitForSeconds(waitAnimation);
            SceneManager.LoadScene(0);
        }

        private IEnumerator ShowCenaPanel()
        {
            yield return new WaitForSeconds(waitAnimation);
            mainMenuPanel.SetActive(false);
            cenaPanel.SetActive(true);
        }

    }
}
