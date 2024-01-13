using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Screens.FirstScreen
{
    public class AnimationTutorial : MonoBehaviour
    {
        [SerializeField] private float fadeTime = 1.0f;
        [SerializeField] private List<GameObject> ObjectsToScale = new();
        [SerializeField] private GameObject logoMenu;
        [SerializeField] private GameObject buttonIniciar;
        [SerializeField] private float DoMoveButton = -479.0f;
        [SerializeField] private float DoMoveLogo;

        [SerializeField] private float scaleBasket;
        [SerializeField] private float scaleObjects;

        [SerializeField] private float fadeTimeAnimMove;


        private void Start()
        {
            StartCoroutine(AnimlogoDescription());

            foreach (var item in ObjectsToScale)
            {
                item.transform.localScale = Vector3.zero;

                if (item.name == "Basket")
                {
                    item.transform.DOScale(scaleBasket, fadeTime).SetEase(Ease.OutBounce);
                }

                item.transform.DOScale(scaleObjects, fadeTime).SetEase(Ease.OutBounce);
            }

            float alturaDaTela = Screen.height;
            float novaPosicaoY = alturaDaTela * 0.1f;

            buttonIniciar.transform.DOMoveY(novaPosicaoY, 2.0f).SetEase(Ease.OutCubic);
        }

        IEnumerator AnimlogoDescription()
        {
            logoMenu.transform.DOMoveX(DoMoveLogo, 2.0f).SetEase(Ease.OutCubic);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
