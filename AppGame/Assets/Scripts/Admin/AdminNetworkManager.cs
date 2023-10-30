using Mirror;
using UnityEngine;

[AddComponentMenu("")]
public class AdminNetworkManager : NetworkManager
{
    public static AdminNetworkManager Singleton;

    public override void Awake()
    {
        base.Awake();
        Singleton = this;
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
