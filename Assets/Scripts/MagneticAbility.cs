using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticAbility : MonoBehaviour
{
    public static MagneticAbility Instance;
    void Awake() => Instance = this;

    Transform PlayerTr => Player.Instance.transform;
    CircleCollider2D circleCol;
    void Start()
    {
        circleCol = GetComponent<CircleCollider2D>();
        Deactivate();
    }

    Vector3 dir;
    float magneticPower = 0.1f;
    void Update()
    {
        if (GameManager.Instance.GameState != GameStateType.Playing)
            return;

        foreach (var item in attachedCoins)
        {
            if (item.Key != null)
            {
                dir = PlayerTr.position - item.Key.position;
                dir.Normalize();
                item.Value.power += magneticPower;

                item.Key.Translate(item.Value.power * Time.deltaTime * dir);
            }
        }
    }

    public void Activate()
    {
        enabled = true;
        circleCol.enabled = true;
    }
    public void Deactivate()
    {
        enabled = false;
        circleCol.enabled = false;
        attachedCoins.Clear();
    }

    class MagneticPower
    {
        public float power = 0;
    }
    Dictionary<Transform, MagneticPower> attachedCoins = new Dictionary<Transform, MagneticPower>();
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (enabled == true)
        {
            if (collision.CompareTag("Coin"))
            {
                if (attachedCoins.ContainsKey(collision.transform))
                    return;
                else
                    attachedCoins[collision.transform] = new MagneticPower();
            }
        }
    }
}
