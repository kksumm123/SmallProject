using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] float mapGapValue = 18;
    [SerializeField] List<Transform> maps = new List<Transform>();
    void Start()
    {
        if (maps.Count == 0)
            Debug.LogError("¸ÊÀÌ ¾ø¾î!");
    }

    void Update()
    {
        
    }
}
