using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Player Components")]
    public Image image;

    [Header("Child Text Objects")]
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI timeGameText;

    public void SetLocalPlayer()
    {

        image.color = new Color(1f, 1f, 1f, 0.1f);
    }

    public void OnPlayerNameChanged(string namePlayers)
    {
        playerNameText.text = namePlayers;
    }

    public void OnTimeGameChanged(float timeGame)
    {
        float minutes = Mathf.FloorToInt(timeGame / 60);
        float seconts = Mathf.FloorToInt(timeGame % 60);
        timeGameText.text = string.Format("{0:00}:{1:00}", minutes, seconts);
    }
}
