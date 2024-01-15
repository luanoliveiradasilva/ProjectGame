using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimationScreen : MonoBehaviour
{

    [SerializeField] private GameObject[] activatedProducts;
    [SerializeField] private GameObject[] similarProducts;

    [SerializeField] private readonly float timeScale = 1f;

    [SerializeField] private readonly float WaitTime = 0.25f;

    [SerializeField] private float DoMoveProducts = 0.0f;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(AnimActivedProduts());
        StartCoroutine(AnimSimilarProduts());
    }

    IEnumerator AnimActivedProduts()
    {
        yield return new WaitForSeconds(WaitTime);

        foreach (var item in activatedProducts)
        {
            int child = item.transform.childCount;

            for (int index = 0; index < child; index++)
            {
                var prod = item.transform.GetChild(0).gameObject;
                prod.transform.localScale = Vector3.one;
            }

            var screenSize = Screen.width/2;
            
            var sizePosProdut = screenSize/4;

            item.transform.DOMoveY(sizePosProdut, 1.0f).SetEase(Ease.OutCubic);
        }
    }

    IEnumerator AnimSimilarProduts()
    {

        foreach (var item in similarProducts)
        {
            item.transform.localScale = Vector3.zero;
        }

        yield return new WaitForSeconds(WaitTime);

        foreach (var item in similarProducts)
        {
            int child = item.transform.childCount;

            for (int index = 0; index < child; index++)
            {
                var prod = item.transform.GetChild(0).gameObject;
                prod.transform.localScale = Vector3.one;
            }

            item.transform.DOScale(timeScale, timeScale).SetEase(Ease.OutBounce);

            yield return new WaitForSeconds(WaitTime);
        }
    }

}
