using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

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
    [SerializeField] TextMeshProUGUI ComboText;
    [SerializeField] GameObject GameOverOverlay;
    [SerializeField] GameObject HelpWindow;

    [Header("Timer")]
    public float levelTimer = 0.0f;

    [Header("ScorePropertie")]
    public int spawnCustomer = 0;
    public int passCustomer = 0;
    public int passDessertNum = 0;
    public int combo = 0;
    public int missInput = 0;
    [SerializeField]
    int score = 0;

    [Header("LevelData")]
    [SerializeField]
    List<GameData> gameDatas = new List<GameData>();
    [Space(10)]
    public int maxDessertNum;
    public int minDessertNum;
    public float regenTime;
    public float panaltyTime;
    public float defaultDecreaseTime;

    int spawnLevelIndex = 0;
    int timerLevelIndex = 0;

    public bool isGameOver = false;
    public bool isGamePause = false;

    void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameOverOverlay.SetActive(false);

        Debug.Log(gameDatas[spawnLevelIndex].minDessertNum);

        maxDessertNum = gameDatas[spawnLevelIndex].maxDessertNum;
        minDessertNum = gameDatas[spawnLevelIndex].minDessertNum;
        regenTime = gameDatas[timerLevelIndex].regenTime;
        panaltyTime = gameDatas[timerLevelIndex].panaltyTime;
        defaultDecreaseTime = gameDatas[timerLevelIndex].defaultDecreaseTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(isGameOver || isGamePause))
        {
            levelTimer -= Time.deltaTime * defaultDecreaseTime;

            levelTimer = levelTimer <= 0 ? 0 : levelTimer;

            if (spawnLevelIndex < gameDatas.Count) 
                if (spawnCustomer >= gameDatas[spawnLevelIndex].costomerCount)
                {
                    spawnLevelIndex++;

                    if (spawnLevelIndex < gameDatas.Count)
                    {
                        maxDessertNum = gameDatas[spawnLevelIndex].maxDessertNum;
                        minDessertNum = gameDatas[spawnLevelIndex].minDessertNum;
                    }
                }

            if (timerLevelIndex < gameDatas.Count) 
                if (passCustomer >= gameDatas[timerLevelIndex].costomerCount)
                {
                    timerLevelIndex++;

                    if (timerLevelIndex < gameDatas.Count)
                    {
                        regenTime = gameDatas[timerLevelIndex].regenTime;
                        panaltyTime = gameDatas[timerLevelIndex].panaltyTime;
                        defaultDecreaseTime = gameDatas[timerLevelIndex].defaultDecreaseTime;
                    }
                }

            score = passCustomer * 500 * combo
                + passDessertNum * 100 * timerLevelIndex
                - missInput * 50;

            score = score <= 0 ? 0 : score;
        }

        if (levelTimer <= 0)
        {
            isGameOver = true;
        }

        if (isGameOver)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Scenes/MenuScene");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isGamePause = !isGamePause;
                
            }
        }

        UpdateUI();
    }

   
    void UpdateUI()
    {
        if (isGameOver)
        {
            HelpWindow.SetActive(false);
            GameOverOverlay.SetActive(true);
        }

        TimerBar.value = levelTimer;

        string str = "" + (levelTimer);
        string str1 = str;
        if (str.Contains('.'))
        {
            str1 = str.Substring(0, str.IndexOf('.'));
        }
        TimerText.text = str1;

        LevelText.text = "" + (timerLevelIndex + 1);
        ScoreText.text = Convert.ToString(("" + (score))).PadLeft(6, '0');
        ComboText.text = combo == 0 ? "" : "" + (combo);
        HelpWindow.SetActive(isGamePause);
    }
}
