using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
Order In Layer


Coin    50

Player  100

clear : StageClear 구현하기
clear : highScore 플레이어 프리퍼런스로 불러오기
clear : 능력 일시적 활성화 구현하기
todo : 돌진능력 구현하기 
*/
public class Player : MonoBehaviour
{
    public static Player Instance;
    void Awake() => Instance = this;

    Animator animator;
    BoxCollider2D boxCol;
    Rigidbody2D rigid;
    float gravityScale = 4;
    float offsetX;
    Button jumpButton;
    Button downButton;
    void Start()
    {
        var controlKeyCanvas = GameObject.Find("ControlKeyCanvas");
        jumpButton = controlKeyCanvas.transform.Find("ControlKey/JumpButton").GetComponent<Button>();
        downButton = controlKeyCanvas.transform.Find("ControlKey/DownButton").GetComponent<Button>();
        jumpButton.onClick.AddListener(() => JumpLogic());
        downButton.onClick.AddListener(() => ForceDownLogic());

        animator = GetComponentInChildren<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        footOffset = boxCol.size.y * 0.5f - boxCol.offset.y;
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        rigid.gravityScale = gravityScale;

        State = StateType.Idle;
        offsetX = Camera.main.transform.position.x - transform.position.x;
    }

    bool IsFixedUpdated = false;
    void FixedUpdate()
    {
        IsFixedUpdated = true;
    }

    void Update()
    {
        if (GameManager.Instance.GameState != GameStateType.Playing)
            return;

        StateUpdate();
        Move();
        Jump();
        ForceDown();
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
    bool IsHitWall()
    {
        var hit = Physics2D.Raycast(transform.position + new Vector3(boxCol.size.x, 0, 0)
             , Vector2.right, IsGroundRayDistance, groundLayer);
        return hit.transform;
    }
    #endregion StateUpdate

    #region Move
    [SerializeField] float compensationSpeed = 6f;
    Vector3 nextPosition;
    void Move()
    {
        if (IsHitWall())
            return;

        nextPosition.x = Camera.main.transform.position.x - transform.position.x - offsetX;
        transform.Translate(compensationSpeed * Time.deltaTime * nextPosition);
    }
    #endregion Move

    #region Jump
    [Header("점프")]
    [SerializeField] int currentJumpCount = 0;
    [SerializeField] int maxJumpCount = 2;
    [SerializeField] float jumpForceY = 700;
    void Jump()
    {
        if (IsGround() && IsFixedUpdated)
            currentJumpCount = 0;

        if (GKD(KeyCode.W))
            JumpLogic();
    }

    private void JumpLogic()
    {
        if (currentJumpCount < maxJumpCount)
        {
            currentJumpCount++;
            State = StateType.Jump;
            IsFixedUpdated = false;
            rigid.Sleep();
            rigid.AddForce(new Vector2(0, jumpForceY), ForceMode2D.Force);
        }
    }
    #endregion Jump

    #region 
    [SerializeField] float downForceY = 700;
    private void ForceDown()
    {
        if (GKD(KeyCode.S))
            ForceDownLogic();
    }

    void ForceDownLogic()
    {
        if (currentJumpCount > 0)
        {
            State = StateType.Fall;
            IsFixedUpdated = false;
            rigid.Sleep();
            rigid.AddForce(new Vector2(0, -downForceY), ForceMode2D.Force);
        }
    }
    #endregion ForceDown

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
        if (Application.isPlaying == false)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position - new Vector3(0, footOffset, 0)
            , Vector2.down * IsGroundRayDistance);
        Gizmos.DrawRay(transform.position + new Vector3(boxCol.size.x, 0, 0)
            , Vector2.right * IsGroundRayDistance);
    }
}
