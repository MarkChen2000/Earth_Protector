using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;

public class TimelineManager : MonoBehaviour
{

    [HideInInspector] public static TimelineManager TimelineManagerSin;

    [SerializeField] PlayableDirector Director;
    [Serializable] public struct TimelineClips
    {
        public PlayableAsset GameIntro, GameStart, GameOver, ResetFromEndMenu, ResetFromPause, InPause, OutPause, InSetting, OutSetting, InCredit, OutCredit;
    }
    public TimelineClips timelineClips;

    void Awake()
    {
        //Singleton
        if (TimelineManagerSin != null && TimelineManagerSin != this) Destroy(this);
        else TimelineManagerSin = this;
    }

    public bool PlayTimeline(PlayableAsset clip)
    {
        if (Director.state == PlayState.Playing) {
            return false;
        }

        if (clip == null) {
            Debug.LogWarning("The timeline clip doesn't exist!");
            return false;
        }

        Director.Play(clip);
        return true;
    }

}
