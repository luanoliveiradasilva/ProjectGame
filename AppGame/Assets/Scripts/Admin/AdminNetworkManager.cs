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
        public string playerScore;
    }

    public List<PlayerData> playerDataList = new();

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn) => base.OnServerAddPlayer(conn);

    public void SetIpAddress(string ipAddress) => networkAddress = ipAddress;

    public void SetPlayerData(string playerName, string playerScore)
    {
        PlayerData addPlayer = new()
        {
            namePlayer = playerName,
            playerScore = playerScore
        };

        playerDataList.Add(addPlayer);
    }
}
