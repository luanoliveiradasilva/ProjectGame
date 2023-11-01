using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

[AddComponentMenu("")]
public class AdminNetworkManager : NetworkManager
{
    public static AdminNetworkManager instance { get; private set; }

    [Serializable]
    public class PlayerData
    {
        public string namePlayer;
        public float playerScore;
    }

    public List<PlayerData> playerDataList = new();

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        SetPlayerData();
        base.OnServerAddPlayer(conn);
    }

    public void SetIpAddress(string ipAddress) => networkAddress = ipAddress;

    private void SetPlayerData()
    {
        PlayerData addPlayer = new()
        {
            namePlayer = PlayerPrefs.GetString("Player"),
            playerScore = PlayerPrefs.GetFloat("Score")
        };

        playerDataList.Add(addPlayer);
    }
}
