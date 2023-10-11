using TMPro;
using UnityEngine;

public class LeadboardUI : MonoBehaviour
{
    internal static string localPlayerName;
    [SerializeField] TextMeshProUGUI textName;

    // Update is called once per frame
    void Update()
    {
      textName.text = localPlayerName;  
    }
}
