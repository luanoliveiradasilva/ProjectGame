using System;
using System.Collections.Generic;
using Mirror;
using Mirror.Discovery;
using UnityEngine;

[AddComponentMenu("")]
public class AdminNetworkManager : NetworkManager
{
    public static AdminNetworkManager instance { get; private set; }

    private string playerName;
    private string playerScore;
    private string nameGame;
    private int countRightProduct;
    private int countWrongProduct;

    [Serializable]
    public class PlayerData
    {
        public string player;
        public string game;
        public string screen;
        public int hit;
        public int error;
        public string time;
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

    public bool SetServerPlayer()
    {
        StartClient();

        bool isActivePlayerInServer = true;

        return isActivePlayerInServer;
    }

    #endregion

    #region Data

    public void SetPlayerData(List<Player.PlayerDatas> playerDatas)
    {
        foreach (var item in playerDatas)
        {
            playerName = item.namePlayerData;
            nameGame = item.nameGameData;
            countRightProduct = item.rightData;
            countWrongProduct = item.wrongData;
            playerScore = item.playerScoreData;
        }

        PlayerData playerData = new()
        {
            player = playerName,
            game = nameGame,
            hit = countRightProduct,
            error = countWrongProduct,
            time = playerScore,
        };

        playerDataList.Add(playerData);
    }
    #endregion
}
