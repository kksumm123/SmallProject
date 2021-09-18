using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    BoxCollider2D boxCol;
    Rigidbody2D rigid;
    [SerializeField] float speed = 5f;
    float gravityScale = 4;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        footOffset = boxCol.size.y * 0.5f - boxCol.offset.y;
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        rigid.gravityScale = gravityScale;

        State = StateType.Idle;
    }

    bool IsFixedUpdated = false;
    void FixedUpdate()
    {
        IsFixedUpdated = true;
    }

    void Update()
    {
        // 2초간 쉬는 임시코드
        if (Time.time < 2)
            return;

        StateUpdate();
        Move();
        Jump();
    }

    #region StateUpdate
    void StateUpdate()
    {
        if (IsFixedUpdated == false)
            return;
        var velo = rigid.velocity;
        if (IsGround())
            State = StateType.Running;
        else if (velo.y > 0.1f)
            State = StateType.Jump;
        else if (velo.y < -0.1f)
            State = StateType.Fall;
    }

    float footOffset;
    float IsGroundRayDistance = 0.01f;
    LayerMask groundLayer;
    bool IsGround()
    {
        var hit = Physics2D.Raycast(transform.position - new Vector3(0, footOffset, 0)
             , Vector2.down, IsGroundRayDistance, groundLayer);
        return hit.transform;
    }
    #endregion StateUpdate

    #region Move
    void Move()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }
    #endregion Move

    #region Jump
    int currentJumpCount = 0;
    [SerializeField] int maxJumpCount = 2;
    [SerializeField] float jumpForceY = 600;
    void Jump()
    {
        if (IsGround() && IsFixedUpdated)
            currentJumpCount = 0;

        if (GKD(KeyCode.W) && currentJumpCount < maxJumpCount)
        {
            currentJumpCount++;
            State = StateType.Jump;
            IsFixedUpdated = false;
            rigid.Sleep();
            rigid.AddForce(new Vector2(0, jumpForceY), ForceMode2D.Force);
        }
    }
    #endregion Jump

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
            animator.Play(m_state.ToString());
        }
    }
    #endregion State

    #region Methods
    bool GK(KeyCode _keycode) => Input.GetKey(_keycode);
    bool GKD(KeyCode _keycode) => Input.GetKeyDown(_keycode);
    #endregion Methods

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position - new Vector3(0, footOffset, 0)
            , Vector2.down * IsGroundRayDistance);
    }
}
