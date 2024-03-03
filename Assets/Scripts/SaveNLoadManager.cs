using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveNLoadManager : MonoBehaviour
{

    [HideInInspector] public static SaveNLoadManager SaveNLoadManagerSin;

    void Awake()
    {
        //Singleton
        if (SaveNLoadManagerSin != null && SaveNLoadManagerSin != this) Destroy(this);
        else SaveNLoadManagerSin = this;
        
        InitializeGameData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Testing_ClearGameData(); 
    }

    //Because this function excute at awake, but to prevent singleton of GameManager didnt be created at this point, adjusted the script order of these two scripts.
    void InitializeGameData()
    {
        if (!PlayerPrefs.HasKey("HasRecord")) {
            PlayerPrefs.SetInt("HasRecord", 1);
            PlayerPrefs.SetInt("BestScore", 0);
            Debug.Log("Created new game data.");
        }
        else {
            GameManager.GameManagerSin.BestScore = PlayerPrefs.GetInt("BestScore");
            Debug.Log("Load an existed game data.");
        }
    }

    public void SaveGameData(int bestScore)
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.Save();
        Debug.Log("Game data saved.");
    }

    void Testing_ClearGameData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Warning! All the game data gave been deleted!!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
