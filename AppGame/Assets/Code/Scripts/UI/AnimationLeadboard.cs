using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationLeadboard : MonoBehaviour
{

    [SerializeField] private List<GameObject> listComponents = new();

    [SerializeField] private List<GameObject> listStars = new();

    [SerializeField] private GameObject componentButtons;
    [SerializeField] private GameObject leadboard;
    [SerializeField] private GameObject exitButton;

    [SerializeField] private float scalePanelX;
    [SerializeField] private float scalePanelY;
    [SerializeField] private float scaleButtonExit = 1.0f;

    [SerializeField] private float scalePButtonExitX;
    [SerializeField] private float scalePButtonExitY;

    [SerializeField] private float scaleStarsX;
    [SerializeField] private float scaleStarsY;
    private float DoMovePosY = 150;
    private Image getImage;
    private readonly float WaitTime = 0.25f;

    private readonly float WaitTimeScale = 1.0f;

    private void OnEnable()
    {
        listComponents.Clear();
        ActiveAnimationLeadboard();
    }

    private void OnDisable()
    {
        leadboard.transform.DOKill();

        exitButton.transform.DOKill();

        foreach (var item in listStars)
        {
            item.transform.DOKill();
        }

        foreach (var item in listComponents)
        {
            item.transform.DOMoveY(-DoMovePosY, 1);
            item.transform.DOMoveY(-DoMovePosY, 1);
            item.transform.DOKill();
        }

        foreach (var item in listComponents)
        {
            if (item.name.Equals("StartHostButton"))
            {
                int child = item.transform.childCount;

                for (int i = 0; i < child; i++)
                {
                    var getButtonServer = item.transform.GetChild(i).gameObject;

                    getImage = getButtonServer.GetComponent<Image>();

                    getImage.color = Color.gray;
                }
            }
        }
    }
    private void ActiveAnimationLeadboard()
    {
        int chilButton = componentButtons.transform.childCount;

        for (var i = 0; i < chilButton; i++)
        {
            var buttons = componentButtons.transform.GetChild(i).gameObject;

            if (buttons != null)
            {
                listComponents.Add(buttons);
            }
        }

        StartCoroutine(WaitAddListToBeginAnimation());
    }
    IEnumerator WaitAddListToBeginAnimation()
    {
        leadboard.transform.localScale = Vector3.zero;
        exitButton.transform.localScale = Vector3.zero;

        foreach (var item in listStars)
        {
            item.transform.localScale = Vector3.zero;
        }

        leadboard.transform.DOScaleX(scalePanelX, WaitTimeScale).SetEase(Ease.OutBounce);
        leadboard.transform.DOScaleY(scalePanelY, WaitTimeScale).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(WaitTime);

        exitButton.transform.DOScaleX(scalePButtonExitX, WaitTimeScale).SetEase(Ease.OutCubic);
        exitButton.transform.DOScaleY(scalePButtonExitY, WaitTimeScale).SetEase(Ease.OutCubic);


        yield return new WaitForSeconds(WaitTime);

        foreach (var item in listStars)
        {
            item.transform.DOScaleX(scaleStarsX, WaitTimeScale).SetEase(Ease.OutCubic);

            item.transform.DOScaleY(scaleStarsY, WaitTimeScale).SetEase(Ease.OutCubic);

            yield return new WaitForSeconds(WaitTime);
        }

        foreach (var item in listComponents)
        {
            item.transform.DOMoveY(DoMovePosY, 1).SetEase(Ease.OutCubic);

            yield return new WaitForSeconds(WaitTime);

            item.transform.DOMoveY(DoMovePosY, 1).SetEase(Ease.OutCubic);
        }
    }
}
