using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    void Update()
    {
        // 2초간 쉬는 임시코드
        if (Time.time < 2)
            return;

        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }
}
