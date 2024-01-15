using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPrefabController : MonoBehaviour
{
    [SerializeField] float LifeTime = 1f; 

    void Start()
    {
        Destroy(gameObject,LifeTime);
    }
}
