using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public static UIManager UIManagerSin;

    [SerializeField] GameObject StartMenu, EndMenu;
    [SerializeField] TMP_Text Text_Score, Text_EndMenuScore, Text_EndMenuBsetScore;

    void Awake()
    {
        //Singleton
        if (UIManagerSin != null && UIManagerSin != this) Destroy(this);
        else UIManagerSin = this;
    }

    void Start()
    {
        SetupUI();

        TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.GameIntro);
    }

    void SetupUI()
    {
        Text_Score.text = 0.ToString();
    }

    public void StartGame()
    {
        TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.GameStart);
        //StartMenu.SetActive(false);
    }

    public void GameOver()
    {
        Text_EndMenuScore.text = Text_Score.text;

        TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.GameOver);
        //EndMenu.SetActive(true);
    }

    bool isSettingMenuEnable = false;
    public void Button_Setting()
    {
        if (!isSettingMenuEnable){
            if (!TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.InSetting)) return;
            isSettingMenuEnable = true;
        }
        else{
            if (!TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.OutSetting)) return;
            isSettingMenuEnable = false;
        }
    }

    bool isPauseMenuEnable = false;
    public void Button_Pause()
    {
        if ( !isPauseMenuEnable) {
            if ( !TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.InPause) ) return ;
            isPauseMenuEnable = true;
        } else {
            if ( !TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.OutPause) ) return ;
            isPauseMenuEnable = false;
        }
    }


    public void Reset()
    {
        TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.Reset);
    }

    public void Signal_ResetUI()
    {
        Text_Score.text = 0.ToString();
    }

    public void UpdateUI(int score, int bestScore)
    {
        Text_Score.text = score.ToString();
        Text_EndMenuBsetScore.text = bestScore.ToString();
    }
}
