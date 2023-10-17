using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdminUI : MonoBehaviour
{
    [Tooltip("Assign Main Panel so it can be turned on from Player:OnStartClient")]
    public RectTransform mainPanel;

    [Tooltip("Assign Players Panel for instantiating PlayerUI as child")]
    public RectTransform playersPanel;

    [Header("UI Elements")]
    [SerializeField] internal TMP_InputField namesPlayer;
    [SerializeField] internal Button buttonsStartClient;

    static AdminUI instance;

    void Awake()
    {
        instance = this;
    }

    public static void SetActive(bool active)
    {
        instance.mainPanel.gameObject.SetActive(active);
    }

    public static RectTransform GetPlayersPanel() => instance.playersPanel;

    public void ToggleButtons(string name)
    {
        buttonsStartClient.interactable = !string.IsNullOrWhiteSpace(name);
    }
}
