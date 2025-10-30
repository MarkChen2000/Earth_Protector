using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Meteorite") {
            MeteoriteController mCon = collision.transform.GetComponent<MeteoriteController>();
            mCon.HitbyBullet(1);

            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (EffectPrefab != null) Instantiate(EffectPrefab, transform.position, Quaternion.identity);
    }

}
