using System;
using System.Collections.Generic;
using Mirror;
using Mirror.Discovery;
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

    readonly Dictionary<long, ServerResponse> discoveredServers = new();

    public NetworkDiscovery networkDiscovery;

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn) => base.OnServerAddPlayer(conn);

    #region Data Server
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
        Debug.Log("Debug "+info.EndPoint.Address.ToString());
    }

    #endregion

    #region Data Player
    public void SetPlayerData(string playerName, string playerScore)
    {
        PlayerData addPlayer = new()
        {
            namePlayer = playerName,
            playerScore = playerScore
        };

        playerDataList.Add(addPlayer);
    }

    public void NetAdvertiseServer()
    {
        discoveredServers.Clear();
        StartHost();
        networkDiscovery.AdvertiseServer();
    }
    #endregion
}
