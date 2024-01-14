using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPosObjects : MonoBehaviour
{

    [SerializeField] private GameObject getObjects;
    [SerializeField] private GameObject getObjectsTwo;
    [SerializeField] private GameObject getObjectsTree;
    private Vector3 getPosObjectProdOne;
    private Vector3 getPosObjectProdTwo;
    private Vector3 getPosObjectProdTree;


    void OnEnable()
    {
       getPosObjectProdOne =  getObjects.transform.position;
       getPosObjectProdOne =  getObjectsTwo.transform.position;
       getPosObjectProdOne =  getObjectsTree.transform.position;
    }
    void OnDisable()
    {
        getObjects.transform.position = getPosObjectProdOne;
        getObjectsTwo.transform.position = getPosObjectProdOne;
        getObjectsTree.transform.position = getPosObjectProdOne;
    }
}
