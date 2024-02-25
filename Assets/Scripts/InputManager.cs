using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    /*bool IsPointerOverUIObject() // to prevent keep recieve touch signal while pressing button.
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }*/

    void Update()
    {
        if (GunSightIcon != null) GunSightIcon.SetActive(false);

        if (!CanControl) return;

        // if ( IsPointerOverUIObject() ) return;

        if ( Input.touchCount > 0 ) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended) return;
            // the end phase of touch will also count as touchcount, but in this case it will become a problem that it count as touch before pressed button. 

            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId)) return; //check touch,                                                                         
            //this conditional construct can not be placed be itself, or when the screen didnt be touched. it will have out-of-index error by the array.
            // BE CAREFUL! BE CRAEFUL! BE CAREFUL! leave the UI objects with "raycast target" enable will also make this be true.

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
            if (EventSystem.current.IsPointerOverGameObject()) return; // check mouse
            // BE CAREFUL! leave the UI objects with "raycast target" enable will also make this be true.

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
