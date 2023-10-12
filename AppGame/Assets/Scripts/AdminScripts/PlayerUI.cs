using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerUI : MonoBehaviour
{
    [Header("Player Components")]
    public Image image;

    [Header("Child Text Objects")]
    public  TextMeshProUGUI playerNameText;

    // Sets a highlight color for the local player
    public void SetLocalPlayer()
    {
        // add a visual background for the local player in the UI
        image.color = new Color(1f, 1f, 1f, 0.1f);
    }

    // This value can change as clients leave and join
    public void OnPlayerNumberChanged(byte newPlayerNumber)
    {
        playerNameText.text = string.Format("Player {0:00}", newPlayerNumber);
    }

    // Random color set by Player::OnStartServer
    public void OnPlayerColorChanged(Color32 newPlayerColor)
    {
        playerNameText.color = newPlayerColor;
    }

    public void OnPlayerNameChanged(string namePlayers)
    {
        playerNameText.text = namePlayers;
    }

}
