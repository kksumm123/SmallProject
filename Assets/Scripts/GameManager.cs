using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake() => Instance = this;

    public enum GameStateType
    {
        None,
        Ready,
        Playing,
        Menu,
        GameOver,
    }
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
                case GameStateType.Playing:
                    Time.timeScale = 1;
                    break;
                case GameStateType.Menu:
                case GameStateType.GameOver:
                    Time.timeScale = 0;
                    break;
            }
            print($"GameState : {m_GameState} -> {value}, TimeScale{Time.timeScale}");
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
        IsGameOver();
    }

    float gameOverDistance = 20;
    void IsGameOver()
    {
        // 게임 오버 조건
        // 1. 캐릭터가 멀어지면
        //if (Vector2.Distance(Camera.main.transform.position
        //    , Player.Instance.transform.position) > gameOverDistance)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameState = GameStateType.GameOver;
            GameOverUI.Instance.ShowUI();
        }
    }

    public void AddScore(int value)
    {
        ScoreUI.Instance.AddScore(value);
    }
}
