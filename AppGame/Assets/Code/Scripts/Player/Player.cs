using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Scripts.Admin;
using Scripts.Player;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public event Action<string> OnPlayerIdChanged;
    public event Action<string> OnPlayerNameChanged;
    public event Action<string> OnPlayerScoreGameChanged;


    [Header("Player UI")]
    [SerializeField] public GameObject playerUIPrefab;
    private GameObject playerUIObject;
    private PlayerUI playerUI;

    private int playerIdLocal;
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

    [SyncVar(hook = nameof(PlayerIdChanged))]
    public string playerId;

    [SyncVar(hook = nameof(PlayerNameChanged))]
    public string playerName;

    [SyncVar(hook = nameof(PlayerScoreGameChanged))]
    public string playerScore;


    private void PlayerIdChanged(string _, string newPlayerId) => OnPlayerIdChanged?.Invoke(newPlayerId);
    private void PlayerNameChanged(string _, string newPlayerName) => OnPlayerNameChanged?.Invoke(newPlayerName);
    private void PlayerScoreGameChanged(string _, string newPlayerScoreGame) => OnPlayerScoreGameChanged?.Invoke(newPlayerScoreGame);

    #endregion


    private void Start()
    {
        if (netIdentity.netId.Equals(1))
            gameObject.SetActive(false);

        if (!isLocalPlayer && isClientOnly)
        {
            gameObject.SetActive(false);
        }
    }

    #region Client
    public override void OnStartClient()
    {
        playerNameLocal = PlayerPrefs.GetString("Player");

        SetPlayer();

        SetDataPlayerToLeadboard();
    }

    private void SetPlayer()
    {
        playerUIObject = Instantiate(playerUIPrefab, AdminUI.GetPlayersPanel());
        playerUI = playerUIObject.GetComponent<PlayerUI>();
        playerId = netIdentity.netId.ToString();
        playerUI.name = playerId;
    }

    private void SetDataPlayerToLeadboard()
    {
        /* CmdSetPlayerId(); */
        CmdSetPlayerNames(playerNameLocal);
        /*  CmdSetPlayerTime(newTimeLocal); */
    }

    /*     public override void OnStopClient()
        {
            OnPlayerNameChanged = null;
            OnPlayerScoreGameChanged = null;

            Destroy(playerUIObject);
        } */

    public void ExecutarComando()
    {
        try
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

            SetNewTimeGame();

        }
        catch (Exception)
        {
            Debug.Log($"Debug Erro ao pegar os dados");
        }
    }

    private void SetPlayerData()
    {
        try
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

            CmdCommandSet(playerDatas);
        }
        catch (Exception)
        {
            Debug.Log($"Debug Erro ao adicionar os dados");
        }

    }

    private void SetNewTimeGame()
    {
        try
        {
            newTimeGame += getTimeGame;

            float minutes = Mathf.FloorToInt(newTimeGame / 60);
            float seconts = Mathf.FloorToInt(newTimeGame % 60);

            newTime = string.Format("{0:00}:{1:00}", minutes, seconts);

            CmdBotaoClicado(newTime);
        }
        catch (Exception)
        {
            Debug.Log($"Debug Erro ao atualizar o tempo do jogador");
        }
    }

    [Command]
    private void CmdBotaoClicado(string newTime)
    {
        try
        {
            var teste = AdminUI.GetPlayersPanel().childCount;

            for (int i = 0; i < teste; i++)
            {
                var teste2 = AdminUI.GetPlayersPanel().GetChild(i).gameObject;

                 if (teste2.name.Equals(playerId))
                 {
                     playerUI.OnTimeGameChanged(newTime);
                 }
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"Erro ao executar o command em {ex.Message}");
        }
    }

    [Command]
    private void CmdCommandSet(List<PlayerDatas> playerDatas) =>
        AdminNetworkManager.instance.SetPlayerData(playerDatas);

    #endregion

    //Id Player
    /* [Command(requiresAuthority = false)]
    private void CmdSetPlayerId(NetworkConnectionToClient sender = null) => playerUI.OnPlayerIdChanged(sender.identity.netId.ToString()); */

    //Name Player
    [Command]
    private void CmdSetPlayerNames(string localPlayerName) => playerUI.OnPlayerNameChanged(localPlayerName);

    /*   [ClientRpc]
      private void RpcSetPlayerName(string localPlayerName) => playerUI.OnPlayerNameChanged(localPlayerName); */

    /*     //Time Player
        [Command]
        private void CmdSetPlayerTime(string newScore) => RpcSetPlayerScore(newScore);

        [ClientRpc]
        private void RpcSetPlayerScore(string newScore) => playerScore = newScore; */

}


