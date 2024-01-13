using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game
{
    public class LevelManager : MonoBehaviour
    {
    
        public List<int> setValueToSecondScreen = new();

        public void SetValueMemory(int valuesProduct)
        {
            setValueToSecondScreen.Add(valuesProduct);
        }
    }
}
