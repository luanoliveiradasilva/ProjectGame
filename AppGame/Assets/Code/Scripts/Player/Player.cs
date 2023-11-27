using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public event Action<string> OnPlayerNameChanged;
    public event Action<string> OnPlayerScoreGameChanged;
    public event Action<string> OnGameNameChaged;

    [Header("Player UI")]
    [SerializeField] public GameObject playerUIPrefab;
    private GameObject playerUIObject;
    private PlayerUI playerUI;

    private string nameGameLocal;
    private int rightLocal;
    private int wrongLocal;
    private string newScoreLocal;
    private string playerNameLocal;

    private float getTimeGame;
    private float newTimeGame;
    private string newTime;


    [Serializable]
    public class PlayerDatas
    {
        public string namePlayerData;
        public string nameGameData;
        public string screenData;
        public int rightData;
        public int wrongData;
        public string playerScoreData;
    }

    #region SyncVars

    [Header("SyncVars")]

    [SyncVar(hook = nameof(PlayerNameChanged))]
    public string playerName;

    [SyncVar(hook = nameof(PlayerScoreGameChanged))]
    public string playerScore;

    [SyncVar(hook = nameof(GameNameChanged))]
    public string gameName;

    private void GameNameChanged(string oldNameGame, string newNameGame) => OnGameNameChaged?.Invoke(newNameGame);

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
        CmdSetPlayerTime(newScoreLocal);
    }


    private void InstantiatePlayerDataInTheUI()
    {
        playerUIObject = Instantiate(playerUIPrefab, AdminUI.GetPlayersPanel());
        playerUI = playerUIObject.GetComponent<PlayerUI>();

        /* OnPlayerNameChanged = playerUI.OnPlayerNameChanged; */
        /* OnPlayerScoreGameChanged = playerUI.OnTimeGameChanged; */

        /* OnPlayerNameChanged.Invoke(playerName); */
        /*  OnPlayerScoreGameChanged.Invoke(playerScore); */
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
        nameGameLocal = PlayerPrefs.GetString("NameGame");
        rightLocal = PlayerPrefs.GetInt("Right");
        wrongLocal = PlayerPrefs.GetInt("Wrong");

        getTimeGame = PlayerPrefs.GetFloat("Time");

        newTimeGame += getTimeGame;

        float minutes = Mathf.FloorToInt(newTimeGame / 60);
        float seconts = Mathf.FloorToInt(newTimeGame % 60);

        newScoreLocal = string.Format("{0:00}:{1:00}", minutes, seconts);

        SetPlayerData();

        newTime = string.Format("{0:00}:{1:00}", minutes, seconts);

        CmdBotaoClicado(newTime, playerNameLocal);
    }

    [Command(requiresAuthority = false)]
    private void CmdBotaoClicado(string newTime, string playerNameLocal)
    {
        RpcReceive(newTime, playerNameLocal);
    }

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
            namePlayerData = playerNameLocal,
            nameGameData = nameGameLocal,
            screenData = "null",
            rightData = rightLocal,
            wrongData = wrongLocal,
            playerScoreData = newScoreLocal
        };

        playerDatas.Add(playerSetData);

        RpcCommandSet(playerDatas);
    }

    [Command]
    private void RpcCommandSet(List<PlayerDatas> playerDatas)
    {
        AdminNetworkManager.instance.SetPlayerData(playerDatas);
    }

    #endregion

    //Name Player
    [Command]
    private void CmdSetPlayerNames(string localPlayerName) => RpcSetPlayerName(localPlayerName);

    [ClientRpc]
    private void RpcSetPlayerName(string localPlayerName) => playerName = localPlayerName;

    //Score Player
    [Command]
    private void CmdSetPlayerTime(string newScore) => RpcSetPlayerScore(newScore);

    [ClientRpc]
    private void RpcSetPlayerScore(string newScore) => playerScore = newScore;

    //Name game
    [Command]
    private void CmdSetNameGame(string nameGame) => RpcSetNameGame(nameGame);

    [ClientRpc]
    private void RpcSetNameGame(string nameGame) => gameName = nameGame;

}

