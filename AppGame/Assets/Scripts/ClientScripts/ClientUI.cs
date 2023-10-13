using Mirror;
using TMPro;
using UnityEngine;

public class ClientUI : MonoBehaviour
{

    public TMP_InputField ipAddressInputField;

    public TMP_InputField clientNameInputField;

    Player player;

    private void Start()
    {
        //player = GameObject.FindWithTag("Player").GetComponent<Player>();
        //SetNamePlayer();
    }

    public void SetNamePlayer()
    {
       // player.localNamePlayer = clientNameInputField.text;
    }

}
