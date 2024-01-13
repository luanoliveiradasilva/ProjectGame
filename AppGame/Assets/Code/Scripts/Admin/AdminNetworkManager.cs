using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Discovery;
using Scripts.Admin;
using Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[AddComponentMenu("")]
public class AdminNetworkManager : NetworkManager
{
    public static AdminNetworkManager instance { get; private set; }

    [SerializeField] private GameObject getComponentInButtonServer;

    [SerializeField] private GameObject login;
    [SerializeField] private GameObject menu;


    [Header("Player UI")]
    [SerializeField] public GameObject playerUIPrefab;
    private GameObject playerUIObject;
    private PlayerUI playerUI;

    private string playerId;
    private string playerName;
    private string playerScore;
    private string nameGame;
    private string screenOfLevel = "Screen";
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
    private bool isServerEnabled;
    private Image getImage;

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
    }


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

        StartCoroutine(StartServerUi());
    }

    IEnumerator StartServerUi()
    {
        int child = getComponentInButtonServer.transform.childCount;

        isServerEnabled = NetworkServer.active;

        for (int i = 0; i < child; i++)
        {
            if (isServerEnabled)
            {
                var getButtonServer = getComponentInButtonServer.transform.GetChild(i).gameObject;

                getImage = getButtonServer.GetComponent<Image>();

                yield return new WaitForSeconds(0.5f);

                getImage.color = Color.green;
            }
        }
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
            playerName = item.player;
            nameGame = item.level;
            screenOfLevel = item.screen;
            countRightProduct = item.right;
            countWrongProduct = item.wrong;
            playerScore = item.time;
        }

        PlayerData playerData = new()
        {
            player = playerName,
            game = nameGame,
            screen = screenOfLevel,
            hit = countRightProduct,
            error = countWrongProduct,
            time = playerScore,
        };

        playerDataList.Add(playerData);
    }
    #endregion


    public void ReloadScenes()
    {
        SceneManager.LoadScene(0);

        login.SetActive(false);
        menu.SetActive(true);
    }
}
