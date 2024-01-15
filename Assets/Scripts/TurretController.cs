using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    
    enum TurretTurnMode
    {
        Immediately, Speed
    }

    [SerializeField] Animator Animator;

    [SerializeField] TurretTurnMode TurnMode = TurretTurnMode.Speed;

    [SerializeField] float TurretRotationSpeed = 1f, GunFireRate = 0.5f;

    [SerializeField] Transform BulletSpawnTrans;
    [SerializeField] GameObject Bullet_Prefab, ShootingEffect_Prefab;
    [SerializeField] Transform Trans_GunSpot;

    void Awake()
    {
        
    }

    void Start()
    {
        Animator.speed = 1 / GunFireRate;
    }

    [SerializeField] bool UseButton = true;

    // Update is called once per frame
    void Update()
    {
        if (UseButton) {
            ButtonControl();
        }
    }

    int movedir;
    void ButtonControl()
    {
        if (Input.GetKey(KeyCode.A)) {
            movedir = 1;
        }
        else if (Input.GetKey(KeyCode.D)) {
            movedir = -1;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            GunFire();
        }
        movedir = 0;
    }

    void FixedUpdate()
    {
        gameObject.transform.Rotate(new Vector3(0f, 0f, 1f), movedir * TurretRotationSpeed); //rotate turret in clockwise or anti-clockwise with speed.
        movedir = 0;
    }

    public void TurretMove(Vector2 targetpos)
    {
        Vector2 dir = (targetpos - Vector2.zero).normalized;

        switch ( TurnMode ) {
            case TurretTurnMode.Immediately:
                // method1: after calculate the angle, rotate turret immediately.
                float rotateAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; //計算出前方向量(1,0)到目標向量的弧度再乘Rad2Deg成為角度
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotateAngle - 90f));

                return;
            case TurretTurnMode.Speed:
                // method2: calculate the angle between target and the turret facing vector. And rotate turret by speed.
                // float rotateAngle = Vector3.Angle(transform.up.normalized, dir);
                Vector3 cross = Vector3.Cross(transform.up.normalized, dir); // Cross 外積 對於角度的判定很有幫助！
                if (cross.z >= 0) movedir = 1;
                else if (cross.z < 0) movedir = -1;
                //Debug.Log(cross+" "+movedir);

                return;
        }
    }

    float lastfiretimer = 0f;
    public void GunFire()
    {
        if (Time.time < lastfiretimer + GunFireRate) return;
        lastfiretimer = Time.time;

        Instantiate(Bullet_Prefab, Trans_GunSpot.position, Trans_GunSpot.rotation, BulletSpawnTrans);
        Instantiate(ShootingEffect_Prefab, Trans_GunSpot.position, Trans_GunSpot.rotation);

        Animator.SetTrigger("Fire");
    }


}
