using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallelMoveBackgroundManager : MonoBehaviour
{
    [Serializable]
    public struct ParallelMoveBackgorund
    {
        public RectTransform _rectTransform;
        [HideInInspector] public Vector2 _startPos;
        public float _rollbackWidth, moveSpeed;
        // the width of background is moving direction devided by 2.
    }

    [SerializeField] List<ParallelMoveBackgorund> backgroundsList = new List<ParallelMoveBackgorund>();

    /*[SerializeField] RectTransform BGTrans, StarsBGTrans, StarsBG2Trans;
    [SerializeField] float BGMoveSpeed = 3f, StarsBGSpeed = 15f, StarsBG2Speed = 10f;

    Vector2 BGStartPos, StarsBGStartPos, StarsBG2StartPos;
    [SerializeField] float BGRepeatWidth = 128f, StarsBGRepeatWidth = 128f, StarsBG2RepeatWidth = 136f; */

    void Awake()
    {
        for (int i = 0; i < backgroundsList.Count; i++)
        {
            var bg = backgroundsList[i];
            bg._startPos = bg._rectTransform.localPosition;
        }

        /*BGStartPos = BGTrans.localPosition;
        StarsBGStartPos = StarsBGTrans.localPosition;
        StarsBG2StartPos = StarsBG2Trans.localPosition;*/
    }

    void FixedUpdate()
    {
        MoveBackgrounds();

        for (int i = 0; i < backgroundsList.Count; i++)
        {
            var bg = backgroundsList[i];

            if ( bg._rectTransform.localPosition.x < bg._startPos.x - bg._rollbackWidth) // because the condition is "<" means it will not be perfactlly 0 when backgrounds have to roll back.
                bg._rectTransform.localPosition = new Vector2(bg._startPos.x + (bg._rectTransform.localPosition.x - (bg._startPos.x - bg._rollbackWidth)), bg._startPos.y);
        }


        /*if (BGTrans.localPosition.x < BGStartPos.x - BGRepeatWidth)
        { 
            BGTrans.localPosition = new Vector2(BGStartPos.x + (BGTrans.localPosition.x - (BGStartPos.x - BGRepeatWidth)), BGStartPos.y);
        }
        if (StarsBGTrans.localPosition.x < StarsBGStartPos.x - StarsBGRepeatWidth)
        {
            StarsBGTrans.localPosition = new Vector2(StarsBGStartPos.x + (StarsBGTrans.localPosition.x - (StarsBGStartPos.x - StarsBGRepeatWidth)), StarsBGStartPos.y);
        }
        if (StarsBG2Trans.localPosition.x < StarsBG2StartPos.x - StarsBG2RepeatWidth)
        {
            StarsBG2Trans.localPosition = new Vector2(StarsBG2StartPos.x + (StarsBG2Trans.localPosition.x - (StarsBG2StartPos.x - StarsBG2RepeatWidth)), StarsBG2StartPos.y);
        }*/
    }

    void MoveBackgrounds()
    {
        for (int i = 0; i < backgroundsList.Count; i++)
        {
            var bg = backgroundsList[i];
            bg._rectTransform.Translate(Vector2.left * bg.moveSpeed * Time.fixedDeltaTime);
        }

        /*BGTrans.Translate(Vector2.left * BGMoveSpeed * Time.fixedDeltaTime);
        StarsBGTrans.Translate(Vector2.left * StarsBGSpeed * Time.fixedDeltaTime);
        StarsBG2Trans.Translate(Vector2.left * StarsBG2Speed * Time.fixedDeltaTime);*/
    }
}
