using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreMenuManager : MonoBehaviour
{
    Canvas canvas;
    public GameObject VerticalLayoutGroupGO;
    

    void Start()
    {
        canvas = GameObject.FindWithTag(TagEnum.CANVAS).GetComponent<Canvas>();
        Clear();
    }

    public void BuildHighScore()
    {
        List<PlayerHighScore> playerHighScores = SaveSystem.readPlayerHighScore();
        playerHighScores.Sort();
        playerHighScores.Reverse();

        float height = 100;

        float scale = canvas.scaleFactor;

        for (int i = 1; i < 11 && i < playerHighScores.Count; i++)
        {
            Transform layoutGroupTransform = VerticalLayoutGroupGO.transform.Find(string.Format("LayoutGroup ({0})",i));

            Transform playerNameT = layoutGroupTransform.transform.Find("PlayerName");
            TextMeshProUGUI playerText = playerNameT.GetComponent<TextMeshProUGUI>();
            playerText.text = playerHighScores[i].PlayerName;

            Transform scoreT = layoutGroupTransform.transform.Find("Score");
            TextMeshProUGUI scoreText = scoreT.GetComponent<TextMeshProUGUI>();
            scoreText.text = (playerHighScores[i].Score).ToString();
        }
    }

    public void DestroyHighScore()
    {
        Clear();
    }

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
