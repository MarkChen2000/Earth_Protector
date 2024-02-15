using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] Transform BGTrans, StarsBGTrans, StarsBG2Trans;
    [SerializeField] float BGMoveSpeed = 3f, StarsBGSpeed = 15f, StarsBG2Speed = 10f;

    Vector2 BGStartPos, StarsBGStartPos, StarsBG2StarsPos;
    [SerializeField] float BGRepeatWidth = 128f, StarsBGRepeatWidth = 128f, StarsBG2RepeatWidth = 136f; // the width of background in moving direction and devided by 2.

    void Awake()
    {
        BGStartPos = BGTrans.position;
        StarsBGStartPos = StarsBGTrans.position;
        StarsBG2StarsPos = StarsBG2Trans.position;
    }

    void Update()
    {
        MoveBackGrounds();

        if (BGTrans.position.x < BGStartPos.x - BGRepeatWidth) BGTrans.position = BGStartPos;
        if (StarsBGTrans.position.x < StarsBGStartPos.x - StarsBGRepeatWidth) StarsBGTrans.position = StarsBGStartPos;
        if (StarsBG2Trans.position.x < StarsBG2StarsPos.x - StarsBG2RepeatWidth) StarsBG2Trans.position = StarsBG2StarsPos;
    }

    void MoveBackGrounds()
    {
        BGTrans.Translate(Vector2.left * BGMoveSpeed * Time.deltaTime);
        StarsBGTrans.Translate(Vector2.left * StarsBGSpeed * Time.deltaTime);
        StarsBG2Trans.Translate(Vector2.left * StarsBG2Speed * Time.deltaTime);
    }
}
