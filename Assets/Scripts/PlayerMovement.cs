﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController character_Controller;
    private Vector3 move_Direction;

    public float speed = 5f;
    private float gravity = 20f;
    public float jump_Force = 10f;
    private float vertical_Velocity;

    void Awake()
    {
        character_Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        move_Direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")); //Vector3 takes 3 arguements
        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;

        ApplyGravity();
        Sprint();
        character_Controller.Move(move_Direction); //Utilises Unity's move function.
    }
    void ApplyGravity()
    {
        vertical_Velocity -= gravity * Time.deltaTime;

        PlayerJump();

        move_Direction.y = vertical_Velocity * Time.deltaTime;

    }
    void PlayerJump()
    { //Checks to see whether the player is grounded, if they are and the space key is down, the vertical velocity (up) will change to be the jumpForce moving the character up. 
        if (character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_Velocity = jump_Force;
        }

    }
    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 7.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
        }
    }

   
}
