using System;
using System.Threading.Tasks;
using DG.Tweening;
using Scripts.Admin;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginUI : MonoBehaviour
{
    [Header("Login Player")]
    [Tooltip("input Name player")]
    [SerializeField] private TMP_InputField playerNameInput;
    private bool isSetPlayerName;

    private void OnDisable()
    {
        DOTween.KillAll();
    }

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
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
    }
}
