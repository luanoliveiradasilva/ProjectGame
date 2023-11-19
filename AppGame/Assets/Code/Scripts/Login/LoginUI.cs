using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    [Header("Login Player")]
    [Tooltip("input Name player")]
    [SerializeField] private TMP_InputField playerNameInput;

    [Tooltip("Start button and set name player")]
    [SerializeField] private Button setNameButton;


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
            AdminNetworkManager.instance.GetStartDiscovery();
        }
    }
    
}
