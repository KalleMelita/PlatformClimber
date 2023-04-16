using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

    public class SaveSystemTest
    {

        List<PlayerHighScore> playerHighSores = new List<PlayerHighScore>();

        [SetUp]
        public void SetUp()
        {
            playerHighSores.Add(new PlayerHighScore(HighScoreTestData.PLAYERA, HighScoreTestData.SCOREA));
            playerHighSores.Add(new PlayerHighScore(HighScoreTestData.PLAYERB, HighScoreTestData.SCOREB));
            playerHighSores.Add(new PlayerHighScore(HighScoreTestData.PLAYERC, HighScoreTestData.SCOREC));
            playerHighSores.Add(new PlayerHighScore(HighScoreTestData.PLAYERD, HighScoreTestData.SCORED));
            playerHighSores.Add(new PlayerHighScore(HighScoreTestData.PLAYERE, HighScoreTestData.SCOREE));
        }

        [Test]
        public void SaveAndReadPlayerHighScores()
        {
            SaveSystem.writePlayerHighScores(playerHighSores);
            List<PlayerHighScore> readPlayerHighSores = SaveSystem.readPlayerHighScore();

            CollectionAssert.AreEqual(playerHighSores, readPlayerHighSores);
        }
    }
