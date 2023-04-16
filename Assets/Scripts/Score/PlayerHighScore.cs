using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MessagePack;

[MessagePackObject]
public class PlayerHighScore : IComparable<PlayerHighScore>
{
    [Key(0)]
    public string PlayerName {get; set;}

    [Key(1)]
    public long Score {get; set;}

    public PlayerHighScore(string name, long score)
    {
        PlayerName = name;
        Score = score;
    }

    public int CompareTo(PlayerHighScore other)
    {
        return this.Score.CompareTo(other.Score);
    }


    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        PlayerHighScore p = obj as PlayerHighScore;
        if ((System.Object)p == null)
        {
            return false;
        }

        return (PlayerName.Equals(p.PlayerName) && Score.Equals(p.Score));
    }

    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            int hash = 17;

            hash = hash * 23 + PlayerName.GetHashCode();
            hash = hash * 23 + Score.GetHashCode();
            return hash;
        }
    }
}
