using System.Collections;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class AnimationLogin : MonoBehaviour
{

    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject logoIcon;
    [SerializeField] private float fadeTime;
    [SerializeField] private float scaleIcons;
    [SerializeField] private float WaitTime;
    [SerializeField] private float DoMoveObjects;
    [SerializeField] private float scaleLogo;

    [SerializeField] private float setPosExit;
    [SerializeField] private Vector3 scaleExit;


    private TweenerCore<Vector3, Vector3, VectorOptions> logoTween;

    private void OnEnable()
    {
        StartCoroutine(AnimIcons());

        logoIcon.transform.localScale = Vector3.zero;

        logoTween = logoIcon.transform.DOScale(scaleLogo, 2.0f).SetEase(Ease.OutCubic);

        foreach (var item in buttons)
        {
            item.transform.localScale = Vector3.zero;

        }
    }

    private void OnDisable()
    {

        logoTween.Kill();

        foreach (var item in buttons)
        {
            item.transform.DOKill();
        }
    }

    IEnumerator AnimIcons()
    {
        foreach (var item in buttons)
        {
            if (item.CompareTag("PlayerName"))
            {
                item.transform.DOScale(new Vector3(2.45f, 4.55f, 4.43f), fadeTime).SetEase(Ease.OutBounce);
            }

            if (item.name == "StartButton")
            {
                item.transform.DOScale(scaleIcons, fadeTime).SetEase(Ease.OutBounce);
            }

            if (item.name == "ExitButton")
            {
                item.transform.DOScale(scaleExit, fadeTime).SetEase(Ease.OutBounce);
            }

            yield return new WaitForSeconds(WaitTime);
        }
    }
}
