using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Screens.SecondScreen
{
    public class AnimationTutorial : MonoBehaviour
    {
        [SerializeField] private float fadeTime = 1.0f;
        [SerializeField] private List<GameObject> ObjectsToScale = new();

        [SerializeField] private GameObject[] numberCount;
        [SerializeField] private GameObject logoMenu;
        [SerializeField] private GameObject buttonIniciar;
        [SerializeField] private float DoMoveButton = -479.0f;
        [SerializeField] private readonly float timeScale = 1f;
        [SerializeField] private readonly float WaitTime = 0.25f;

        [SerializeField] private float moveLogo;

        private void Start()
        {
            StartCoroutine(AnimlogoDescription());

            foreach (var item in ObjectsToScale)
            {
                item.transform.localScale = Vector3.zero;

                item.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
            }

            float alturaDaTela = Screen.height;
            float novaPosicaoY = alturaDaTela * 0.1f;

            buttonIniciar.transform.DOMoveY(novaPosicaoY, 2.0f).SetEase(Ease.OutCubic);
        }

        IEnumerator AnimlogoDescription()
        {
            float alturaDaTela = Screen.width;
            float novaPosicaoX = alturaDaTela * moveLogo;

            logoMenu.transform.DOMoveX(novaPosicaoX, 2.0f).SetEase(Ease.OutCubic);

            foreach (var item in numberCount)
            {
                item.transform.localScale = Vector3.zero;
            }

            foreach (var item in numberCount)
            {
                if (item.name.Equals("Value Text (TMP)"))
                {
                    item.transform.DOScale(timeScale, timeScale).SetEase(Ease.OutBounce);

                    yield return new WaitForSeconds(WaitTime);
                }

                item.transform.DOScale(timeScale, timeScale).SetEase(Ease.OutBounce);
            }
        }
    }
}
