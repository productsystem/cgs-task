using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public CanvasGroup fadePanel;
    public float fadeTime = 1f;
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

    IEnumerator FadeIn()
    {
        float t = fadeTime;
        while(t>0)
        {
            t-=Time.deltaTime;
            fadePanel.alpha = t/fadeTime;
            yield return null;
        }
        fadePanel.alpha = 0;
    }

    IEnumerator FadeOut(int level)
    {
        float t = 0;
        while(t<fadeTime)
        {
            t+= Time.deltaTime;
            fadePanel.alpha = t/fadeTime;
            yield return null;
        }
        fadePanel.alpha = 1;
        SceneManager.LoadScene(level);
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
