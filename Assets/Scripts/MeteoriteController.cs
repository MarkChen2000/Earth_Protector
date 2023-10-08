using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteController : MonoBehaviour
{
    public int HitPoint = 1;
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

    public void HitbyBullet(int costHitPoint)
    {
        if (HitPoint - costHitPoint <= 0)
        {
            GameManager.GameManagerSin.GainPoint(1);
        }
        else HitPoint -= costHitPoint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.transform.tag == "Earth") {
            GameManager.GameManagerSin.EarthDestroyed();
        }

        if (EffectPrefab != null) Instantiate(EffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
