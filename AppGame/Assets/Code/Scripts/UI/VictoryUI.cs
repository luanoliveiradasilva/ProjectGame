using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private Button button;

    private Player playerTeste;

    private void Start()
    {
        playerTeste = FindObjectOfType<Player>();
        button.onClick.AddListener(ExecutartComando);
    }

    private void ExecutartComando()
    {
        playerTeste.ExecutartComando();
    }
}
