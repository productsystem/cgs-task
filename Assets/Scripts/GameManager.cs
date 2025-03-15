using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool isPaused = false;

    GameObject levelGrid;
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
        levelGrid = GameObject.Find("LevelSelect");
        levelGrid.SetActive(false);
        }
    }
    public int level = 0;
    public void OpenGridSelect()
    {
        Debug.Log("grid open");
        GameObject.Find("Exit").SetActive(false);
        GameObject.Find("Start").SetActive(false);
        GameObject.Find("Title").SetActive(false);
        levelGrid.SetActive(true);
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + level);
    } 

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void PauseMenuOpen(GameObject pauseMenu)
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void PauseMenuClose(GameObject pauseMenu)
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
