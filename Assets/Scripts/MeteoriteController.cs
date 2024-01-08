using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GravityEffectType
{
    Physics, Simplified
}

public class MeteoriteController : MonoBehaviour
{
    [SerializeField] GravityEffectType GravityType = GravityEffectType.Simplified;
    [SerializeField] float SimplifiedPullForce = 0.001f;

    Transform EarthTrans;
    [SerializeField] float EarthMass = 150f;

    [SerializeField] float Mass = 0.0001f, Force = 0.1f, AngularVelocity = 130f;
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

    public void SetStats(GravityEffectType gravityType, Transform earthTrans, float earthMass, float force, float mass, int hitPoint)
    {
        GravityType = gravityType;
        EarthTrans = earthTrans;
        EarthMass = earthMass;
        Force = force;
        Mass = mass;
        HitPoint = hitPoint;

        AngularVelocity = Random.Range(100f, 200f);

        _rigidbody2D.AddRelativeForce( transform.up.normalized * Force );
        //Debug.Log(transform.up.normalized * Force);
    }

    float pullForce;
    void GravityEffect()
    {
        if (!EarthTrans.gameObject.activeSelf) return;

        Vector2 vectorFaceToEarth = EarthTrans.position-transform.position.normalized;
        float distanceToEarth = Vector2.Distance(transform.position, EarthTrans.position);

        switch ( GravityType ) {
            case GravityEffectType.Physics:
                pullForce = 9.8f * Mass * EarthMass / Mathf.Pow( distanceToEarth, 2);
                Debug.Log("Pull force from earth is: " + pullForce);

                break;
            case GravityEffectType.Simplified:
                pullForce = SimplifiedPullForce;

                break;
        }
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
