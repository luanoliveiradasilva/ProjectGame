using System;
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

    private string nameGame;
    private string newScore;
    private string localPlayerName;

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
        SetDataPlayer();

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

    private void ConvertDataPlayers()
    {
        var getScore = PlayerPrefs.GetFloat("Score");
        float minutes = Mathf.FloorToInt(getScore / 60);
        float seconts = Mathf.FloorToInt(getScore % 60);

        localPlayerName = PlayerPrefs.GetString("Player");
        newScore = string.Format("{0:00}:{1:00}", minutes, seconts);
    }
    private void SetDataPlayer()
    {
        CmdSetPlayerNames(localPlayerName);
        CmdSetPlayerScore(newScore);
        CmdSetNameGame(nameGame);
        CmdSetPlayerData(localPlayerName, newScore);
    }

    //TODO mudar a chamada RPC em um objeto só, serielizar tudo.

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

    //Set Player data to extract file
    [Command]
    private void CmdSetPlayerData(string localPlayerName, string newScore) => AdminNetworkManager.instance.SetPlayerData(localPlayerName, newScore);

    #endregion
}
