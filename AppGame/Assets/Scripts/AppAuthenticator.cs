using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Scripts
{

    [AddComponentMenu("")]
    public class AppAuthenticator : NetworkAuthenticator
    {
        readonly HashSet<NetworkConnection> connectionsPendingDisconnect = new HashSet<NetworkConnection>();
        internal static readonly HashSet<string> playerNames = new HashSet<string>();

        [Header("Client Username")]
        public string playerName;

        #region Messages

        public struct AuthRequestMessage : NetworkMessage
        {
            public string authUsername;
        }

        public struct AuthResponseMessage : NetworkMessage
        {
            public byte code;
            public string message;
        }

        #endregion

        #region Server

        // RuntimeInitializeOnLoadMethod -> fast playmode without domain reload
        [UnityEngine.RuntimeInitializeOnLoadMethod]
        static void ResetStatics()
        {
            playerNames.Clear();
        }

        public override void OnStartServer()
        {
            NetworkServer.RegisterHandler<AuthRequestMessage>(OnAuthRequestMessage, false);
        }

        public override void OnStopServer()
        {
            NetworkServer.UnregisterHandler<AuthRequestMessage>();
        }

        public override void OnServerAuthenticate(NetworkConnectionToClient conn)
        {
            // do nothing...wait for AuthRequestMessage from client
        }

        public void OnAuthRequestMessage(NetworkConnectionToClient conn, AuthRequestMessage requestMessage)
        {
            Debug.Log($"Authentication Request: {requestMessage.authUsername}");

            if (connectionsPendingDisconnect.Contains(conn)) return;

            if (!playerNames.Contains(requestMessage.authUsername))
            {

                playerNames.Add(requestMessage.authUsername);

                conn.authenticationData = requestMessage.authUsername;

                // create and send msg to client so it knows to proceed
                AuthResponseMessage authResponseMessage = new AuthResponseMessage
                {
                    code = 100,
                    message = "Success"
                };

                conn.Send(authResponseMessage);

                ServerAccept(conn);
            }
           /*  else
            {
                connectionsPendingDisconnect.Add(conn);

                // create and send msg to client so it knows to disconnect
                AuthResponseMessage authResponseMessage = new AuthResponseMessage
                {
                    code = 200,
                    message = "Username already in use...try again"
                };

                conn.Send(authResponseMessage);

                // must set NetworkConnection isAuthenticated = false
                conn.isAuthenticated = false;

                // disconnect the client after 1 second so that response message gets delivered
                StartCoroutine(DelayedDisconnect(conn, 1f));
            } */
        }

        IEnumerator DelayedDisconnect(NetworkConnectionToClient conn, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            // Reject the unsuccessful authentication
            ServerReject(conn);

            yield return null;

            // remove conn from pending connections
            connectionsPendingDisconnect.Remove(conn);
        }

        #endregion

        #region Client

        public void SetPlayername(string username)
        {
            playerName = username;
/*             LoginUI.instance.errorText.text = string.Empty;
            LoginUI.instance.errorText.gameObject.SetActive(false); */
        }

        public override void OnStartClient()
        {
            NetworkClient.RegisterHandler<AuthResponseMessage>(OnAuthResponseMessage, false);
        }
  
        public override void OnStopClient()
        {
            NetworkClient.UnregisterHandler<AuthResponseMessage>();
        }

        public override void OnClientAuthenticate()
        {
            NetworkClient.Send(new AuthRequestMessage { authUsername = playerName });
        }

        public void OnAuthResponseMessage(AuthResponseMessage msg)
        {
            if (msg.code == 100)
            {
                Debug.Log($"Authentication Response: {msg.code} {msg.message}");

                ClientAccept();
            }
          /*   else
            {
                Debug.LogError($"Authentication Response: {msg.code} {msg.message}");

                // Authentication has been rejected
                // StopHost works for both host client and remote clients
                NetworkManager.singleton.StopHost();

                // Do this AFTER StopHost so it doesn't get cleared / hidden by OnClientDisconnect
                LoginUI.instance.errorText.text = msg.message;
                LoginUI.instance.errorText.gameObject.SetActive(true);
            } */
        }

        #endregion
    }
}
