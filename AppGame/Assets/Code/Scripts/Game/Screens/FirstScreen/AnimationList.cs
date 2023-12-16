using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Screens.FirstScreen
{
    public class AnimationList : MonoBehaviour
    {

        [SerializeField] private float fadeTime = 1f;
        [SerializeField] private float fadeTimeColor = 1f;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float waitTimeList = 5f;
        [SerializeField] private float waitTimeProduct = 3f;
        [HideInInspector] public bool completedAnimation = false;

        [Header("Position fadein and fadeout")]
        [SerializeField] private float localPosX = 0f;
        [SerializeField] private float localPosY = 0f;

        private AnimationProduct animationProduct;

        private void Awake()
        {
            animationProduct = FindObjectOfType<AnimationProduct>();
        }

        private void Start()
        {
            canvasGroup.alpha = 0f;
            rectTransform.transform.localPosition = new Vector3(localPosX, localPosY, 0f);
            rectTransform.DOAnchorPos(new Vector2(43f, 0f), fadeTime, false).SetEase(Ease.OutElastic);
            canvasGroup.DOFade(1, fadeTimeColor);

            StartCoroutine(PanelFadeOut());
        }

        IEnumerator PanelFadeOut()
        {
            yield return new WaitForSeconds(waitTimeList);

            canvasGroup.alpha = 1f;
            rectTransform.transform.localPosition = new Vector3(localPosX, 0f, 0f);
            rectTransform.DOAnchorPos(new Vector2(43f, localPosY), fadeTime, false).SetEase(Ease.InOutQuint);
            canvasGroup.DOFade(0, fadeTime);

            yield return new WaitForSeconds(waitTimeProduct);

            animationProduct.ActiveAnimation();
        }
    }
}
