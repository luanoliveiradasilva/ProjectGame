using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace UI
{
    public class ScenasPanels : MonoBehaviour
    {

        [Header("Buttons")]
        [SerializeField] private Button[] showGame;

        public string nameGame;

        public static ScenasPanels instance;

        //TODO adicionar condição para adicinar todos os botões automaticos.
        void Awake() => instance = this;

        void Update()
        {
            OnButtonActiveScene();
        }

        public void OnButtonActiveScene()
        {
            for (int indice = 0; indice < showGame.Length; indice++)
            {
                int indiceBotao = indice;
                showGame[indice].onClick.AddListener(() => SelectSceneGame(indiceBotao));
            }
        }

        private void SelectSceneGame(int indice)
        {
            var selectedScene = showGame.Select((Value, indice) => new { Value, indice }).FirstOrDefault(item => item.indice == indice);

            nameGame = selectedScene?.Value.ToString();
        }
    }
}