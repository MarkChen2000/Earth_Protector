using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] Transform StarsBG1Trans, StarsBG2Trans;
    [SerializeField] Transform BG1Trans, BG2Trans;
    [SerializeField] float StarsMoveSpeed = 0.05f, BGMoveSpeed = 0.01f;


    void Update()
    {
        BG1Trans.position = new Vector2(BG1Trans.position.x + BGMoveSpeed, BG1Trans.position.y);
    }
}
