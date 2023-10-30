using System;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public event Action<string> OnPlayerNameChanged;
    public event Action<float> OnPlayerScoreGameChanged;

    [Header("Player UI")]
    [SerializeField] public GameObject playerUIPrefab;
    GameObject playerUIObject;
    PlayerUI playerUI = null;

    [Header("Variaables Local")]
    private float newScore;
    private string localPlayerName;

    #region SyncVars

    [Header("SyncVars")]

    [SyncVar(hook = nameof(PlayerNameChanged))]
    public string playerName;

    [SyncVar(hook = nameof(PlayerScoreGameChanged))]
    public float playerScore;

    void PlayerNameChanged(string oldPlayerName, string newPlayerName)
    {
        OnPlayerNameChanged?.Invoke(newPlayerName);
    }

    void PlayerScoreGameChanged(float oldPlayerScoreGame, float newPlayerScoreGame)
    {
        OnPlayerScoreGameChanged?.Invoke(newPlayerScoreGame);
    }

    #endregion

    #region Client
    public override void OnStartClient()
    {
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

    void SetDataPlayer()
    {
        localPlayerName = PlayerPrefs.GetString("Player");
        newScore = PlayerPrefs.GetFloat("Score");

        CmdSetPlayerNames(localPlayerName);
        CmdSetPlayerScore(newScore);
    }

    //Name Player
    [Command]
    private void CmdSetPlayerNames(string localPlayerName) =>
   RpcSetPlayerName(localPlayerName);


    [ClientRpc]
    private void RpcSetPlayerName(string localPlayerName)
    {
        playerName = localPlayerName;
    }

    //Score Player
    [Command]
    private void CmdSetPlayerScore(float newScore) =>
       RpcSetPlayerScore(newScore);

    [ClientRpc]
    private void RpcSetPlayerScore(float newScore)
    {
        playerScore = newScore;
    }


    #endregion
}
