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

        if ( Input.touchCount > 0 ) {
            Touch touch = Input.GetTouch(0);

            //Debug.Log("Touched point in screen space: " + touch.position.x + " " + touch.position.y);

            Vector2 position = Camera.main.ScreenToWorldPoint(touch.position);
            _turretController.TurretMove(position);
            _turretController.GunFire();

            if ( GunSightIcon!=null ) {
                GunSightIcon.SetActive(true);
                GunSightIcon.transform.position = position;
            }
            //Debug.Log("Touched point in world space: "+position.x + " " + position.y);


        }

        if (Input.GetMouseButton(0)) {

            //Debug.Log("Mouse point in screen space: " + Input.mousePosition.x + " " + Input.mousePosition.y);

            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _turretController.TurretMove(position);
            _turretController.GunFire();

            if (GunSightIcon != null)
            {
                GunSightIcon.SetActive(true);
                GunSightIcon.transform.position = position;
            }

            //Debug.Log("Mouse point in world space: " + position.x + " " + position.y);
        }
    }
}
