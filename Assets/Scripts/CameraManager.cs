using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public enum ShakeDataTType
{
    Test, Shoot_SingleGun, Hitted
}

public class CameraManager : MonoBehaviour
{
    [SerializeField] ShakeData ShakeData_Test, ShakeData_Shoot_SingleGun, ShakeData_Hitted;

    [HideInInspector] public static CameraManager CameraManagerSin;
    void Awake()
    {
        //Singleton
        if (CameraManagerSin != null && CameraManagerSin != this) Destroy(this);
        else CameraManagerSin = this;
    }

    public bool Shake(ShakeDataTType type)
    {
        switch (type) {
            case ShakeDataTType.Test:
                if (ShakeData_Test == null) {
                    Debug.Log("Fail to Shake:" + ShakeDataTType.Test);
                    return false;
                }
                CameraShakerHandler.Shake(ShakeData_Test);
                break;
            case ShakeDataTType.Shoot_SingleGun:
                if (ShakeData_Shoot_SingleGun == null)
                {
                    Debug.Log("Fail to Shake:" + ShakeDataTType.Shoot_SingleGun);
                    return false;
                }
                CameraShakerHandler.Shake(ShakeData_Shoot_SingleGun);
                break;
            case ShakeDataTType.Hitted:
                if (ShakeData_Hitted == null)
                {
                    Debug.Log("Fail to Shake:" + ShakeDataTType.Hitted);
                    return false;
                }
                CameraShakerHandler.Shake(ShakeData_Hitted);
                break;
        }

        return true;

    }

}
