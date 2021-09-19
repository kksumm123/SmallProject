using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI Instance;
    void Awake() => Instance = this;

    Text scoreValue;
    Text highScoreValue;
    int score;
    int highScore;
    void Start()
    {
        scoreValue = transform.Find("Current/Value").GetComponent<Text>();
        highScoreValue = transform.Find("High/Value").GetComponent<Text>();

        scoreValue.text = score.ToString();
        highScoreValue.text = highScore.ToString();
    }

    internal void AddScore(int value)
    {
        score += value;
        scoreValue.text = score.ToString();
        Debug.LogWarning("DOTween.To() 증가하는 애니메이션 하도록");

        if (highScore < score)
        {
            highScore = score;
            highScoreValue.text = highScore.ToString();
        }
    }
}
