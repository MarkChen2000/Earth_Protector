using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager GameManagerSin;
    [SerializeField] MeteoriteSpawnManager meteoriteSpawnManager;
    [SerializeField] GameObject Testing_InvisibleBarrierObj;
    [SerializeField] Transform BulletSpawnTrans, MeteoriteSpawnTrans;

    public int Score = 0, BestScore = 0/*, Money=0*/;

    void Awake()
    {
        //Singleton
        if (GameManagerSin != null && GameManagerSin != this) Destroy(this);
        else GameManagerSin = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.UIManagerSin.UpdateUI(Score, BestScore);
    }

    Coroutine meteoriteSpawnCoroutine;

    public void Button_StartGame()
    {
        InputManager.InputManagerSin.CanControl = true;

        UIManager.UIManagerSin.StartGame();
        meteoriteSpawnCoroutine = StartCoroutine( meteoriteSpawnManager.StartSpawningMeteorites() );
    }

    bool isSettingMenuEnable = false;
    public void Button_Setting()
    {
        if (!isSettingMenuEnable) {
            if (!TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.InSetting)) return;
            isSettingMenuEnable = true;
        }
        else {
            Debug.Log("Warining! Setting menu was already be opened.");
        }
    }

    public void Button_ResumeFromSetting()
    {
        if (isSettingMenuEnable) {
            if (!TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.OutSetting)) return;
            isSettingMenuEnable = false;
        }
        else {
            Debug.Log("Warining! Setting menu was not be opened.");
        }
    }

    bool isPauseMenuEnable = false;
    bool isGamePause = false;
    public void Button_Pause()
    {
        if (!isPauseMenuEnable) {

            if (!TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.InPause)) return;

            InputManager.InputManagerSin.CanControl = false;
            Time.timeScale = 0f;
            isGamePause = true;

            isPauseMenuEnable = true;
        }
        else {
            Debug.Log("Warining! Pause menu was already be opened.");
        }
    }

    public void Button_ResumeFromPause()
    {
        if (isPauseMenuEnable) {
            
            if (!TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.OutPause)) return;

            InputManager.InputManagerSin.CanControl = true;
            Time.timeScale = 1f;
            isGamePause = false;

            isPauseMenuEnable = false;
        }
        else {
            Debug.Log("Warining! Pause menu was not be opened.");
        }
    }

    public void Button_ResetFromPause()
    {
        if ( !TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.ResetFromPause) ) return;
        ResetGame();
    }

    public void Signal_ResumeTimeScale()
    {
        if (isGamePause) {
            Time.timeScale = 1f;
            isGamePause = false;
        }
        if (isPauseMenuEnable) {
            isPauseMenuEnable = false;
        }
    }

    public void Button_ResetFromEndMenu()
    {
        if ( !TimelineManager.TimelineManagerSin.PlayTimeline(TimelineManager.TimelineManagerSin.timelineClips.ResetFromEndMenu) ) return;
        ResetGame();
    }


    public void GainPoint(int gain)
    {
        Score += gain;
        UIManager.UIManagerSin.UpdateUI(Score,BestScore);
    }

    public void GameOver()
    {
        InputManager.InputManagerSin.CanControl = false;

        ClearBullets();

        if (Score > BestScore) BestScore = Score;

        UIManager.UIManagerSin.GameOver();
        UIManager.UIManagerSin.UpdateUI(Score, BestScore);

        SaveNLoadManager.SaveNLoadManagerSin.SaveGameData(BestScore);

        Debug.Log("Earth Destroyed! Game Over");

        CameraManager.CameraManagerSin.Shake(ShakeDataTType.Hitted);
    }

    void ClearBullets()
    {
        for (int i = 0; i < BulletSpawnTrans.childCount; i++) {
            BulletSpawnTrans.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Signal_ClearMeteorite()
    {
        StopCoroutine(meteoriteSpawnCoroutine);
        for (int i = 0; i < MeteoriteSpawnTrans.childCount; i++) {
            MeteoriteSpawnTrans.GetChild(i).gameObject.SetActive(false);
        }
        Debug.Log("Clear Meteor!");
    }

    void ResetGame()
    {
        Score = 0;
    }

    public void StoptheGame()
    {
        Debug.Log("Game Stopped.");
        Application.Quit();
    }

    public void Testing_InvisibleBarrierSwitch()
    {
        if (Testing_InvisibleBarrierObj.activeSelf) Testing_InvisibleBarrierObj.SetActive(false);
        else Testing_InvisibleBarrierObj.SetActive(true);

        Debug.Log("Testing object invisiblebarrier has been switched!");
    }

}
