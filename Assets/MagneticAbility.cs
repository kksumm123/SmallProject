using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticAbility : MonoBehaviour
{
    CircleCollider2D circleCol;
    void Start()
    {
        circleCol = GetComponent<CircleCollider2D>();
    }
}
