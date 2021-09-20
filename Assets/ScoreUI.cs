using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI Instance;

    Text scoreValue;
    Text highScoreValue;
    int score;
    int highScore;
    void Awake()
    {
        Instance = this;

        scoreValue = transform.Find("Current/Value").GetComponent<Text>();
        highScoreValue = transform.Find("High/Value").GetComponent<Text>();

        scoreValue.text = score.ToString();
        highScoreValue.text = highScore.ToString();
    }

    int oldScore;
    float scoreAnimationTime = 0.2f;
    DG.Tweening.Core.TweenerCore<int, int, DG.Tweening.Plugins.Options.NoOptions> handle;
    internal void AddScore(int value)
    {
        oldScore = score;
        score += value;

        if (handle != null)
            handle.Kill();
        handle = DOTween.To(() => oldScore, x => scoreValue.text = x.ToString(), score, scoreAnimationTime)
            .SetUpdate(true).SetLink(gameObject);

        if (highScore < score)
        {
            highScore = score;
            highScoreValue.text = highScore.ToString();
        }
    }
}
