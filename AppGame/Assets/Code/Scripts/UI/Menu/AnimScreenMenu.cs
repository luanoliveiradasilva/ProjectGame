using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class AnimScreenMenu : MonoBehaviour
{
    [SerializeField] private GameObject logoMenu;
    [SerializeField] private GameObject buttonExit;
    [SerializeField] private GameObject costumizePlayer;
    [SerializeField] private GameObject beginGame;
    [SerializeField] private Vector3 timeScaleButtons;
    [SerializeField] private Vector3 timeScaleButtonsExit;
    [SerializeField] private float moveLogPos;
    [SerializeField] private float waitTimeScale;

    private void Start()
    {
        StartCoroutine(AnimlogoMenu());
        AnimCostumizePlayer();
        AnimBeginGame();
    }

    IEnumerator AnimlogoMenu()
    {
        logoMenu.transform.DOMoveX(moveLogPos, 2.0f).SetEase(Ease.OutCubic);
        buttonExit.transform.localScale = Vector3.zero;

        yield return new WaitForSeconds(0.5f);

        buttonExit.transform.DOScale(timeScaleButtonsExit, waitTimeScale).SetEase(Ease.OutBounce);
    }

    private void AnimCostumizePlayer()
    {
        costumizePlayer.transform.localScale = Vector3.zero;
        costumizePlayer.transform.DOScale(timeScaleButtons, waitTimeScale).SetEase(Ease.OutBounce);
    }

    private void AnimBeginGame()
    {
        beginGame.transform.localScale = Vector3.zero;
        beginGame.transform.DOScale(timeScaleButtons, waitTimeScale).SetEase(Ease.OutBounce);
    }
}
