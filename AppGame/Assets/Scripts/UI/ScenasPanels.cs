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

        //TODO adicionar condição para adicinar todos os botões automaticos.

        void Update()
        {
            OnButtonActiveScene();
        }

        public void OnButtonActiveScene()
        {
            for (int index = 0; index < showGame.Length; index++)
            {
                int indiceBotao = index;
                showGame[index].onClick.AddListener(() => GetSceneGame(indiceBotao));
            }
        }

        private void GetSceneGame(int index)
        {
            var selectedScene = showGame.Select((Value, Index) => new { Value, indexGame=Index }).FirstOrDefault(item => item.indexGame == index);

            nameGame = selectedScene?.Value.ToString();
        }
    }
}