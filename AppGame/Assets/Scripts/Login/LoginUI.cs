using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public Button setNameButton;

    void Start()
    {
        if (setNameButton != null)
        {
            setNameButton.onClick.AddListener(SetPlayerName);
        }
    }

    public void SetPlayerName()
    {
        string newName = playerNameInput.text;

        if (!string.IsNullOrEmpty(newName))
        {
            PlayerPrefs.SetString("PlayerName", newName);
        }
    }

}
