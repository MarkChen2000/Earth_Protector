using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRecycleManager : MonoBehaviour
{

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Meteorite") Destroy(collision.gameObject);
        if (collision.transform.tag == "Bullet") Destroy(collision.gameObject);
    }
}
