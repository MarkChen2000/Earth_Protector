using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public static UIManager UIManagerSin;

    [SerializeField] GameObject StartMenu, EndMenu;
    [SerializeField] TMP_Text Text_Score, Text_EndMenuScore, Text_EndMenuBsetScore, Text_PauseMenuScore;

    void Awake()
    {
        //Singleton
        if (UIManagerSin != null && UIManagerSin != this) Destroy(this);
        else UIManagerSin = this;
    }

    void Start()
    {
        TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.GameIntro);
    }

    public bool StartGame()
    {
        if (!TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.GameStart)) return false;
        return true;
        //StartMenu.SetActive(false);
    }

    public void GameOver()
    {
        TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.GameOver);
        //EndMenu.SetActive(true);
    }

    public void Signal_ResetUI()
    {
        Text_Score.text = 0.ToString();
        Text_PauseMenuScore.text = 0.ToString();
        Text_EndMenuScore.text = 0.ToString();
    }

    public void UpdateUI(int score, int bestScore)
    {
        Text_Score.text = score.ToString();
        Text_PauseMenuScore.text = score.ToString();
        Text_EndMenuScore.text = score.ToString();
        Text_EndMenuBsetScore.text = bestScore.ToString();
    }
}
