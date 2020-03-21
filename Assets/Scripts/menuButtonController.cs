﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public Button startGameBtn;
    public Button settings;
    public Button quitGameBtn;
    void Start()
    {
        Button btn = startGameBtn.GetComponent<Button>();
        btn.onClick.AddListener(StartGame);
        Button btn1 = quitGameBtn.GetComponent<Button>();
        btn1.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    void ExitGame()
    {
        Application.Quit();
    }
    void Update()
    {
        
    }
}
