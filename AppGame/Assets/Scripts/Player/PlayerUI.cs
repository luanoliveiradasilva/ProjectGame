using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Child Text Objects")]
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI timeGameText;

    private readonly float existTime = 0.0f;


    public void OnPlayerNameChanged(string namePlayer)
    {
        playerNameText.text = namePlayer;

    }

    public void OnTimeGameChanged(float timeGame)
    {

        if (timeGame > existTime)
        {
            float minutes = Mathf.FloorToInt(timeGame / 60);
            float seconts = Mathf.FloorToInt(timeGame % 60);

            timeGameText.text = string.Format("{0:00}:{1:00}", minutes, seconts);
        }
    }
}