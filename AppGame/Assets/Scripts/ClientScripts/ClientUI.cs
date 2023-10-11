using TMPro;
using UnityEngine;

public class ClientUI : MonoBehaviour
{


    public TMP_InputField ipAddressInputField;

    public TMP_InputField clientNameInputField;

    // static instance that can be referenced from static methods below.
    static ClientUI instance;

    void Awake()
    {
        instance = this;
    }

}
