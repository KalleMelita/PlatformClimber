using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HighScoreManagerTest
{

    List<PlayerHighScore> playerHighSoresFive = new List<PlayerHighScore>();
    List<PlayerHighScore> playerHighSoresTen = new List<PlayerHighScore>();

    static long TEST_SCORE_ONE = 400;
    static long TEST_SCORE_TWO = 0;

    static string TEST_PLAYER_NAME = "Test";

    static bool isSetUp = false;

    [SetUp]
    public void SetUp()
    {
        if(isSetUp){
            return;
        }

        playerHighSoresFive.Add(new PlayerHighScore(HighScoreTestData.PLAYERA, HighScoreTestData.SCOREA));
        playerHighSoresFive.Add(new PlayerHighScore(HighScoreTestData.PLAYERB, HighScoreTestData.SCOREB));
        playerHighSoresFive.Add(new PlayerHighScore(HighScoreTestData.PLAYERC, HighScoreTestData.SCOREC));
        playerHighSoresFive.Add(new PlayerHighScore(HighScoreTestData.PLAYERD, HighScoreTestData.SCORED));
        playerHighSoresFive.Add(new PlayerHighScore(HighScoreTestData.PLAYERE, HighScoreTestData.SCOREE));

        playerHighSoresTen.Add(new PlayerHighScore(HighScoreTestData.PLAYERA, HighScoreTestData.SCOREA));
        playerHighSoresTen.Add(new PlayerHighScore(HighScoreTestData.PLAYERB, HighScoreTestData.SCOREB));
        playerHighSoresTen.Add(new PlayerHighScore(HighScoreTestData.PLAYERC, HighScoreTestData.SCOREC));
        playerHighSoresTen.Add(new PlayerHighScore(HighScoreTestData.PLAYERD, HighScoreTestData.SCORED));
        playerHighSoresTen.Add(new PlayerHighScore(HighScoreTestData.PLAYERE, HighScoreTestData.SCOREE));
        playerHighSoresTen.Add(new PlayerHighScore(HighScoreTestData.PLAYERF, HighScoreTestData.SCOREF));
        playerHighSoresTen.Add(new PlayerHighScore(HighScoreTestData.PLAYERG, HighScoreTestData.SCOREG));
        playerHighSoresTen.Add(new PlayerHighScore(HighScoreTestData.PLAYERH, HighScoreTestData.SCOREH));
        playerHighSoresTen.Add(new PlayerHighScore(HighScoreTestData.PLAYERI, HighScoreTestData.SCOREI));
        playerHighSoresTen.Add(new PlayerHighScore(HighScoreTestData.PLAYERJ, HighScoreTestData.SCOREJ));

        isSetUp = true;
    }

    [Test]
    public void HighScoreManagerSaveScoreFive()
    {
        SaveSystem.writePlayerHighScores(playerHighSoresFive);
        GameObject highScoreManagerGO = new GameObject();
        HighScoreManager highScoreManager = highScoreManagerGO.AddComponent<HighScoreManager>();
        PlayerHighScore newScore = new PlayerHighScore(TEST_PLAYER_NAME, TEST_SCORE_ONE);
        
        highScoreManager.SaveScore(TEST_SCORE_ONE, TEST_PLAYER_NAME);
        
        List<PlayerHighScore> savedScores = SaveSystem.readPlayerHighScore();
        Assert.AreEqual(6, savedScores.Count);
        Assert.IsTrue(savedScores.FindAll(savedScore => savedScore.Equals(newScore)).Count == 1);
    }

    [Test]
    public void HighScoreManagerSaveScoreTen()
    {
        SaveSystem.writePlayerHighScores(playerHighSoresTen);
        GameObject highScoreManagerGO = new GameObject();
        HighScoreManager highScoreManager = highScoreManagerGO.AddComponent<HighScoreManager>();
        PlayerHighScore newScore = new PlayerHighScore(TEST_PLAYER_NAME, TEST_SCORE_ONE);

        highScoreManager.SaveScore(TEST_SCORE_ONE, TEST_PLAYER_NAME);

        List<PlayerHighScore> savedScores = SaveSystem.readPlayerHighScore();
        Assert.AreEqual(10, savedScores.Count);
        Assert.IsTrue(savedScores.FindAll(savedScore => savedScore.Equals(newScore)).Count == 1);
    }

    [Test]
    public void HighScoreManagerDontSaveScoreTen()
    {
        SaveSystem.writePlayerHighScores(playerHighSoresTen);
        GameObject highScoreManagerGO = new GameObject();
        HighScoreManager highScoreManager = highScoreManagerGO.AddComponent<HighScoreManager>();
        PlayerHighScore newScore = new PlayerHighScore(TEST_PLAYER_NAME, TEST_SCORE_TWO);

        highScoreManager.SaveScore(TEST_SCORE_TWO, TEST_PLAYER_NAME);
        
        List<PlayerHighScore> savedScores = SaveSystem.readPlayerHighScore();
        Assert.AreEqual(10, savedScores.Count);
        Assert.IsTrue(savedScores.FindAll(savedScore => savedScore.Equals(newScore)).Count <= 0);
    }
}

