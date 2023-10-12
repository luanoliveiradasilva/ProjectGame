using TMPro;
using UnityEngine;

public class ClientUI : MonoBehaviour
{


    public TMP_InputField ipAddressInputField;

    public TMP_InputField clientNameInputField;

    // static instance that can be referenced from static methods below.
    static ClientUI instance;

    Player player =  new Player();

    void Awake()
    {
        instance = this;
    }

    public void SetNamePlayer()
    {
        player.playerNameClient = clientNameInputField.text;
    }

}
