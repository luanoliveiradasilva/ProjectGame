using UnityEngine;

namespace Scripts.UI
{
    public class ActiveLevel : MonoBehaviour
    {
        [Header("Games to select")]
        [SerializeField] private GameObject[] sceneLevel;

        private GameManager scenes;

        private readonly string nameLevel = "Level";

        private void Awake()
        {
            scenes = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            SelectLevelGame();
        }

        public void SelectLevelGame()
        {
            foreach (var item in sceneLevel)
            {
                bool isLevel = scenes.NameGame == item.name;

                if (isLevel)
                {
                    PlayerPrefs.SetString(nameLevel, item.name);
                    item.SetActive(true);
                }
            }
        }
    }
}
