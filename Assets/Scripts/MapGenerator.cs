using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] int mapCount = 20;
    [SerializeField] Transform mapParent;
    [SerializeField] float mapGapValue = 18;
    [SerializeField] List<Transform> maps = new List<Transform>();
    [SerializeField] Transform mapEnd;
    void Awake()
    {
        if (maps.Count == 0)
            Debug.LogError("���� ����!");
        else if (mapEnd == null)
            Debug.LogError("������ ���� ����!");

        Vector3 mapPos;
        mapPos = Vector3.zero;
        for (int i = 0; i < mapCount; i++)
        {
            // �� �������� ����, ���� & ��ġ
            var newMapGo = Instantiate(maps[Random.Range(0, maps.Count)], mapParent);
            mapPos.x += mapGapValue;
            newMapGo.position = mapPos;
        }
        var mapEndGo = Instantiate(mapEnd, mapParent);
        mapPos.x += mapGapValue;
        mapEndGo.position = mapPos;
    }
}
