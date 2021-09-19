using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    float offsetX;
    void Start()
    {
        offsetX = Player.Instance.transform.position.x - transform.position.x;
    }

    [SerializeField] float compensationSpeed = 20f;
    Vector3 nextPosition;
    void Update()
    {
        nextPosition.x = Player.Instance.transform.position.x - transform.position.x - offsetX;
        transform.Translate(compensationSpeed * Time.deltaTime * nextPosition);
    }
}
