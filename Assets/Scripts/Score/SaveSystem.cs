using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using MessagePack;

public static class SaveSystem
{
    static Encoding ENCODING = Encoding.ASCII;  
    static string FILE_NAME = "Highscores.bin";  

    public static void writePlayerHighScores(List<PlayerHighScore> playerHighScores)
    { 
        string filePath = Application.dataPath + "/" + FILE_NAME;

        try  
        {  
            Debug.Log("Write to file: " + filePath);

            using (BinaryWriter binWriter = new BinaryWriter(new FileStream(FILE_NAME, FileMode.Create), ENCODING))  
            {
                foreach (PlayerHighScore playerHighScore in playerHighScores)
                {
                    byte[] bytes = MessagePackSerializer.Serialize(playerHighScore);
                    binWriter.Write(bytes);
                }
            }                
        }  
        catch (IOException ioexp)  
        {  
            Debug.Log("Error: " + ioexp.Message);
        }  
    }

    public static List<PlayerHighScore> readPlayerHighScore()
    {
        List<PlayerHighScore> playerHighScores = new List<PlayerHighScore>();
        // string filePath = Application.dataPath + "/" + FILE_NAME;

        string filePath = "Highscores.bin";

        if (File.Exists(filePath))
        {
            using (FileStream stream = File.Open(filePath, FileMode.Open))
            {
                while (stream.Position < stream.Length)
                {
                    PlayerHighScore playerHighScore = MessagePackSerializer.Deserialize<PlayerHighScore>(stream);

                    Debug.Log("Read Player Name: " + playerHighScore.PlayerName);
                    Debug.Log("Read Score: " + playerHighScore.Score);

                    playerHighScores.Add(playerHighScore);
                }
            }
        }

        return playerHighScores;
    }
}
