using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public event System.Action<byte> OnPlayerNumberChanged;
    public event System.Action<Color32> OnPlayerColorChanged;
    public event System.Action<ushort> OnPlayerDataChanged;


    static readonly List<Player> playersList = new();

    [Header("Player UI")]
    public GameObject playerUIPrefab;

    GameObject playerUIObject;
    PlayerUI playerUI = null;

    #region SyncVars

    [Header("SyncVars")]

    [SyncVar(hook = nameof(PlayerNumberChanged))]
    public byte playerNumber = 0;

    [SyncVar(hook = nameof(PlayerColorChanged))]
    public Color32 playerColor = Color.white;

    [SyncVar(hook = nameof(PlayerDataChanged))]
    public ushort playerData = 0;

    void PlayerNumberChanged(byte _, byte newPlayerNumber)
    {
        OnPlayerNumberChanged?.Invoke(newPlayerNumber);
    }

    void PlayerColorChanged(Color32 _, Color32 newPlayerColor)
    {
        OnPlayerColorChanged?.Invoke(newPlayerColor);
    }

    void PlayerDataChanged(ushort _, ushort newPlayerData)
    {
        OnPlayerDataChanged?.Invoke(newPlayerData);
    }

    #endregion

    #region Server

    public override void OnStartServer()
    {
        base.OnStartServer();

        // Add this to the static Players List
        playersList.Add(this);

        // set the Player Color SyncVar
        playerColor = Random.ColorHSV(0f, 1f, 0.9f, 0.9f, 1f, 1f);

        // set the initial player data
        playerData = (ushort)Random.Range(100, 1000);

        // Start generating updates
        InvokeRepeating(nameof(UpdateData), 1, 1);
    }

    [ServerCallback]
    internal static void ResetPlayerNumbers()
    {
        byte playerNumber = 0;
        foreach (Player player in playersList)
            player.playerNumber = playerNumber++;
    }

    [ServerCallback]
    void UpdateData()
    {
        playerData = (ushort)Random.Range(100, 1000);
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
        // Instantiate the player UI as child of the Players Panel
        playerUIObject = Instantiate(playerUIPrefab, AdminUI.GetPlayersPanel());
        playerUI = playerUIObject.GetComponent<PlayerUI>();

        // wire up all events to handlers in PlayerUI
        OnPlayerNumberChanged = playerUI.OnPlayerNumberChanged;
        OnPlayerColorChanged = playerUI.OnPlayerColorChanged;
        //OnPlayerDataChanged = playerUI.OnPlayerDataChanged;

        // Invoke all event handlers with the initial data from spawn payload
        OnPlayerNumberChanged.Invoke(playerNumber);
        OnPlayerColorChanged.Invoke(playerColor);
        //OnPlayerDataChanged.Invoke(playerData);
    }

    //TODO Adicionar tela do leadboard
    public override void OnStartLocalPlayer()
    {
        // Set isLocalPlayer for this Player in UI for background shading
        playerUI.SetLocalPlayer();

        // Activate the main panel
        AdminUI.SetActive(true);
    }

    public override void OnStopLocalPlayer()
    {
        // Disable the main panel for local player
        AdminUI.SetActive(false);
    }

    public override void OnStopClient()
    {
        // disconnect event handlers
        OnPlayerNumberChanged = null;
        OnPlayerColorChanged = null;
        OnPlayerDataChanged = null;

        // Remove this player's UI object
        Destroy(playerUIObject);
    }

    #endregion
}
