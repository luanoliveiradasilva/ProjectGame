using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<int> screenMemory = new();

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetValueMemory(int valuesProduct)
    {
        screenMemory.Add(valuesProduct);
    }
}
