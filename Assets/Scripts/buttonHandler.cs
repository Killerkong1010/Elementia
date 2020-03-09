using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(0);
            Debug.Log("m key pressed");
        }

    }
}
