using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public static DashAbility Instance;
    void Awake() => Instance = this;

    [SerializeField] float speed = 18;
    Transform PlayerTr => Player.Instance.transform;
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
        PlayerTr.Translate(speed * Time.deltaTime * Vector3.right);
    }

    public void Activate()
    {
        enabled = true;
    }
    public void Deactivate()
    {
        enabled = false;
    }
}
