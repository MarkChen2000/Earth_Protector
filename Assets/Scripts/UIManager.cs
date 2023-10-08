using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public static UIManager UIManagerSin;

    [SerializeField] GameObject StartMenu;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupUI()
    {
        TMPText_Score.text = 0.ToString();
    }

    public void DisableMenu()
    {
        StartMenu.SetActive(false);
    }

    public void UpdateUI(int score)
    {
        TMPText_Score.text = score.ToString();
    }
}
