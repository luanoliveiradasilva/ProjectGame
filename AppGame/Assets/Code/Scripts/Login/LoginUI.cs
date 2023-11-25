using TMPro;
using UnityEngine;

public class LoginUI : MonoBehaviour
{
    [Header("Login Player")]
    [Tooltip("input Name player")]
    [SerializeField] private TMP_InputField playerNameInput;

    private void Start()
    {
        AdminNetworkManager.instance.GetStartDiscovery();
    }

    //Adicionar o discovery antes, para chamar o ip antes.
    public void SetPlayerName()
    {
        string newName = playerNameInput.text;

        if (!string.IsNullOrEmpty(newName))
        {
            PlayerPrefs.SetString("Player", newName);
        }
    }
}
