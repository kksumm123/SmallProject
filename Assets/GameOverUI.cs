using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;
    Text gameOverText;
    Text tabToContinueText;
    void Awake()
    {
        Instance = this;
        gameOverText = transform.Find("Text").GetComponent<Text>();
        tabToContinueText = transform.Find("TabToContinue").GetComponent<Text>();
        tabToContinueText.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        gameObject.SetActive(true);
        var localPos = gameOverText.rectTransform.localPosition;
        localPos.y += 600;
        gameOverText.rectTransform.localPosition = localPos;
        gameOverText.rectTransform.DOLocalMoveY(0, 2)
                    .SetEase(Ease.OutBounce)
                    .SetUpdate(true)
                    .SetLink(gameObject)
                    .OnComplete(() => PunchScaleTabToContinue());
    }

    Vector3 punchScale = new Vector3(0.1f, 0.1f, 0.1f);
    private void PunchScaleTabToContinue()
    {
        tabToContinueText.gameObject.SetActive(true);
        tabToContinueText.rectTransform.DOPunchScale(punchScale, 1, 1, 0.5f)
                         .SetLoops(-1, LoopType.Yoyo)
                         .SetUpdate(true)
                         .SetLink(tabToContinueText.gameObject);
    }
}
