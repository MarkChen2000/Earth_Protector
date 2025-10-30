using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GravityEffectType
{
    Physics, Simplified
}

public class MeteoriteController : MonoBehaviour
{
    //why they are public are mean the stats should controll by manager but not themselves.
    [HideInInspector] public GravityEffectType GravityType = GravityEffectType.Simplified;
    [HideInInspector] public float SimplifiedEarthPullForce = 0.001f;

    [HideInInspector] public Transform EarthTrans;
    [HideInInspector] public float EarthMass = 150f;

    [SerializeField] float Mass = 0.0001f;
    [SerializeField] float InitialForce = 0.1f, AngularVelocity = 130f;
    [SerializeField] int HitPoint = 1;

    //[SerializeField] float LifeTime = 5f;

    [SerializeField] GameObject EffectPrefab;

    Rigidbody2D Rigidbody2D;
    [SerializeField] TrailRenderer TrailRenderer;

    void Awake()
    {
        //Destroy(gameObject, LifeTime);
        Rigidbody2D = GetComponent<Rigidbody2D>();

        Rigidbody2D.mass = Mass;
    }

    void Update()
    {
        GravityEffect();
    }

    void FixedUpdate()
    {
        Rigidbody2D.angularVelocity = AngularVelocity;
    }

    public void SetDefaultStats()
    {
        EarthTrans = GameObject.Find("Earth").transform;
        Rigidbody2D.AddRelativeForce(transform.up.normalized * InitialForce);
        //Debug.Log(transform.up.normalized * Force);
    }

    public void SetStats(GravityEffectType gravityType,float pullForce, Transform earthTrans, float earthMass, float initialForceMultiplier)
    {
        GravityType = gravityType;
        SimplifiedEarthPullForce = pullForce;
        EarthTrans = earthTrans;
        EarthMass = earthMass;
        InitialForce *= initialForceMultiplier;

        AngularVelocity = Random.Range(80f, 250f);

        Rigidbody2D.AddRelativeForce( transform.up.normalized * InitialForce);
        //Debug.Log(transform.up.normalized * Force);
    }

    float currentPullForce;
    void GravityEffect()
    {
        if (!EarthTrans.gameObject.activeSelf) return;

        Vector2 vectorFaceToEarth = EarthTrans.position-transform.position.normalized;
        float distanceToEarth = Vector2.Distance(transform.position, EarthTrans.position);

        switch ( GravityType ) {
            case GravityEffectType.Physics:
                currentPullForce = 9.8f * Mass * EarthMass / Mathf.Pow( distanceToEarth, 2);
                //Debug.Log("Pull force from earth is: " + pullForce);

                break;
            case GravityEffectType.Simplified:
                currentPullForce = SimplifiedEarthPullForce;

                break;
        }
        Rigidbody2D.AddForce(vectorFaceToEarth * currentPullForce);

    }

    public void HitbyBullet(int costHitPoint)
    {
        if (HitPoint - costHitPoint <= 0) {
            GameManager.GameManagerSin.GainPoint(1);

            if (EffectPrefab != null) Instantiate(EffectPrefab, transform.position, Quaternion.identity);

            AudioManager.AudioManagerSin.PlaySoundEffect(SFX.Meteor_Destroy, transform.root);

            Destroy(gameObject);
        }
        else HitPoint -= costHitPoint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.transform.tag == "Earth") {
            GameManager.GameManagerSin.GameOver();
            
            Destroy(gameObject);
        }

        if ( collision.transform.tag == "Barrier") {
            Destroy(gameObject);
        }

    }

    /*public void GetDestroyed() // Because unparent trail creator in OnDestroy will make the trail creator not be cleaned when scene closes or changes. 
    {
        TrailRenderer.transform.parent = null;
    }*/

    bool isQutting = false;
    void OnApplicationQuit()
    {
        isQutting = true;
    }

    void OnDestroy()
    {
        if (EffectPrefab != null) Instantiate(EffectPrefab, transform.position, Quaternion.identity);

        if (isQutting) return; // Because unparent trail creator in OnDestroy will make the trail creator not be cleaned when scene closes or changes. 

        TrailRenderer.transform.parent = null;
        TrailRenderer.autodestruct = true;
        TrailRenderer = null;
    }

}
