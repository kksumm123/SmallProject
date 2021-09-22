using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public static MenuUI Instance;
    void Awake() => Instance = this;
    void Start()
    {
        transform.Find("VolumeButton").GetComponent<Button>().onClick
                                      .AddListener(() => OnClick());
    }

    public void OnClick()
    {
        if (VolumeUI.Instance.gameObject.activeSelf == false)
            VolumeUI.Instance.ShowUI();
        else
            VolumeUI.Instance.CloseUI();
    }
}
