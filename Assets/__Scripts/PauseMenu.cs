using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool IsGamePaused { get; private set; }

    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _pauseMenu;

    private bool _isGamePaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    { 
        _isGamePaused = true;

        Time.timeScale = 0;

        _ui.SetActive(false);
        _pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        _isGamePaused = false;

        Time.timeScale = 1;

        _ui.SetActive(true);
        _pauseMenu.SetActive(false);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}