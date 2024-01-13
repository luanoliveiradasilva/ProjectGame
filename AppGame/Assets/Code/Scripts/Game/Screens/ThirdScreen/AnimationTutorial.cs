using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Screens.ThirdScreen
{
    public class AnimationTutorial : MonoBehaviour
    {
        [SerializeField] private float fadeTime = 1.0f;
        [SerializeField] private List<GameObject> ObjectsToScale = new();
        [SerializeField] private GameObject logoMenu;
        [SerializeField] private GameObject buttonIniciar;
        [SerializeField] private GameObject textTutorial;
        [SerializeField] private GameObject step;
        [SerializeField] private GameObject arrow;
        [SerializeField] private float moveLogo;
        [SerializeField] private float DoMoveButton = -479.0f;
        [SerializeField] private readonly float timeScale = 1f;
        [SerializeField] private readonly float WaitTime = 0.5f;

        private void Start()
        {
            StartCoroutine(AnimlogoDescription());

            arrow.transform.localScale = Vector3.zero;

            arrow.transform.DOScale(1.0f, fadeTime).SetEase(Ease.OutBounce);

            foreach (var item in ObjectsToScale)
            {
                item.transform.localScale = Vector3.zero;

                item.transform.DOScale(new Vector3(0.9807647f, 2.042167f, 1.4331f), fadeTime).SetEase(Ease.OutBounce);
            }

            float alturaDaTela = Screen.height;
            float novaPosicaoY = alturaDaTela * 0.1f;

            buttonIniciar.transform.DOMoveY(novaPosicaoY, 2.0f).SetEase(Ease.OutCubic);

            textTutorial.transform.localScale = Vector3.zero;

            textTutorial.transform.DOScale(timeScale, fadeTime).SetEase(Ease.OutBounce);

            step.transform.localScale = Vector3.zero;

            step.transform.DOScale(timeScale, fadeTime).SetEase(Ease.OutBounce);
        }

        IEnumerator AnimlogoDescription()
        {
            logoMenu.transform.DOMoveX(moveLogo, 2.0f).SetEase(Ease.OutCubic);

            yield return new WaitForSeconds(WaitTime);

        }
    }
}
