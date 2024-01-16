using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Name Game selected")]
    [Tooltip("Indicates the name of the game that was selected.")]
    [SerializeField] private string nameGame;

    [Header("Buttons Select game")]
    [Tooltip("Buttons to select the level you want.")]
    [SerializeField] private Button[] showGame;

    [SerializeField] private GameObject levelDesative;

    public bool isReload;
    public string NameGame
    {
        get => nameGame;
        set => nameGame = value;
    }

    private void Awake() => Instance = this;

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
        if (nameGame != "Level 1")
        {
            levelDesative.SetActive(false);
        }
    }

    void OnDestroy()
    {
       DOTween.KillAll(); 
    }
    
    public void ReloadScenes()
    {
        SceneManager.LoadScene(1);
    }

    public void LadoScene()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
    }
}
