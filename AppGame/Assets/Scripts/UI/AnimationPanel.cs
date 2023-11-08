using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPanel : MonoBehaviour
{
    private Animator animator;

    [Header("Time the animation")]
    [SerializeField] private float waitAnimation = 1.25f;

    [Header("Panels")]
    [Tooltip("Player login Panel")]
    [SerializeField] private GameObject LoginPanel;

    [Tooltip("Server Panel")]
    [SerializeField] private GameObject serverPanel;

    private readonly int HideButton = Animator.StringToHash("Show");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnUpPanel()
    {
        animator.SetTrigger(HideButton);
        StartCoroutine("ShowServerPanel");
    }

    private IEnumerator ShowServerPanel()
    {
        yield return new WaitForSeconds(waitAnimation);
        LoginPanel.SetActive(false);
        serverPanel.SetActive(true);
    }

}
