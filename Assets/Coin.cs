using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Coin : MonoBehaviour
{
    Animator animator;
    CircleCollider2D circleCol;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        circleCol = GetComponent<CircleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            StartCoroutine(CoinTouchCo());
    }

    float touchAnimationTime = 0.5f;
    IEnumerator CoinTouchCo()
    {
        circleCol.enabled = false;
        animator.Play("CoinTouch");
        yield return new WaitForSeconds(touchAnimationTime);
        Destroy(gameObject);
    }
}
