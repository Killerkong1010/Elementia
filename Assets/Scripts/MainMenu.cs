﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isStart;
    public bool isQuit;
    void OnMouseUp()
    {
        if (isStart)
        {
            SceneManager.LoadScene(1);
        }
        if (isQuit)
        {
            Application.Quit();
        }
    }

 

}
