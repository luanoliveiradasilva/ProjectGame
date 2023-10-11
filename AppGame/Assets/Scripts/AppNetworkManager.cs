using Mirror;
using UnityEngine;

namespace Scripts
{
    [AddComponentMenu("")]
    public class AppNetworkManager : NetworkManager
    {

        public static new AppNetworkManager singleton { get; private set; }

        public override void Awake()
        {
            base.Awake();
            singleton = this;
        }

        // Called by UI element NetworkAddressInput.OnValueChanged
        public void SetHostname(string hostname)
        {
            networkAddress = hostname;
        }
    }
}
