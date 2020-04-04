using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorUnlock : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor();
    }
    void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}

