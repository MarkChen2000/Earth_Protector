using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] Transform BGTrans, StarsBGTrans;
    [SerializeField] float BGMoveSpeed = 0.01f, StarsMoveSpeed = 0.05f;

    Vector2 BGStartPos, StarsBGStartPos;
    float repeatWidth;

    void Awake()
    {
        BGStartPos = BGTrans.position;
        repeatWidth = 128f;
    }

    void Update()
    {
        MoveBackGrounds();

        if (BGTrans.position.x < BGStartPos.x - repeatWidth) BGTrans.position = BGStartPos;
        if (StarsBGTrans.position.x < StarsBGStartPos.x - repeatWidth) StarsBGTrans.position = StarsBGStartPos;
    }

    void MoveBackGrounds()
    {
        BGTrans.Translate(Vector2.left * BGMoveSpeed * Time.deltaTime);
        StarsBGTrans.Translate(Vector2.left * StarsMoveSpeed * Time.deltaTime);
    }
}
