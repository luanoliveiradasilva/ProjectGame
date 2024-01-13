using UnityEngine;
using TMPro;

namespace Scripts.Player
{
    public class PlayerUI : MonoBehaviour
    {
        [Header("Child Text Objects")]
        [Tooltip("Set name player text in box text")]
        [SerializeField] private TextMeshProUGUI playerNameText;

        [Tooltip("Set score time player text in box text")]
        [SerializeField] private TextMeshProUGUI timeGameText;

        public void OnPlayerIdChanged(string idPlayer) => gameObject.name = idPlayer;
        public void OnPlayerNameChanged(string namePlayer) => playerNameText.text = namePlayer;
        public void OnTimeGameChanged(string timeGame) => timeGameText.text = timeGame;
    }
}