using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    void Update()
    {
        // 2�ʰ� ���� �ӽ��ڵ�
        if (Time.time < 2)
            return;

        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }
}
