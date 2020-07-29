using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject MainMenu, Levels;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ShowLevels()
    {
        MainMenu.SetActive(false);
        Levels.SetActive(true);
    }
    public void BackFromLevels()
    {
        MainMenu.SetActive(true);
        Levels.SetActive(false);
    }
}
