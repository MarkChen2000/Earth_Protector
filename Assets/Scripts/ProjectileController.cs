using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public float MoveSpeed = 10f;
    [SerializeField] float LifeTime = 3f;

    [SerializeField] GameObject EffectPrefab;

    void Awake()
    {
        Destroy(gameObject, LifeTime);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up* MoveSpeed,Space.Self);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (EffectPrefab != null) Instantiate(EffectPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

}
