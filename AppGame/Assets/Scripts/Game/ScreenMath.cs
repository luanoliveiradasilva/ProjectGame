using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMath : MonoBehaviour
{

    void Start()
    {
        foreach (var item in GameManager.instance.screenMemory)
        {
            Debug.Log("Debug " + item);
        }

    }
}
