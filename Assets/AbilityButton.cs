using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    Image abilityImage;
    void Start()
    {
        abilityImage = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(() => UseAbility());
    }

    float useableTime;
    [SerializeField] float coolTime = 10;
    void UseAbility()
    {
        if (GameManager.Instance.GameState != GameStateType.Playing)
            return;

        if (useableTime < Time.time)
        {
            useableTime = Time.time + coolTime;
            StartCoroutine(CoolTimeCo());
            StartCoroutine(UseAbilityCo());
        }
    }

    IEnumerator CoolTimeCo()
    {
        var endTime = Time.time + coolTime;
        while (Time.time < endTime)
        {
            abilityImage.fillAmount = 1 - ((endTime - Time.time) / coolTime);
            print(abilityImage.fillAmount);
            yield return null;
        }
        abilityImage.fillAmount = 1;
    }

    [SerializeField] float duration = 3;
    IEnumerator UseAbilityCo()
    {
        MagneticAbility.Instance.Activate();
        yield return new WaitForSeconds(duration);
        MagneticAbility.Instance.Deactivate();
    }
}
