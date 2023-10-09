using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public static UIManager UIManagerSin;

    [SerializeField] GameObject StartMenu, EndMenu;
    [SerializeField] TMP_Text TMPText_Score;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupUI()
    {
        TMPText_Score.text = 0.ToString();
    }

    public void DisableStartMenu()
    {
        TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.GameStart);
        //StartMenu.SetActive(false);
    }

    public void EnableEndMenu()
    {
        TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.GameOver);
        //EndMenu.SetActive(true);
    }

    public void ReturnToStart()
    {
        TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.Return);
    }

    public void UpdateUI(int score)
    {
        TMPText_Score.text = score.ToString();
    }
}
