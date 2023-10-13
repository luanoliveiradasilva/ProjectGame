using Mirror;
using TMPro;
using UnityEngine;

public class ClientUI : MonoBehaviour
{

    [Header("Client Username")]
    public string playerName;

    public void SetPlayername(string username)
    {
        playerName = username;
        
    }

}
