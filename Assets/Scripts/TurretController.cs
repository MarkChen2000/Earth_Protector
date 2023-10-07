using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    
    [SerializeField] float TurretRotationSpeed = 1f;

    [SerializeField] GameObject Bullet_Prefab;
    [SerializeField] Transform Trans_GunSpot;

    void Awake()
    {
        
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
        movedir = 0;
        if (Input.GetKey(KeyCode.A)) {
            movedir = 1;
        }
        else if (Input.GetKey(KeyCode.D)) {
            movedir = -1;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(Bullet_Prefab,Trans_GunSpot.position,Trans_GunSpot.rotation);
        }
    }

    void FixedUpdate()
    {
        gameObject.transform.Rotate(new Vector3(0f, 0f, 1f), movedir * TurretRotationSpeed);
    }

    public void TurretMove(Vector2 targetpos)
    {
        Vector2 dir = (targetpos - Vector2.zero).normalized;

        float rotateAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; //�p��X(1,0)�e��V�q��ؼЦV�q�����צA��Rad2Deg��������
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotateAngle - 90f));

        
    }

}
