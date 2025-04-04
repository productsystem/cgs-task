using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    public Animator transition;

    GameObject levelGrid;

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
        levelGrid = GameObject.Find("LevelGrid");
        levelGrid.SetActive(false);
        
        }
        transition.SetTrigger("Start");
    }
    public void OpenGridSelect()
    {
        GameObject.Find("Exit").SetActive(false);
        GameObject.Find("Start").SetActive(false);
        GameObject.Find("Title").SetActive(false);
        levelGrid.SetActive(true);
    }

    public void StartLevel(int level)
    {
        Time.timeScale = 1;
        StartCoroutine(LoadLevel(level));
    }

    IEnumerator LoadLevel(int level)
    {
        transition.SetTrigger("End");
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + level);
        
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void TogglePause(GameObject pauseMenu)
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }
}
