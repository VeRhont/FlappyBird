using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Handler : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
