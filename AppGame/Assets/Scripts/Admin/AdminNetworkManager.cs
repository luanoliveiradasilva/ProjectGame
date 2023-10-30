using Mirror;
using UnityEngine;

[AddComponentMenu("")]
public class AdminNetworkManager : NetworkManager
{
    /* public static AdminNetworkManager current; */
    /*     public class PlayerData
        {
            public string displayName = "no name";
            public int score = 0;
        } */

    /*  public Dictionary<uint, PlayerData> playerData = new(); */

    public static new AdminNetworkManager singleton { get; private set; }

    public override void Awake()
    {
        //current = this;

        base.Awake();
        singleton = this;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        /*   playerData.Add(conn.identity.netId, new PlayerData()); */
        base.OnServerAddPlayer(conn);
    }

    public void SetIpAddress(string ipAddress)
    {
        networkAddress = ipAddress;
    }
}
