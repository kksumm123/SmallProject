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
                case GameStateType.Ready:
                case GameStateType.Menu:
                case GameStateType.GameOver:
                    Time.timeScale = 0;
                    break;
            }
            print($"GameState : {m_GameState} -> {value}, TimeScale{Time.timeScale}");
            m_GameState = value;
        }
    }

    [SerializeField] float readyTime = 3;
    IEnumerator Start()
    {
        GameState = GameStateType.Ready;
        yield return new WaitForSeconds(readyTime);
        GameState = GameStateType.Playing;
    }
}
