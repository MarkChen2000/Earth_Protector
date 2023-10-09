using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager GameManagerSin;
    [SerializeField] MeteoriteSpawnManager meteoriteSpawnManager;
    [SerializeField] Transform BulletSpawnTrans;

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

    public void StarttheGame()
    {
        InputManager.InputManagerSin.CanControl = true;

        UIManager.UIManagerSin.DisableStartMenu();
        meteoriteSpawnCoroutine = StartCoroutine( meteoriteSpawnManager.StartSpawningMeteorites() );
    }

    public void GainPoint(int gain)
    {
        Score += gain;
        UIManager.UIManagerSin.UpdateUI(Score);
    }

    public void GameOver()
    {
        StopCoroutine(meteoriteSpawnCoroutine);
        InputManager.InputManagerSin.CanControl = false;

        for (int i = 0; i < BulletSpawnTrans.childCount; i++) {
            Destroy( BulletSpawnTrans.GetChild(0).gameObject );
        }

        UIManager.UIManagerSin.EnableEndMenu();
        Debug.Log("Earth Destroyed! Game Over");
    }

    public void RestartGame()
    {
        UIManager.UIManagerSin.ReturnToStart();
    }

    public void StoptheGame()
    {
        Debug.Log("Game Stopped.");
        Application.Quit();
    }
}
