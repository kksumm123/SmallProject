using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : AbilityBase
{
    public float speed = 1;
    public float DashSpeed = 3;
    Transform PlayerTr => Player.Instance.transform;

    public static DashAbility Instance;
    void Awake() => Instance = this;


    void Start()
    {
        Deactivate();
    }

    Vector3 dashPos = Vector3.zero;
    void Update()
    {
        if (GameManager.Instance.GameState != GameStateType.Playing)
            return;

        Player.Instance.SleepRigidBody();
        dashPos = PlayerTr.position;
        dashPos.y = dashPos.z = 0;
        PlayerTr.position = dashPos;
        PlayerTr.Translate(speed * Time.deltaTime * Vector3.right);
    }

    public override AbilityType GetAbilityType()
    {
        return AbilityType.Dash;
    }
    public override void Activate()
    {
        speed = DashSpeed;
        enabled = true;
    }
    public override void Deactivate()
    {
        speed = 1;
        enabled = false;
    }
}
