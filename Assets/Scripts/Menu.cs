using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
    }
    public void RestartLevel(int levelindex)
    {
        SceneManager.LoadScene(levelindex);
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame(int levelindex)
    {
        SceneManager.LoadScene(levelindex);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
