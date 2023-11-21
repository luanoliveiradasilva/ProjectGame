using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMemory : MonoBehaviour
{
    
    public List<int> screenMemory = new();

    public void SetValueMemory(int valuesProduct)
    {
        screenMemory.Add(valuesProduct);
    }
}
