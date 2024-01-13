using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationLeadboard : MonoBehaviour
{

    [SerializeField] private List<GameObject> listComponents = new();

    [SerializeField] private GameObject componentButtons;
    [SerializeField] private GameObject leadboard;
    [SerializeField] private GameObject exitButton;
    private float DoMovePosX = 50;
    private float DoMovePosY = 150;
    private Image getImage;
    private readonly float WaitTime = 0.25f;

    private void OnDisable()
    {
        exitButton.transform.DOMoveX(-DoMovePosX, 1);

        foreach (var item in listComponents)
        {
            item.transform.DOMoveY(-DoMovePosY, 1);
            item.transform.DOMoveY(-DoMovePosY, 1);
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

    private void OnEnable()
    {
        listComponents.Clear();
        ActiveAnimationLeadboard();
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

        leadboard.transform.DOScale(1f, 1f).SetEase(Ease.OutBounce);

        exitButton.transform.DOMoveX(DoMovePosX, 1).SetEase(Ease.OutCubic);

        yield return new WaitForSeconds(WaitTime);

        foreach (var item in listComponents)
        {
            item.transform.DOMoveY(DoMovePosY, 1).SetEase(Ease.OutCubic);

            yield return new WaitForSeconds(WaitTime);

            item.transform.DOMoveY(DoMovePosY, 1).SetEase(Ease.OutCubic);
        }
    }
}
