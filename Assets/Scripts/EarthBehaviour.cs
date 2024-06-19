using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBehaviour : MonoBehaviour
{
    public static EarthBehaviour EarthBehaviourSin;

    [SerializeField] Animator animator;
    [SerializeField] GameObject Effect_ExplosionOfEarthPrefab;

    void Awake()
    {
        //Singleton
        if (EarthBehaviourSin != null && EarthBehaviourSin != this) Destroy(this);
        else EarthBehaviourSin = this;
    }

    public void EarthBeDestroyed()
    {
        if (Effect_ExplosionOfEarthPrefab!=null)
        {
            Instantiate(Effect_ExplosionOfEarthPrefab,transform.position,Quaternion.identity);
        }
    }
}
