using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public Button setNameButton;
    public static LoginUI instance;

    void Awake()
    {
        instance = this;
    }

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
            PlayerPrefs.SetString("Player", newName);
        }
    }
}
