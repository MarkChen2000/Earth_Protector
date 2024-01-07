using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    Player, Enemy
}

public class ProjectileController : MonoBehaviour
{
    
    [SerializeField] ProjectileType ProjectileType = ProjectileType.Player;

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
        if ( ProjectileType == ProjectileType.Player ) {
            if (collision.transform.tag == "Meteorite") {
                collision.transform.GetComponent<MeteoriteController>().HitbyBullet(1);

                if (EffectPrefab != null) Instantiate(EffectPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }


    }

}
