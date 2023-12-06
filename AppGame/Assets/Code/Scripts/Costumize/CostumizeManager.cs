using System;
using System.Collections.Generic;
using Scripts.Costumize;
using UnityEngine;

public class CostumizeManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsArray;
    [SerializeField] private GameObject getPanel;
    private GameObject getObjectInPanel;
    private readonly List<GameObject> listOfObject = new();

    private void Start()
    {
        GetObjectPanel();
        GetObjectsChild(objectsArray);
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
                    objectsArray.Add(getObjectInPanel);
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

                getObject.AddComponent<DragAndDrop>();

                if (getObject != null)
                {
                    listOfObject.Add(getObject);
                }
            }
        }
    }
}
