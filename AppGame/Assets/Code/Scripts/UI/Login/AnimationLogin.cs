using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class AnimationLogin : MonoBehaviour
{

    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject logoIcon;
    [SerializeField] private float fadeTime;
    [SerializeField] private float scaleIcons;
    [SerializeField] private float WaitTime;
    [SerializeField] private float DoMoveObjects;
    [SerializeField] private float scaleLogo;

    [SerializeField] private float setPosSettings;

    private Vector3 getPosVectorSetting;
    private float getPosSetting;

    private TweenerCore<Vector3, Vector3, VectorOptions> settingsButtonTween;

    private TweenerCore<Vector3, Vector3, VectorOptions> logoTween;

    private void OnEnable()
    {
        StartCoroutine(AnimIcons());

        SetSettings();

        logoIcon.transform.localScale = Vector3.zero;

        logoTween = logoIcon.transform.DOScale(scaleLogo, 2.0f).SetEase(Ease.OutCubic);

        foreach (var item in buttons)
        {
            item.transform.localScale = Vector3.zero;

        }
    }

    private void SetSettings()
    {
        var height = Screen.height / 2;

        var result = height * 2;

        settingsButtonTween = settingsButton.transform.DOLocalMoveY(result, 2.0f).SetEase(Ease.OutCubic);

        Debug.Log("Debug " + settingsButtonTween);
    }

    private void OnDisable()
    {
        settingsButton.transform.position = getPosVectorSetting;

        settingsButtonTween.Kill();

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

            yield return new WaitForSeconds(WaitTime);
        }
    }
}
