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
        transform.Find("Button").GetComponent<Button>().onClick
                                .AddListener(() => OnClick());
    }

    void OnClick()
    {
        if (VolumeUI.Instance.gameObject.activeSelf == false)
            VolumeUI.Instance.ShowUI();
        else
            VolumeUI.Instance.CloseUI();
    }
}
