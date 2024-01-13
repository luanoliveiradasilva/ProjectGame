using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimationVictory : MonoBehaviour
{
    [SerializeField] private float fadeTime = 1.0f;

    private void OnEnable()
    {
        gameObject.transform.localScale = Vector3.zero;
        gameObject.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
    }
}
