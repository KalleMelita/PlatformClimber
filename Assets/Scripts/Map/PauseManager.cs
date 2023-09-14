using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the Pause Menue during the Game.
/// </summary>
public class PauseManager : MonoBehaviour
{ 
    //TODO: probably use Stateless

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused){
                Resume();
            } else {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = StaticInfomration.GAME_TIME_SPEED;
        GameIsPaused = false;
    }

    public void Exit()
    {
        string sceneName = StaticInfomration.SCENE_MENU;
        Debug.Log(System.String.Format("Load Scene: {0}!", sceneName));
        SceneManager.LoadScene(sceneName);
    }
}
