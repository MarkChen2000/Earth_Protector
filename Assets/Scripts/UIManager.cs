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
