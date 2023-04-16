using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Scripts for a High Score Menue Manager object.
/// </summary>
public class HighScoreMenuManager : MonoBehaviour
{
    /// <summary>
    /// Vertiacal Layout Group  that contains UI-Elemnts to display High Scores.
    /// </summary>
    public GameObject VerticalLayoutGroupGO;

    /// <summary>
    /// Clear all High Scores in the UI on Start.
    /// </summary>
    void Start()
    {
        Clear();
    }

    /// <summary>
    /// Builds the High Score.
    /// Gets all High Scores form the Save System.
    /// Sorts the High Scores an runs throw the 10 best scores.
    /// Finds the 10 Layout Groups to display the High Scores.
    /// The Layout groups need to be names after the pattern 'LayoutGroup (<Score Ranking>)'.
    /// So the Layhout group for Score 5 must be named 'LayoutGroup (5)'.
    /// </summary>
    public void BuildHighScore()
    {
        List<PlayerHighScore> playerHighScores = SaveSystem.readPlayerHighScore();
        playerHighScores.Sort();
        playerHighScores.Reverse();

        for (int i = 1; i < 11 && i < playerHighScores.Count; i++)
        {
            Transform layoutGroupTransform = VerticalLayoutGroupGO.transform.Find(string.Format("LayoutGroup ({0})", i));

            Transform playerNameT = layoutGroupTransform.transform.Find("PlayerName");
            TextMeshProUGUI playerText = playerNameT.GetComponent<TextMeshProUGUI>();
            playerText.text = playerHighScores[i].PlayerName;

            Transform scoreT = layoutGroupTransform.transform.Find("Score");
            TextMeshProUGUI scoreText = scoreT.GetComponent<TextMeshProUGUI>();
            scoreText.text = (playerHighScores[i].Score).ToString();
        }
    }

    /// <summary>
    /// Destroy/ clear the High Score.
    /// All Score displaying Elements will be resettet.
    /// </summary>
    public void DestroyHighScore()
    {
        Clear();
    }

    /// <summary>
    /// Runs through all displaying Elements and resets them.
    /// </summary>
    private void Clear()
    {
        for (int i = 1; i < 11; i++)
        {
            Transform layoutGroupTransform = VerticalLayoutGroupGO.transform.Find(string.Format("LayoutGroup ({0})", i));

            Transform playerNameT = layoutGroupTransform.transform.Find("PlayerName");
            TextMeshProUGUI playerText = playerNameT.GetComponent<TextMeshProUGUI>();
            playerText.text = "";

            Transform scoreT = layoutGroupTransform.transform.Find("Score");
            TextMeshProUGUI scoreText = scoreT.GetComponent<TextMeshProUGUI>();
            scoreText.text = "";
        }
    }
}
