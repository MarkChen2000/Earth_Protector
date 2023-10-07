using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    [SerializeField] TurretController _turretController;

    void Awake()
    {
        
    }

    void Update()
    {
        if ( Input.touchCount > 0 )
        {
            Touch touch = Input.GetTouch(0);
            Vector2 position = Camera.main.ScreenToWorldPoint(touch.position);

            _turretController.TurretMove(position);


            Debug.Log("Touched "+position.x + " " + position.y);
        }
    }
}
