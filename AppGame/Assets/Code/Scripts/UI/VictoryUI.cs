using System;
using UnityEngine;
using UnityEngine.UI;


public class VictoryUI : MonoBehaviour
{
    [SerializeField] private Button button;

    private Player player;

    private void Awake()
    {
        try
        {
            bool isExistPlayer = AdminNetworkManager.instance.SetServerPlayer();

            if (isExistPlayer)
            {
                player = FindObjectOfType<Player>();
                button.onClick.AddListener(ExecutartComando);
                Debug.Log($"Victory!");
            }
        }
        catch (Exception ex)
        {
            throw new ArgumentException("It lost! " + ex.Message);
        }
    }

    private void ExecutartComando()
    {
        player.ExecutartComando();
    }
}

