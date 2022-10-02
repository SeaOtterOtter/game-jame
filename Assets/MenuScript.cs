using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject HelpWindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            HelpWindow.SetActive(false);
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Scenes/PlayScene");
    }

    public void GameHelp()
    {
        HelpWindow.SetActive(true);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
