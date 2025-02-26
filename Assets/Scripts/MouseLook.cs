﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot;
    [SerializeField]
    private Transform lookRoot;
    [SerializeField]
    private bool invert;
    [SerializeField]
    private bool can_Unlock = true; //unlocks cursor to allow button pressing
    [SerializeField]
    private float sensitivity = 5f;
    [SerializeField]
    private int smooth_Steps = 10;
    [SerializeField]
    private float smooth_Weight = 0.4f;
    [SerializeField]
    private float roll_Angle = 10f;
    [SerializeField]
    private float roll_Speed = 3f;
    [SerializeField]
    private Vector2 default_Look_Limits = new Vector2(-70, 80f);

    private Vector2 look_Angles;

    private Vector2 current_Mouse_Look;
    private Vector2 smooth_Move;
    private float current_Roll_Angle;
    private int last_Look_Frame;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor();
        if (Cursor.lockState == CursorLockMode.Locked){
            LookAround();
        }
    }
    void LockAndUnlockCursor(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (Cursor.lockState == CursorLockMode.Locked){
                Cursor.lockState = CursorLockMode.None;

            }
            else{
              Cursor.lockState = CursorLockMode.Locked;
              Cursor.visible = false;  
            }
        }

    } //lock and unlock cursor
    void LookAround(){
        current_Mouse_Look = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")); //Vector 2 takes two arguments (Left, Right / Up, Down)
        look_Angles.x += current_Mouse_Look.x * sensitivity * (invert ? 1f : -1f); //invert - If invert is true use 1f, otherwise use -1f values
        look_Angles.y += current_Mouse_Look.y * sensitivity;
        look_Angles.x = Mathf.Clamp(look_Angles.x, default_Look_Limits.x, default_Look_Limits.y); //Wont allow look_Angles.x to go above either other values

        //current_Roll_Angle = Mathf.Lerp(current_Roll_Angle, Input.GetAxisRaw("Mouse X") * roll_Angle, Time.deltaTime * roll_Speed);
                //Used to create a dizzy/drunk effect - could be useful
                //Causes rotation on the z axis.
            //Lerp = Linearly interpolates between a and b
            //GetAxisRaw will increase instantly, not incrementally - better for mouse movement.

        lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, 0f);
        playerRoot.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f);
    }//look around
}//class
