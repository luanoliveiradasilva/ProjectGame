using System.Text.RegularExpressions;
using UI;
using UnityEngine;

public class ActiveGameScene : MonoBehaviour
{
    [Header("Games to select")]
    [SerializeField] private GameObject[] sceneGames;

    [SerializeField] private ScenasPanels scenes;

    // Start is called before the first frame update
    void Start()
    {
        scenes = FindObjectOfType<ScenasPanels>();
    }

    // Update is called once per frame
    void Update()
    {
        RegexName();
        SelectSceneGame();
    }

    private string RegexName()
    {
        var spaceIndex = scenes.nameGame;

        var regexValuesName = @"(\bGame\s\d\b)";

        Regex regex = new(regexValuesName);

        Match match = regex.Match(spaceIndex);

        return match.Value;
    }

    private void SelectSceneGame()
    {
        foreach (var item in sceneGames)
        {
            bool nameGame = RegexName() == item.name;

            if (nameGame)
            {
                PlayerPrefs.SetString("NameGame", item.name);
                item.SetActive(true);
            }

        }
    }
}
