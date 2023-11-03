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

    private static AdminUI instance;

    private void Awake() => instance = this;
    
    public static RectTransform GetPlayersPanel() => instance.playersPanel;

}
