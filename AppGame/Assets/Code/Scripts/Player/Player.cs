using System;
using System.Collections.Generic;
using Mirror;
using Scripts.Admin;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public event Action<string> OnPlayerNameChanged;
    public event Action<string> OnPlayerScoreGameChanged;


    [Header("Player UI")]
    [SerializeField] public GameObject playerUIPrefab;
    private GameObject playerUIObject;
    private PlayerUI playerUI;

    private string playerNameLocal;
    private string nameLevelLocal;
    private string screenLevelLocal;
    private int rightLocal;
    private int wrongLocal;
    private string newTimeLocal;

    private float getTimeGame;
    private float newTimeGame;
    private string newTime;

    [Serializable]
    public class PlayerDatas
    {
        public string player;
        public string level;
        public string screen;
        public int right;
        public int wrong;
        public string time;
    }

    #region SyncVars

    [Header("SyncVars")]

    [SyncVar(hook = nameof(PlayerNameChanged))]
    public string playerName;

    [SyncVar(hook = nameof(PlayerScoreGameChanged))]
    public string playerScore;


    private void PlayerNameChanged(string oldPlayerName, string newPlayerName) => OnPlayerNameChanged?.Invoke(newPlayerName);

    private void PlayerScoreGameChanged(string oldPlayerScoreGame, string newPlayerScoreGame) => OnPlayerScoreGameChanged?.Invoke(newPlayerScoreGame);

    #endregion

    #region Client
    public override void OnStartClient()
    {

        SetDataPlayerToLeadboard();

        InstantiatePlayerDataInTheUI();
    }

    private void SetDataPlayerToLeadboard()
    {
        CmdSetPlayerNames(playerNameLocal);
        CmdSetPlayerTime(newTimeLocal);
    }


    private void InstantiatePlayerDataInTheUI()
    {
        playerUIObject = Instantiate(playerUIPrefab, AdminUI.GetPlayersPanel());
        playerUI = playerUIObject.GetComponent<PlayerUI>();
    }

    public override void OnStopClient()
    {
        OnPlayerNameChanged = null;
        OnPlayerScoreGameChanged = null;

        Destroy(playerUIObject);
    }

    public void ExecutartComando()
    {
        playerNameLocal = PlayerPrefs.GetString("Player");
        nameLevelLocal = PlayerPrefs.GetString("Level");
        screenLevelLocal = PlayerPrefs.GetString("Screen");
        rightLocal = PlayerPrefs.GetInt("Right");
        wrongLocal = PlayerPrefs.GetInt("Wrong");

        getTimeGame = PlayerPrefs.GetFloat("Time");

        float minutesLocal = Mathf.FloorToInt(getTimeGame / 60);
        float secontsLocal = Mathf.FloorToInt(getTimeGame % 60);

        newTimeLocal = string.Format("{0:00}:{1:00}", minutesLocal, secontsLocal);

        SetPlayerData();

        newTimeGame += getTimeGame;

        float minutes = Mathf.FloorToInt(newTimeGame / 60);
        float seconts = Mathf.FloorToInt(newTimeGame % 60);

        newTime = string.Format("{0:00}:{1:00}", minutes, seconts);

        CmdBotaoClicado(newTime, playerNameLocal);
    }

    [Command(requiresAuthority = false)]
    private void CmdBotaoClicado(string newTime, string playerNameLocal) => RpcReceive(newTime, playerNameLocal);

    [ClientRpc]
    void RpcReceive(string newTimeGame, string playerNameLocal)
    {
        if (playerUIObject.TryGetComponent<PlayerUI>(out playerUI))
        {
            playerUI.OnPlayerNameChanged(playerNameLocal);
            playerUI.OnTimeGameChanged(newTimeGame);
        }
        else
        {
            Debug.Log("Objeto PlayerUI UI não encontrado.");
        }
    }


    private void SetPlayerData()
    {
        List<PlayerDatas> playerDatas = new();

        PlayerDatas playerSetData = new()
        {
            player = playerNameLocal,
            level = nameLevelLocal,
            screen = screenLevelLocal,
            right = rightLocal,
            wrong = wrongLocal,
            time = newTimeLocal
        };

        playerDatas.Add(playerSetData);

        RpcCommandSet(playerDatas);
    }

    [Command]
    private void RpcCommandSet(List<PlayerDatas> playerDatas) =>
        AdminNetworkManager.instance.SetPlayerData(playerDatas);

    #endregion

    //Name Player
    [Command]
    private void CmdSetPlayerNames(string localPlayerName) => RpcSetPlayerName(localPlayerName);

    [ClientRpc]
    private void RpcSetPlayerName(string localPlayerName) => playerName = localPlayerName;

    //Time Player
    [Command]
    private void CmdSetPlayerTime(string newScore) => RpcSetPlayerScore(newScore);

    [ClientRpc]
    private void RpcSetPlayerScore(string newScore) => playerScore = newScore;

}

