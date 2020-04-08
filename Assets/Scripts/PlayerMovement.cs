using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController character_Controller; // Uses the character controller Unity function.
    private Vector3 move_Direction; // Holds the x,y,z under move_Direction.

    public float speed = 5f;
    private float gravity = 20f;
    public float jump_Force = 10f;
    private float vertical_Velocity;
    public bool sprinting;
    //Sprintbar
    private Timer oneSecondTimer; // Timer that is used to determine whenever a second has passed.
    public PlayerEnergy energybar; // Accesses the energybar of the player.

    void Start() // Called once at the start of runtime.
    {
        //create a timer and fire it ever 1000ms (1 second)
        oneSecondTimer = new Timer(1000);
        oneSecondTimer.Start(); //Starts the timer
        //call the OneSecondTimer_Elapsed every time the timer fires
        oneSecondTimer.Elapsed += OneSecondTimer_Elapsed;
    }

    private void OneSecondTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        //this will fire every second
        energybar.EnergyRegen(); //Uses the EnergyRegen function from the PlayerEnergy script.
        if (sprinting)//Determines if the player is sprinting = If true then
            energybar.DrainEnergy(); //Drains the energy of the player using the DrainEnergy function from the PlayerEnergy script.

        //make sure the energy bar is updated
        energybar.Update();
    }

    void Awake()
    {
        character_Controller = GetComponent<CharacterController>(); // Gets the CharacterController component from the player.
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer(); //Calls the MovePlayer function.
    }
    void MovePlayer()
    {
        move_Direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")); //Gets the horizontal and vertical axis.
        move_Direction = transform.TransformDirection(move_Direction); // Gets the transform (x,y,z) of the move_Direction variable using the unity function TransformDirection.
        move_Direction *= speed * Time.deltaTime; // Calculates the new transform of move_Direction dictated by the speed and the Time.deltatime.
        //Time.deltatime = Time between the current and previous frame.

        ApplyGravity(); // Calls the apply gravity function

        Sprint(); // Calls the sprint function.

        character_Controller.Move(move_Direction); //Utilises Unity's move function.
    }
    void ApplyGravity()
    {
        vertical_Velocity -= gravity * Time.deltaTime; //Calculates the vertical velocity using gravity ant time.deltatime

        PlayerJump(); // Calls the jump function.

        move_Direction.y = vertical_Velocity * Time.deltaTime; // Changes the Y by the vertical_Velocity and time.deltatime

    }
    void PlayerJump()
    { //Checks to see whether the player is grounded, if they are and the space key is down, the vertical velocity (up) will change to be the jumpForce moving the character up. 
        if (character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_Velocity = jump_Force; // Changes the vertical velocity to equal the jump_Force.
        }

    }
    void Sprint() // Sprint function that makes the player move faster
    {
        if (Input.GetKey(KeyCode.LeftShift) && energybar.HasEnergy) // If the player presses shift and has energy.
        {
            speed = 7.5f; // Sets the speed to 7.5f, faster than normal speed which is 5
            sprinting = true; // Sprinting is now true
        }
        else
        {
            sprinting = false; // Sets sprinting to false
            speed = 5f; // Sets the speed back to the original 5f.
        }

    }
}
