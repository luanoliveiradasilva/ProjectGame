using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private Button button;

    private Player playerTeste;

    private void Awake()
    {
        try
        {
            bool isExistPlayer = AdminNetworkManager.instance.SetServerPlayer();

            if (isExistPlayer)
            {
                playerTeste = FindObjectOfType<Player>();
                button.onClick.AddListener(ExecutartComando);
                Debug.Log($"Victory!");
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log($"Defeat! {ex.Message}");
        }
    }

    private void ExecutartComando()
    {
        playerTeste.ExecutartComando();
    }
}
