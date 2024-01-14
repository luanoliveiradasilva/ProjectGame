using System;
using System.Threading.Tasks;
using Scripts.Admin;
using TMPro;
using UnityEngine;

public class LoginUI : MonoBehaviour
{
    [Header("Login Player")]
    [Tooltip("input Name player")]
    [SerializeField] private TMP_InputField playerNameInput;
    private bool isSetPlayerName;
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject MainMenu;

    private void Start()
    {
        AdminNetworkManager.instance.GetStartDiscovery();
    }

    public void SetPlayerName()
    {
        string newName = playerNameInput.text;

        if (!string.IsNullOrEmpty(newName))
        {
            PlayerPrefs.SetString("Player", newName);

            isSetPlayerName = true;

            Debug.Log($"Player online: {newName}");

            if (isSetPlayerName)
            {
                SetServe();
            }
        }
    }

    private async void SetServe()
    {
        try
        {
            bool isActivatedUser = AdminNetworkManager.instance.SetServerPlayer();

            Debug.Log("Debug "+isActivatedUser);
            await Task.Delay(TimeSpan.FromSeconds(1f));
            
            if (isActivatedUser)
            {
                ReturnScenesGame();
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"Player not logged in!" + ex.Message);
        }
    }

    void ReturnScenesGame()
    {
        loginPanel.SetActive(false);
        MainMenu.SetActive(true);
    }
}
