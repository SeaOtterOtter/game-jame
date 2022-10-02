using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public struct GameData
{
    public int costomerCount;
    public int maxDessertNum;
    public int minDessertNum;
    public float regenTime;
    public float panaltyTime;
    public float defaultDecreaseTime;
}

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    public MonsterManager monsterManager;

    [Header("GamObject")]
    public GameObject player;
    public GameObject ground;

    [Header("UI")]
    [SerializeField] GameObject Timer;
    [SerializeField] Slider TimerBar;
    [SerializeField] TextMeshProUGUI TimerText;
    [SerializeField] TextMeshProUGUI LevelText;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI CustomerText;
    [SerializeField] GameObject GameOverOverlay;

    [Header("Timer")]
    public float levelTimer = 0.0f;

    [Header("ScorePropertie")]
    public int spawnCustomer = 0;
    public int passCustomer = 0;
    public int passDessertNum = 0;
    public int combo = 0;
    [SerializeField]
    int score = 0;

    [SerializeField]
    List<GameData> gameDatas = new List<GameData>();

    public int maxDessertNum;
    public int minDessertNum;
    public float regenTime;
    public float panaltyTime;
    public float defaultDecreaseTime;

    int currentLevelIndex = 0;
    int Level = 0;

    bool isGameOver = false;

    void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameOverOverlay.SetActive(false);

        Debug.Log(gameDatas[currentLevelIndex].minDessertNum);

        maxDessertNum = gameDatas[currentLevelIndex].maxDessertNum;
        minDessertNum = gameDatas[currentLevelIndex].minDessertNum;
        regenTime = gameDatas[Level].regenTime;
        panaltyTime = gameDatas[Level].panaltyTime;
        defaultDecreaseTime = gameDatas[Level].defaultDecreaseTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelTimer <= 0)
        {
            isGameOver = true;
        }

        levelTimer -= Time.deltaTime * defaultDecreaseTime;

        if (spawnCustomer >= gameDatas[currentLevelIndex].costomerCount)
        {
            currentLevelIndex++;

            maxDessertNum = gameDatas[currentLevelIndex].maxDessertNum;
            minDessertNum = gameDatas[currentLevelIndex].minDessertNum;
            
        }

        if (passCustomer >= gameDatas[Level].costomerCount)
        {
            Level++;

            regenTime = gameDatas[Level].regenTime;
            panaltyTime = gameDatas[Level].panaltyTime;
            defaultDecreaseTime = gameDatas[Level].defaultDecreaseTime;
        }

        UpdateUI();
    }

   
    void UpdateUI()
    {
        if (isGameOver)
        {
            GameOverOverlay.SetActive(true);
        }
        else
        {
            TimerBar.value = levelTimer;
            string str = "" + (levelTimer);
            string str1 = str.Substring(0, str.IndexOf('.'));
            TimerText.text = str1;
            LevelText.text = "" + (Level + 1);
            ScoreText.text = "" + (score);
            CustomerText.text = "" + (passCustomer);
        }
    }
}
