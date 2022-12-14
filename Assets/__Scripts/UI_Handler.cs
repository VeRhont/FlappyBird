using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Handler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _highScoreText;

    private void Awake()
    {
        _highScoreText.SetText($"High Score: {PlayerPrefs.GetInt("HighScore")}");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
