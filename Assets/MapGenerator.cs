using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] int mapCount = 20;
    [SerializeField] Transform mapParent;
    [SerializeField] float mapGapValue = 18;
    [SerializeField] List<Transform> maps = new List<Transform>();
    void Awake()
    {
        if (maps.Count == 0)
            Debug.LogError("���� ����!");

        for (int i = 0; i < mapCount; i++)
        {
            // �� �������� ����, ���� & ��ġ
            var newMapGo = Instantiate(maps[Random.Range(0, maps.Count)], mapParent);
            newMapGo.position = new Vector3((i + 1) * mapGapValue, 0, 0);
        }
    }
}
