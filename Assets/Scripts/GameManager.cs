using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStateType
{
    None,
    Ready,
    Playing,
    Menu,
    GameOver,
    StageClear,
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake() => Instance = this;

    GameStateType m_GameState;
    public GameStateType GameState
    {
        get => m_GameState;
        set
        {
            if (m_GameState == value)
                return;

            switch (value)
            {
                case GameStateType.Ready:
                case GameStateType.Playing:
                    Time.timeScale = 1;
                    break;
                case GameStateType.Menu:
                case GameStateType.GameOver:
                case GameStateType.StageClear:
                    Time.timeScale = 0;
                    break;
            }
            print($"GameState : {m_GameState} -> {value}, TimeScale : {Time.timeScale}");
            m_GameState = value;
        }
    }


    [SerializeField] int readyTime = 3;
    IEnumerator Start()
    {
        GameState = GameStateType.Ready;
        ReadyUI.Instance.SetReady(readyTime);
        yield return new WaitForSeconds(readyTime);
        GameState = GameStateType.Playing;
    }

    void Update()
    {
        if (GameState == GameStateType.GameOver)
            return;

        IsGameOver();
    }

    float gameOverDistance = 20;
    void IsGameOver()
    {
        // ���� ���� ����
        // 1. ĳ���Ͱ� �־�����
        if (Vector2.Distance(Camera.main.transform.position
            , Player.Instance.transform.position) > gameOverDistance)
        {
            GameState = GameStateType.GameOver;
            GameOverAndClearUI.Instance.ShowUI(GameStateType.GameOver);
        }
    }

    internal void StageEnd()
    {
        GameState = GameStateType.StageClear;
        GameOverAndClearUI.Instance.ShowUI(GameStateType.StageClear);
    }

    internal void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void AddScore(int value)
    {
        ScoreUI.Instance.AddScore(value);
    }
}
