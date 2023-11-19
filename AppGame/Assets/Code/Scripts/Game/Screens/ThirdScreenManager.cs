using UnityEngine;

public class ThirdScreenManager : MonoBehaviour
{

    [SerializeField] private int correctObject;
    [SerializeField] private int incorrectObject;

    private readonly int quantityProducts = 4;

    public void ValuesCorrect(bool correct)
    {
        if (correct)
        {
            correctObject++;
            SetPlayerPrefs(correctObject);
        }
        else
        {
            incorrectObject++;
        }
    }

    private void SetPlayerPrefs(int correctObject)
    {
        if (correctObject.Equals(quantityProducts))
        {
            PlayerPrefs.SetInt("Right", correctObject);
            PlayerPrefs.SetInt("Wrong", incorrectObject);
        }
    }
}
