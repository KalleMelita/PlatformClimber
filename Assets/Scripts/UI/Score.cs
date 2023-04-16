using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    /// <summary>
    /// Character Game Object.
    /// </summary>
    private GameObject character;

    /// <summary>
    /// Character Score.
    /// </summary>
    float score = 0;

    public long GetScoreValue()
    {
        return (long)score;
    }

    /// <summary>
    /// Connected Text Object.
    /// </summary>
    [SerializeField] Text textUI;

    void Start()
    {
        character = GameObject.FindWithTag(TagEnum.Player);
        score = 0;
    }

    void Update()
    {
        float characterHeight = character.transform.position.y;

        if (characterHeight > score)
        {
            score = characterHeight;
            textUI.text = "Score: " + ((int)score).ToString();
        }
    }
}
