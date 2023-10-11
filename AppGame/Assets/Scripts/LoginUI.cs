using UnityEngine;
using UnityEngine.UI;


namespace Scripts
{
    public class LoginUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] internal InputField usernameInput;

        [SerializeField] internal Button startHostButton;
        [SerializeField] internal Button startClientButton;

        public static LoginUI instance;

        private void Awake()
        {
            instance = this;
        }

        //TODO Habilitar o botao quando o campo for preenchido. !
        public void ToggleButtons(string username)
        {
            startHostButton.interactable = !string.IsNullOrWhiteSpace(username);
            startClientButton.interactable = !string.IsNullOrWhiteSpace(username);
        }
    }
}
