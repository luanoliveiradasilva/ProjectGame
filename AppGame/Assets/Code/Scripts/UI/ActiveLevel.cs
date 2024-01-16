using UnityEngine;

namespace Scripts.UI
{
    public class ActiveLevel : MonoBehaviour
    {
        private readonly string nameLevel = "Level";

        public void SelectLevelGame(string nameLevelActive)
        {
            PlayerPrefs.SetString(nameLevel, nameLevelActive);
        }
    }
}
