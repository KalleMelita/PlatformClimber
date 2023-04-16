using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Observes the Game and Checks if the Character is beneath a critical minimum height. 
/// </summary>
public class GameOverManager : MonoBehaviour
{
    /// <summary>
    /// <para>If the Characters Vertical Position is lower then the Cameras Vertical Position minus the Value of this Constant the Game must end.</para>
    /// </summary>
    private static readonly uint HEIGHT_GAME_OVER = 10;

    /// <summary>
    /// Character Game Object.
    /// </summary>
    private GameObject character;

    /// <summary>
    /// Camera Game Object.
    /// </summary>
    private GameObject mainCamera;

    /// <summary>
    /// High Score Manager Game Object.
    /// </summary>
    private GameObject highScoreManagerGO;

    /// <summary>
    /// Find Character on Start with tag.
    /// </summary>
    void Start()
    {
        character = GameObject.FindWithTag(TagEnum.Player);
        mainCamera = GameObject.FindWithTag(TagEnum.Camera);
        highScoreManagerGO = GameObject.FindWithTag(TagEnum.HIGH_SCORE_MANAGER_TAG);
    }

    /// <summary>
    /// Observes the Game and Checks if the Character is beneath a critical minimum height (<see cref="HEIGHT_GAME_OVER"/>). 
    /// If the character is under this height, ends the Game.
    /// </summary>
    void Update()
    {
        float cameraHeight = mainCamera.transform.position.y;
        float characterHeight = character.transform.position.y;
        float gameOverHeight = cameraHeight - HEIGHT_GAME_OVER;

        //Debug.Log(System.String.Format("Character Height:{0}  Camera Heigth:{1}!", characterHeight, cameraHeight));
        //Debug.Log(System.String.Format("Character Height:{0} computed Game Over Height:{1}!", characterHeight, gameOverHeight));

        if (characterHeight < gameOverHeight)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log(System.String.Format("GAME OVER"));

        HighScoreManager highScoreManager = highScoreManagerGO.GetComponent<HighScoreManager>();
        highScoreManager.SaveScore();
        SceneManager.LoadScene(StaticInfomration.SCENE_MENU);
    }
}
