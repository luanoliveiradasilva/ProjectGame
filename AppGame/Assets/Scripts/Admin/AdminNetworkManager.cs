using System;
using System.Collections.Generic;
using Mirror;
using Mirror.Discovery;
using UnityEngine;

[AddComponentMenu("")]
public class AdminNetworkManager : NetworkManager
{
    public static AdminNetworkManager instance { get; private set; }

    //TODO adiconar property talvez.
    private string playerName;
    private string playerScore;
    private string nameGame;

    [Serializable]
    public class PlayerData
    {
        public string namePlayer;
        public string nameGame;
        public string screen;
        public string error;
        public string hit;
        public string playerScore;
    }

    public List<PlayerData> playerDataList = new();

    readonly Dictionary<long, ServerResponse> discoveredServers = new();

    public NetworkDiscovery networkDiscovery;


    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn) => base.OnServerAddPlayer(conn);

    #region Server
    public void SetIpAddress(string ipAddress) => networkAddress = ipAddress;
    public void GetStartDiscovery()
    {
        discoveredServers.Clear();
        networkDiscovery.OnServerFound.AddListener(OnDiscoveredServer);
        networkDiscovery.StartDiscovery();
    }
    private void OnDiscoveredServer(ServerResponse info)
    {
        discoveredServers[info.serverId] = info;
        SetIpAddress(info.EndPoint.Address.ToString());
        Debug.Log("Debug " + info.EndPoint.Address.ToString());
    }

    public void NetAdvertiseServer()
    {
        discoveredServers.Clear();
        StartHost();
        networkDiscovery.AdvertiseServer();
    }
    #endregion

    #region Data

    public void SetPlayerData(List<Player.PlayerDatas> playerDatas)
    {
        foreach (var item in playerDatas)
        {
            playerName = item.namePlayerData;
            nameGame = item.nameGameData;
            playerScore = item.playerScoreData;
        }

        PlayerData playerData = new()
        {
            namePlayer = playerName,
            nameGame = nameGame,
            playerScore = playerScore,
        };

        playerDataList.Add(playerData);
    }
    #endregion
}
