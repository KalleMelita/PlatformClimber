using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        string sceneName = StaticInfomration.SCENE_PLAY;
        Debug.Log(System.String.Format("Load Scene: {0}!", sceneName));
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame(){
        Debug.Log(System.String.Format("Quit Game"));
        Application.Quit();
    }
}
