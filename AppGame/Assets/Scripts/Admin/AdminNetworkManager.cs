using Mirror;
using Scripts.Game;
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
    }

    public void SetIpAddress(string ipAddress)
    {
        networkAddress = ipAddress;
    }
}
