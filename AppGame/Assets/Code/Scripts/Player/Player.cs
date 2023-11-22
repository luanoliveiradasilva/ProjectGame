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
        ConvertDataPlayers();

        SetDataPlayerToLeadboard();

        SetDataPlayerToAnalize();

        InstantiatePlayerDataInTheUI();
    }

    private void ConvertDataPlayers()
    {
        float getScore = PlayerPrefs.GetFloat("Time");
        float minutes = Mathf.FloorToInt(getScore / 60);
        float seconts = Mathf.FloorToInt(getScore % 60);

        playerNameLocal = PlayerPrefs.GetString("Player");
        newScoreLocal = string.Format("{0:00}:{1:00}", minutes, seconts);
        nameGameLocal = PlayerPrefs.GetString("NameGame");
        rightLocal = PlayerPrefs.GetInt("Right");
        wrongLocal = PlayerPrefs.GetInt("Wrong");


        Debug.Log($"Debug : {playerNameLocal}\n{newScoreLocal}\n{nameGameLocal}\n{rightLocal}\n{wrongLocal}");
    }

    private void SetDataPlayerToLeadboard()
    {
        CmdSetPlayerNames(playerNameLocal);
        CmdSetPlayerScore(newScoreLocal);
        CmdSetNameGame(nameGameLocal);
    }

    private void SetDataPlayerToAnalize()
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

        CmdSetPlayerData(playerDatas);
    }

    private void InstantiatePlayerDataInTheUI()
    {
        playerUIObject = Instantiate(playerUIPrefab, AdminUI.GetPlayersPanel());
        playerUI = playerUIObject.GetComponent<PlayerUI>();

        OnPlayerNameChanged = playerUI.OnPlayerNameChanged;
        OnPlayerScoreGameChanged = playerUI.OnTimeGameChanged;

        OnPlayerNameChanged.Invoke(playerName);
        OnPlayerScoreGameChanged.Invoke(playerScore);
    }

    public override void OnStopClient()
    {
        OnPlayerNameChanged = null;
        OnPlayerScoreGameChanged = null;

        Destroy(playerUIObject);
    }

    #endregion

    //Name game
    [Command]
    private void CmdSetNameGame(string nameGame) => RpcSetNameGame(nameGame);

    [ClientRpc]
    private void RpcSetNameGame(string nameGame) => gameName = nameGame;

    //Name Player
    [Command]
    private void CmdSetPlayerNames(string localPlayerName) => RpcSetPlayerName(localPlayerName);

    [ClientRpc]
    private void RpcSetPlayerName(string localPlayerName) => playerName = localPlayerName;

    //Score Player
    [Command]
    private void CmdSetPlayerScore(string newScore) => RpcSetPlayerScore(newScore);

    [ClientRpc]
    private void RpcSetPlayerScore(string newScore) => playerScore = newScore;

    //Set data player 
    [Command]
    private void CmdSetPlayerData(List<PlayerDatas> playerDatas)
    {
        AdminNetworkManager.instance.SetPlayerData(playerDatas);
    }
}
