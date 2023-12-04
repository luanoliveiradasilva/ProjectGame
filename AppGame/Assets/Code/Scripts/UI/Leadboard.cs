using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class Leadboard : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textMeshPro;

        [SerializeField] List<string> teste;

        public void AlterarNome(string playerName)
        {
            textMeshPro.text += " \n" + playerName;

            teste.Add(playerName);
        }
    }
}
