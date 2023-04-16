using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    static string UNKNOWN_PLAYER_NAME = "?Unknown?";

    static GameObject scoreGO;

    void Start()
    {
        scoreGO = GameObject.FindWithTag(TagEnum.SCORE_TAG);
    }

    public void SaveScore()
    {
        Score score = scoreGO.GetComponent<Score>();
        SaveScore(score.GetScoreValue(), GetPlayerName());
    }

    public void SaveScore(long score, string playerName)
    {
        //Read previsous High Scores form Files.
        List<PlayerHighScore> playerHighScores = SaveSystem.readPlayerHighScore();

        PlayerHighScore playerHighScore = new PlayerHighScore(playerName, score);
        playerHighScores.Add(playerHighScore);

        //Put new Score into Highscore List, if it was under the top ten Scores.
        List<PlayerHighScore> topTenScores = new List<PlayerHighScore>();
        playerHighScores.Sort();
        playerHighScores.Reverse();
        for (int i = 0; i < 10 && i < playerHighScores.Count; i++)
        {
            if (playerHighScores[i] != null)
            {
                topTenScores.Add(playerHighScores[i]);
            }
        }
        SaveSystem.writePlayerHighScores(topTenScores);
    }

    string GetPlayerName()
    {
        return PlayerPrefs.GetString(StaticInfomration.PLAYER_PREF_TAG, UNKNOWN_PLAYER_NAME);
    }
}
