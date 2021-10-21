using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour
{
    public static VolumeUI Instance;
    void Awake() => Instance = this;

    Slider bgmSlider;
    Slider sfxSlider;
    AudioSource bgmPlayer;
    AudioSource coinSFXPlayer;
    void Start()
    {
        bgmSlider = transform.Find("BGM/Slider").GetComponent<Slider>();
        sfxSlider = transform.Find("SFX/Slider").GetComponent<Slider>();
        bgmPlayer = GameObject.Find("BGMPlayer").GetComponent<AudioSource>();
        coinSFXPlayer = GameObject.Find("CoinSFXPlayer").GetComponent<AudioSource>();

        bgmSlider.onValueChanged.AddListener((x) => bgmPlayer.volume = x);
        sfxSlider.onValueChanged.AddListener((x) => coinSFXPlayer.volume = x);
        CloseUI();
    }

    public void ShowUI()
    {
        GameManager.Instance.GameState = GameStateType.Menu;
        gameObject.SetActive(true);

        var localPos = transform.localPosition;
        localPos.y += 600;
        transform.localPosition = localPos;
        transform.DOKill();
        transform.DOLocalMoveY(0, 1)
                 .SetEase(Ease.OutBounce)
                 .SetUpdate(true)
                 .SetLink(gameObject);
    }
    public void CloseUI()
    {
        GameManager.Instance.GameState = GameStateType.MenuOut;
        gameObject.SetActive(false);
    }
}
