using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteController : MonoBehaviour
{

    Transform EarthTrans;
    [SerializeField] float EarthMass = 1f;

    [SerializeField] float Mass = 0.01f, Force = 1f, AngularVelocity = 130f;
    [SerializeField] int HitPoint = 1;

    [SerializeField] float LifeTime = 5f;

    [SerializeField] GameObject EffectPrefab;

    Rigidbody2D _rigidbody2D;

    void Awake()
    {
        Destroy(gameObject, LifeTime);
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.mass = Mass;
    }

    float pullForce;
    void Update()
    {
        GravityEffect();
    }

    void FixedUpdate()
    {
        _rigidbody2D.angularVelocity = AngularVelocity;
    }

    public void SetDefaultStats()
    {
        EarthTrans = GameObject.Find("Earth").transform;
        _rigidbody2D.AddRelativeForce(transform.up.normalized * Force);
        //Debug.Log(transform.up.normalized * Force);
    }

    public void SetStats(Transform earthTrans, float earthMass, float force, float mass, int hitPoint)
    {
        EarthTrans = earthTrans;
        EarthMass = earthMass;
        Force = force;
        Mass = mass;
        HitPoint = hitPoint;

        AngularVelocity = Random.Range(60f, 180f);

        _rigidbody2D.AddRelativeForce( transform.up.normalized * Force);
        //Debug.Log(transform.up.normalized * Force);
    }

    void GravityEffect()
    {
        if (EarthTrans == null) return;

        pullForce = 9.8f * Mass * EarthMass / Mathf.Pow(Vector2.Distance(transform.position, EarthTrans.position), 2);
        Vector2 vectorFaceToEarth = EarthTrans.position-transform.position.normalized;
        _rigidbody2D.AddForce(vectorFaceToEarth * pullForce);
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
