using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyUI : MonoBehaviour
{
    public static ReadyUI Instance;
    void Awake() => Instance = this;

    Text readyText;
    void Start()
    {
        readyText = GetComponentInChildren<Text>();
    }

    internal void SetReady(int readyTime)
    {
        StartCoroutine(ReadyCo(readyTime));
    }

    IEnumerator ReadyCo(int readyTime)
    {
        for (int i = readyTime; i > 0; i--)
        {
            readyText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        readyText.text = "G O ! ! !";
    }
}
