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
            Debug.LogError("맵이 없어!");
    }

    void Update()
    {
        // 맵 랜덤으로 선택, 복사 & 배치
    }
}
