using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class AnimationSettings : MonoBehaviour
{
    [SerializeField] private GameObject[] levelGames;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private float fadeTime;
    [SerializeField] private float scaleIcons;
    [SerializeField] private float WaitTime;
    [SerializeField] private float DoMoveButtonExit;
    private Vector3 getPosVectorExit;

    private float getPosExit;
    private float setPos;
    private TweenerCore<Vector3, Vector3, VectorOptions> exitButtonTween;

    private void OnEnable()
    {
        StartCoroutine(AnimIconsLevel());

        getPosVectorExit = exitButton.transform.position;

        getPosExit = exitButton.transform.position.y;

        setPos = getPosExit + DoMoveButtonExit;

        exitButtonTween?.Kill();

        exitButtonTween = exitButton.transform.DOMoveY(setPos, 2.0f).SetEase(Ease.OutCubic);
    }

    private void OnDisable()
    {
        exitButton.transform.position = getPosVectorExit;

        exitButtonTween.Kill();

        foreach (var item in levelGames)
        {
            item.transform.DOKill();
        }
    }

    IEnumerator AnimIconsLevel()
    {
        foreach (var item in levelGames)
        {
            item.transform.localScale = Vector3.zero;
        }

        foreach (var item in levelGames)
        {
            item.transform.DOScale(scaleIcons, fadeTime).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(WaitTime);
        }
    }
}
