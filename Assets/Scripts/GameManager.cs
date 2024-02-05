using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager GameManagerSin;
    [SerializeField] MeteoriteSpawnManager meteoriteSpawnManager;
    [SerializeField] Transform BulletSpawnTrans, MeteoriteSpawnTrans;

    [SerializeField] int Score =0/*, Money=0*/;

    void Awake()
    {
        //Singleton
        if (GameManagerSin != null && GameManagerSin != this) Destroy(this);
        else GameManagerSin = this;
     }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    Coroutine meteoriteSpawnCoroutine;

    public void Button_StartGame()
    {
        InputManager.InputManagerSin.CanControl = true;

        UIManager.UIManagerSin.StartGame();
        meteoriteSpawnCoroutine = StartCoroutine( meteoriteSpawnManager.StartSpawningMeteorites() );
    }

    public void GainPoint(int gain)
    {
        Score += gain;
        UIManager.UIManagerSin.UpdateUI(Score);
    }

    public void GameOver()
    {
        InputManager.InputManagerSin.CanControl = false;

        ClearBullets();

        UIManager.UIManagerSin.GameOver();
        Debug.Log("Earth Destroyed! Game Over");
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

    public void Button_ResetGame()
    {
        UIManager.UIManagerSin.Reset();
    }

    public void StoptheGame()
    {
        Debug.Log("Game Stopped.");
        Application.Quit();
    }
}
