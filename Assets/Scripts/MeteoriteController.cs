using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteController : MonoBehaviour
{
    Transform Earth;
    [SerializeField] float EarthMass = 1f;

    public float Mass = 0.01f, Force = 1f;
    public int HitPoint = 1;

    [SerializeField] float LifeTime = 5f;

    [SerializeField] GameObject EffectPrefab;

    Rigidbody2D _rigidbody2D;

    void Awake()
    {
        Destroy(gameObject, LifeTime);
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.mass = Mass;

        Earth = GameObject.Find("Earth").transform;
    }

    float pullForce;
    void Update()
    {
        pullForce = 9.8f * Mass * EarthMass / Mathf.Pow(Vector2.Distance(transform.position, new Vector2(0f, 0f)), 2);
        Vector2 vectorFaceToEarth = -transform.position.normalized;
        _rigidbody2D.AddForce(vectorFaceToEarth * pullForce);
    }

    public void SetStats()
    {
        _rigidbody2D.AddRelativeForce(transform.up.normalized * Force);
        //Debug.Log(transform.up.normalized * Force);
    }

    public void SetStats(float force, float mass, int hitPoint)
    {
        if (force != Force) Force = force;
        if (mass!=Mass) Mass = mass;
        if (HitPoint != hitPoint) HitPoint = hitPoint;

        _rigidbody2D.AddRelativeForce( transform.up.normalized * Force);
        //Debug.Log(transform.up.normalized * Force);
    }

    public void HitbyBullet(int costHitPoint)
    {
        if (HitPoint - costHitPoint <= 0) {
            GameManager.GameManagerSin.GainPoint(1);

            if (EffectPrefab != null) Instantiate(EffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else HitPoint -= costHitPoint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.transform.tag == "Earth") {
            GameManager.GameManagerSin.GameOver();

            if (EffectPrefab != null) Instantiate(EffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if ( collision.transform.tag == "Barrier") {
            if (EffectPrefab != null) Instantiate(EffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

}
