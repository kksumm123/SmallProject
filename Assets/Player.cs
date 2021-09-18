using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    float speed = 5f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        State = StateType.Idle;
    }

    void Update()
    {
        // 2초간 쉬는 임시코드
        if (Time.time < 2)
            return;

        Move();
    }

    #region Move
    void Move()
    {
        State = StateType.Running;
        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }
    #endregion Move

    #region State
    enum StateType
    {
        None,
        Idle,
        Running,
        Jump,
        Fall,
    }
    StateType m_state = StateType.None;

    StateType State 
    {
        get => m_state;
        set
        {
            if (m_state == value)
                return;

            print($"PlayerState : {m_state} -> {value}");
            m_state = value;
        }
    }
    #endregion State

    #region Methods
    bool GK(KeyCode _keycode) => Input.GetKey(_keycode);
    bool GKD(KeyCode _keycode) => Input.GetKeyDown(_keycode);
    #endregion Methods
}
