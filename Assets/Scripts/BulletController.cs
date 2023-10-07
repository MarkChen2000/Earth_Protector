using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] float MoveSpeed = 10f;
    [SerializeField] float LifeTime = 3f;

    void Awake()
    {
        Destroy(gameObject,LifeTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(transform.up*MoveSpeed);
    }
}
