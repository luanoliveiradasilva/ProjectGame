using Mirror;
using UnityEngine;

public class PlayerTeste : NetworkBehaviour
{
    public override void OnStartClient()
    {
        Debug.Log("Player inciiado com sucesso!");
    }

    public override void OnStartLocalPlayer()
    {
        Debug.Log("Player logadado com sucesso!");
    }
}
