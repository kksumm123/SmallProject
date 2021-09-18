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
    }

    void Update()
    {
        Move();
    }

    #region Move
    Vector2 move;
    void Move()
    {
        move = Vector2.zero;
        if (GK(KeyCode.A)) move.x = -1;
        if (GK(KeyCode.D)) move.x = 1;

        if (move != Vector2.zero)
        {
            transform.Translate(speed * Time.deltaTime * move);
        }
    }
    #endregion Move

    #region Methods
    bool GK(KeyCode _keycode) => Input.GetKey(_keycode);
    bool GKD(KeyCode _keycode) => Input.GetKeyDown(_keycode);
    #endregion Methods
}
