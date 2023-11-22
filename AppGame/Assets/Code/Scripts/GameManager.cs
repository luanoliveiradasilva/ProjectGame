using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button[] showGame;

    private string nameGame;
    public string NameGame
    {
        get => nameGame;
        set => nameGame = value;
    }

    void Update()
    {
        OnButtonActiveScene();
    }

    public void OnButtonActiveScene()
    {
        for (int index = 0; index < showGame.Length; index++)
        {
            int indexButton = index;
            showGame[index].onClick.AddListener(() => GetLevelName(indexButton));
        }
    }

    private void GetLevelName(int index)
    {  
        nameGame = showGame[index].name;
    }
}
