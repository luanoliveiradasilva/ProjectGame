using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Scripts.Costumize;
using UnityEngine;
using UnityEngine.UI;

public class CostumizeManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> listOfPanels;
    [SerializeField] private GameObject getPanel;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button returnButton;

    /*  private readonly List<GameObject> listOfInPanels = new(); */

    private GameObject getObjectInPanel;
    private int indexPanels;
    private Button nextBtn;
    private Button returnBtn;
    [SerializeField] private float DOMoveXPanel = 500;


    private void Awake()
    {
        nextBtn = nextButton.GetComponent<Button>();
        nextBtn.onClick.AddListener(OnNextButton);

        returnBtn = returnButton.GetComponent<Button>();
        returnBtn.onClick.AddListener(OnReturnButton);

        returnButton.interactable = false;
    }

    private void Start()
    {
        GetObjectPanel();
        GetObjectsChild(listOfPanels);
    }

    private void GetObjectPanel()
    {
        foreach (var item in Enum.GetNames(typeof(EnumTags)))
        {
            var getTransformObjectPanel = getPanel.transform;

            int childPanel = getTransformObjectPanel.childCount;

            for (int index = 0; index < childPanel; index++)
            {
                getObjectInPanel = getTransformObjectPanel.GetChild(index).gameObject;

                bool isTagName = getObjectInPanel.CompareTag(item);

                if (isTagName)
                {
                    listOfPanels.Add(getObjectInPanel);
                }
            }
        }
    }

    private void GetObjectsChild(List<GameObject> getGameObjects)
    {
        for (int i = 0; i < getGameObjects.Count; i++)
        {
            var getTransformObject = getGameObjects[i].transform;
            int child = getTransformObject.childCount;

            for (int j = 0; j < child; j++)
            {
                var getObject = getTransformObject.GetChild(j).gameObject;

                if (!getObject.name.Equals("Colors"))
                    getObject.AddComponent<DragAndDrop>();

                if (getObject.name.Equals("Colors"))
                {
                    var getLayoutElement = getObject.GetComponent<LayoutElement>();

                    getLayoutElement.ignoreLayout = true;
                }
            }
        }
    }

    private void OnNextButton()
    {
        listOfPanels[indexPanels].transform.DOMoveX(DOMoveXPanel, 1).SetEase(Ease.OutCirc);

        var getLastElement = listOfPanels.IndexOf(listOfPanels.Last());

        if (nextBtn.interactable == true)
            returnBtn.interactable = true;

        if (indexPanels == getLastElement)
            nextBtn.interactable = false;

        indexPanels++;
    }

    private void OnReturnButton()
    {
        var getFirstElement = listOfPanels.IndexOf(listOfPanels.First());

        indexPanels--;

        if (indexPanels == getFirstElement)
        {
            indexPanels = getFirstElement;
            returnBtn.interactable = false;
        }

        if (returnBtn.interactable == true)
            nextBtn.interactable = true;

        listOfPanels[indexPanels].transform.DOMoveX(-DOMoveXPanel, 1).SetEase(Ease.OutCirc);
    }
}
