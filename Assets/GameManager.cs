using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject levelGrid;
    void Start()
    {
        levelGrid = GameObject.Find("LevelSelect");
        levelGrid.SetActive(false);
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

    public void PauseMenuOpen()
    {

    }

    public void PauseMenuClose()
    {
        
    }
}
