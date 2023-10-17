using System;
using System.Collections.Generic;
using Mirror;
using Scripts.Game;
using UnityEngine;

public class Player : NetworkBehaviour
{

    public event Action<string> OnPlayerNameChanged;
    public event Action<float> OnPlayerScoreGameChanged;

    static readonly List<Player> playersList = new();

    [Header("Player UI")]
    public GameObject playerUIPrefab;

    GameObject playerUIObject;

    PlayerUI playerUI = null;


    void Start()
    {
        NetworkServer.RegisterHandler<TimeMessage>(OnTimeMessageReceived);
    }
    //https://mirror-networking.gitbook.io/docs/manual/guides/synchronization/syncvars
    #region SyncVars

    [Header("SyncVars")]

    [SyncVar(hook = nameof(PlayerNameChanged))]
    public string playerName;

    [SyncVar(hook = nameof(PlayerScoreGameChanged))]
    public float playerScore;

    void PlayerNameChanged(string _, string newPlayerName)
    {
        OnPlayerNameChanged?.Invoke(newPlayerName);
    }

    void PlayerScoreGameChanged(float _, float newPlayerScoreGame)
    {
        OnPlayerScoreGameChanged?.Invoke(newPlayerScoreGame);
    }

    #endregion

    #region Server

    public override void OnStartServer()
    {
        base.OnStartServer();

        playersList.Add(this);
        playerName = PlayerPrefs.GetString("Player");
        playerScore = 0.0f;
    }

    public override void OnStopServer()
    {
        CancelInvoke();
        playersList.Remove(this);
    }

    #endregion

    #region Client

    public override void OnStartClient()
    {
        playerUIObject = Instantiate(playerUIPrefab, AdminUI.GetPlayersPanel());
        playerUI = playerUIObject.GetComponent<PlayerUI>();

        OnPlayerNameChanged = playerUI.OnPlayerNameChanged;
        OnPlayerScoreGameChanged = playerUI.OnTimeGameChanged;

        OnPlayerNameChanged.Invoke(playerName);
        OnPlayerScoreGameChanged.Invoke(playerScore);
    }

    public override void OnStartLocalPlayer()
    {
        playerUI.SetLocalPlayer();

        AdminUI.SetActive(true);
    }

    public override void OnStopLocalPlayer()
    {

        AdminUI.SetActive(false);
    }

    public override void OnStopClient()
    {
        OnPlayerNameChanged = null;
        OnPlayerScoreGameChanged = null;

        Destroy(playerUIObject);
    }

    #endregion

    #region Message

    void OnTimeMessageReceived(NetworkConnection conn, TimeMessage msg)
    {
        float receivedTime = msg.timePlayerGame;
        playerScore = receivedTime;
    }

    #endregion
}
