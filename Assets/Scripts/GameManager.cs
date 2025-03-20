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
    public float fadeTime = 1f;
    public bool isPaused = false;
    public Animator transition;

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
        StartCoroutine(LoadLevel(level));
    }

    IEnumerator LoadLevel(int level)
    {
        transition.SetTrigger("End");
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + level);
        transition.SetTrigger("Start");
        
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
        SceneManager.LoadSceneAsync(0);
    }
}
