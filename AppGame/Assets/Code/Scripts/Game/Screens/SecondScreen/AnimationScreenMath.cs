using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimationScreenMath : MonoBehaviour
{
    [SerializeField] private List<GameObject> listComponents = new();
    [SerializeField] private GameObject componentButtons;
    [SerializeField] private GameObject componentTexts;

    private readonly float timeScale = 1f;
    private readonly float WaitTime = 0.25f;

    private void Start()
    {
        int chilButton = componentButtons.transform.childCount;

        for (var i = 0; i < chilButton; i++)
        {
            var buttons = componentButtons.transform.GetChild(i).gameObject;

            if (buttons != null)
            {
                listComponents.Add(buttons);
            }
            else
            {
                Debug.LogWarning("Child button transform at index " + i + " is null.");
            }
        }

        int chilText = componentTexts.transform.childCount;

        for (var i = 0; i < chilText; i++)
        {
            var texts = componentTexts.transform.GetChild(i).gameObject;

            if (texts != null)
            {
                listComponents.Add(texts);
            }
            else
            {
                Debug.LogWarning("Child button transform at index " + i + " is null.");
            }
        }

        StartCoroutine(WaitAddListToBeginAnimation());
    }

    IEnumerator WaitAddListToBeginAnimation()
    {
        foreach (var item in listComponents)
        {
            item.transform.localScale = Vector3.zero;
        }

        foreach (var item in listComponents)
        {
            if (item.name.Equals("Text (TMP)"))
            {
                item.transform.DOScale(timeScale, timeScale).SetEase(Ease.OutBounce);

                yield return new WaitForSeconds(WaitTime);
            }

            item.transform.DOScale(timeScale, timeScale).SetEase(Ease.OutBounce);
        }
    }

}
