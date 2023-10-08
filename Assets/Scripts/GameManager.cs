using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager GameManagerSin;
    [SerializeField] MeteoriteSpawnManager meteoriteSpawnManager;

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

    public void StarttheGame()
    {
        UIManager.UIManagerSin.DisableMenu();
        StartCoroutine( meteoriteSpawnManager.StartSpawningMeteorites() );
    }

    public void GainPoint(int gain)
    {
        Score += gain;
        UIManager.UIManagerSin.UpdateUI(Score);
    }

    public void EarthDestroyed()
    {
        Time.timeScale = 0f;
        Debug.Log("Earth Destroyed! Game Stopped");
    }

    public void StoptheGame()
    {
        Debug.Log("Game Stopped.");
        Application.Quit();
    }
}
