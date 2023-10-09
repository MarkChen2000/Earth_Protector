using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [HideInInspector] public static InputManager InputManagerSin;
    [SerializeField] TurretController _turretController;

    public bool CanControl = false;

    [SerializeField] GameObject GunSightIcon;

    void Awake()
    {
        //Singleton
        if (InputManagerSin != null && InputManagerSin != this) Destroy(this);
        else InputManagerSin = this;
    }

    void Update()
    {
        if (GunSightIcon != null) GunSightIcon.SetActive(false);

        if (!CanControl) return;

        if ( Input.touchCount > 0 )
        {
            Touch touch = Input.GetTouch(0);

            Vector2 position = Camera.main.ScreenToWorldPoint(touch.position);
            _turretController.TurretMove(position);
            _turretController.GunFire();

            if ( GunSightIcon!=null ) {
                GunSightIcon.SetActive(true);
                GunSightIcon.transform.position = position;
            }
            Debug.Log("Touched "+position.x + " " + position.y);
        }
    }
}
