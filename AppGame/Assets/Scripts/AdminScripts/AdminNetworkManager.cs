using Mirror;
using UnityEngine;


[AddComponentMenu("")]
public class AdminNetworkManager : NetworkManager
{
    public static new AdminNetworkManager singleton { get; private set; }

    public override void Awake()
    {
        base.Awake();
        singleton = this;
    }


    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        Player.ResetPlayerNumbers();
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        Player.ResetPlayerNumbers();
    }

    public void SetIpAddress(string ipAddress)
    {
        networkAddress = ipAddress;
    }
}
