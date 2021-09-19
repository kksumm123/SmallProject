using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }
}
