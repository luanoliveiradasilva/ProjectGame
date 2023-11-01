using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [Header("Child Text Objects")]
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI timeGameText;

    private readonly float existTime = 0.0f;
    private readonly int timeGameConverted = 60;

    public void OnPlayerNameChanged(string namePlayer)
    {
        playerNameText.text = namePlayer;
    }

    public void OnTimeGameChanged(float timeGame)
    {
        if (timeGame > existTime)
        {
            float minutes = Mathf.FloorToInt(timeGame / timeGameConverted);
            float seconts = Mathf.FloorToInt(timeGame % timeGameConverted);

            timeGameText.text = string.Format("{0:00}:{1:00}", minutes, seconts);
        }
    }
}