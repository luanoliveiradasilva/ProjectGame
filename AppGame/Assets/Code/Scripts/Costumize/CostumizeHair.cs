using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostumizeHair : MonoBehaviour
{
    [SerializeField] private GameObject childObjectHair;
    [SerializeField] private GameObject getColors;
    [SerializeField] private List<Button> colorsButton = new();
    private Color setColorBegin;
    private Transform getChild;
    private bool isChild;

    void Start()
    {
        setColorBegin = Color.white;

        int child = getColors.transform.childCount;

        for (int i = 0; i < child; i++)
        {
            var getColorToLost = getColors.transform.GetChild(i).GetComponent<Button>();

            colorsButton.Add(getColorToLost);
        }

        for (int i = 0; i < colorsButton.Count; i++)
        {
            int buttonIndex = i;
            colorsButton[i].onClick.AddListener(() => OnSliderValueChanged(buttonIndex));
        }

    }

    void Update()
    {
        try
        {
            getChild = childObjectHair.transform.GetChild(0);

            isChild = true;

            if (isChild)
            {
                getChild.GetComponent<Image>().color = setColorBegin;
            }
        }
        catch (Exception)
        {
            isChild = false;
        }
    }

    private void OnSliderValueChanged(int buttonIndex)
    {
        setColorBegin = colorsButton[buttonIndex].GetComponent<Image>().color;
    }
}
