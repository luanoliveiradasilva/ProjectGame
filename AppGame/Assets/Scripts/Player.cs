using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;


namespace Scripts
{
    public class Player : NetworkBehaviour
    {
        [SyncVar]
        public string playerName;

        public override void OnStartServer()
        {
            playerName = (string)connectionToClient.authenticationData;
        }

        public override void OnStartLocalPlayer()
        {
            LeadboardUI.localPlayerName = playerName;
        }
    }
}
