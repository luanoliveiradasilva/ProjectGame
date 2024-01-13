using System;
using UnityEngine;
using UnityEngine.UI;

public class CostumizeEyes : MonoBehaviour
{
    [SerializeField] private GameObject childObjectEye;
    [SerializeField] private Button[] colorsButton;

    private Transform getChild;
    private bool isChild;
    private Color getColors;

    private void Start()
    {
        getColors = Color.white;

        for (int i = 0; i < colorsButton.Length; i++)
        {
            int buttonIndex = i;
            colorsButton[i].onClick.AddListener(() => OnSliderValueChanged(buttonIndex));
        }
    }

    private void Update()
    {
        try
        {
            getChild = childObjectEye.transform.GetChild(0);

            isChild = true;

            if (isChild)
            {
                getChild.GetComponent<Image>().color = getColors;
            }
        }
        catch (Exception)
        {
            isChild = false;
        }
    }

    private void OnSliderValueChanged(int buttonIndex)
    {
        getColors = colorsButton[buttonIndex].GetComponent<Image>().color;
    }

}
