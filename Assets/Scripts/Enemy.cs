using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro; //UI text component
using UnityEngine;
using UnityEngine.UI; //Used to access UI components
// Import Libraries


public class Enemy : MonoBehaviour
{
    //Storing the x,y,z coordinates of the variable.
    public Transform Player;
    public Transform deathPoint;
    // Storing the variables as gameobjects
    GameObject deathVFX;
    GameObject rockVFX;
    GameObject turtleHitVFX;
    GameObject slimeHitVFX;
    // Storing the variables as public gameobjects - Can be accessed in other scripts / in the Unity Editor.
    public GameObject golem;
    public GameObject turtle;
    public GameObject slime;
    // Defining a public text variable.
    public Text txt;
    //Defining public floats that can be changed in other scripts / Unity Editor when testing.
    public float slowDuration = 5;
    public float MoveSpeed = 4;
    public float MaxDist = 10;
    public float MinDist = 5;
    // Accessing the healthBar GameObject that will be modified to show the health of the monster.
    public Image healthBar;
    public float startHealth = 100; // Setting the start health of the monster.
    private float health;
    public float damage = 20;
    private bool isDead = false; //Boolean to determine whether the monster is alive or dead.
    private bool slowed; // Boolean to determine whether the monster is slowed or not.
    private bool isBlue;
    private DateTime slowedAt; // DateTime variable used to determine the time at which the monster was slowed at.
    public int goldDrop;
    void Start() // Start function is called once at the beginning of runtime.
    {
        health = startHealth; // Setting the health variable to be equal to the StartHealth.
        Player = GameObject.FindGameObjectWithTag("PlayerMain").transform; // Accessing the transform (x,y,z) of the player by searching for the tag "PlayerMain"
        deathVFX = Resources.Load("deathEffect") as GameObject; // Loading the VFX from the resources folder and storing them as gameobjects.
        rockVFX = Resources.Load("rockHit") as GameObject; // Loading the VFX from the resources folder and storing them as gameobjects.
        turtleHitVFX = Resources.Load("turtleHit") as GameObject; // Loading the VFX from the resources folder and storing them as gameobjects.
        slimeHitVFX = Resources.Load("miscHit") as GameObject; // Loading the VFX from the resources folder and storing them as gameobjects.
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player); //Uses a unity function that rotates the transform so that it faces the players current position.
        if (Vector3.Distance(transform.position, Player.position) >= MinDist) //If the player is too far away to attack
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime; // Sets the transform of the monster so that it moves towards the player at a dictated speed.

            if (Vector3.Distance(transform.position, Player.position) <= MaxDist) // If the monster is far away from the player.
            {
                //range attack here
            }
        }
        CheckEffects(); // Calls the CheckEffects() function
        SlowColorChange(); // Calls the colour change function.
    }



    void Die() // Function called when the monster's health reaches 0.
    {
        isDead = true; // Sets the boolean isDead to be true.
        Destroy(gameObject); // Destroys the gameobject, removing it from the world space.

        GameObject deathEffect = Instantiate(deathVFX) as GameObject;// Instantiates the deathEffect gameobject 
        deathEffect.transform.position = deathPoint.transform.position; // Sets the transform of the deathEffect gameobject to be a point in the monsters body.

        var stats = GameObject.FindObjectOfType<PlayerStats>(); // Getting the playerMoney script from the player gameobject
        stats.TotalMoney += goldDrop; //adds the value of goldDrop to the total gold of the player
        stats.MonstersKilled++; // Increments the MonstersKilled integer by 1.
        var objGold = GameObject.FindGameObjectsWithTag("txtGold").FirstOrDefault(); //Retrieving the UI text component that displays the gold by searching for its tag.
        var txtGold = objGold.GetComponent<TextMeshProUGUI>(); // Gets the text component so that it can be edited.
        var objScore = GameObject.FindGameObjectsWithTag("ScoreNum").FirstOrDefault(); //Retrieving the UI text component that displays the score by searching for its tag.
        var txtScore = objScore.GetComponent<TextMeshProUGUI>();// Gets the text component so that it can be edited.
        txtGold.SetText(stats.TotalMoney.ToString()); // Sets the text on the UI to the current gold, changing the integer to a string
        txtScore.SetText(stats.MonstersKilled.ToString()); // Sets the textt on the UI to the current score, changing the integer to a string.
    }

    private void OnTriggerEnter(Collider col) // Function that is called once a trigger enters the monsters collider component.
    {
        if (col.gameObject.CompareTag("Fireball")) // If the collider that entered the monsters' collider belongs to the gameobject with the tag "Fireball"
        {
            health -= 50; // Subtracts the health of the monster by the damage of the fireball spell
            healthBar.fillAmount = health / startHealth; // Adjusts the fillAmount (amount of the healthbar that is full) to accomodate the change in health. Represented by a % of health 
            hitEffects(); // Applies any hit effects depending on the monster that is hit.

            if (health <= 0 && !isDead) // If the health of the monster is less than or equal to 0 and isDead is false then
            {
                Die(); //Die function is called.
            }
        }
        if (col.gameObject.CompareTag("FrostWave")) // If the collider that entered the monsters' collider belongs to the gameobject with the tag "FrostWave"
        {
            health -= 20; // Subtracts the health of the monster by the damage of the FrostWave spell
            healthBar.fillAmount = health / startHealth; // Adjusts the fillAmount (amount of the healthbar that is full) to accomodate the change in health. Represented by a % of health 
            hitEffects(); // Applies any hiteffects
            slowed = true; // Sets the boolean slowed to be true
            isBlue = true; // Sets the boolean isBlue to be true
            slowedAt = DateTime.Now; // Calculates the time at which the monster was slowed
            MoveSpeed = MoveSpeed / 2; // Reduces the movement speed by 50% of the current movespeed.
            if (health <= 0 && !isDead) // If the health of the monster is less than or equal to 0 and isDead is false
            {
                Die(); //Die function is called.
            }
        }
    }


    public void Damage(int damageAmount) // Damage function that is called in other scripts.
    {
        health -= damageAmount; // Subtracts the health of the monster by the integer that is passed in (damageAmount)
        healthBar.fillAmount = health / startHealth; // Adjusts the healthbar to accomodate the change in health.
        hitEffects(); // Applies any hiteffects
        if (health <= 0 && !isDead) // If the health of the monster is less than or equal to 0 and isDead is false
        {
            Die();//Die function is called.

        }
    }

    public void hitEffects() // function that is called once a monster is hit with a spell
    {
        if (startHealth == 500) // Determines between the differnt monsters (Golem = 500hp, Turtle = 200hp, Slime = 100hp)
        {
            GameObject rockHit = Instantiate(rockVFX) as GameObject; // Instantiates the rock hit effect as a gameobject
            rockHit.transform.position = deathPoint.transform.position; // Sets the rockHit effect's transform to be at the deathPoint.
        }
        else if (startHealth == 200)// Determines between the differnt monsters (Golem = 500hp, Turtle = 200hp, Slime = 100hp)
        {
            GameObject turtleHit = Instantiate(turtleHitVFX) as GameObject;// Instantiates the turtleHit effect as a gameobject
            turtleHit.transform.position = deathPoint.transform.position; // Sets the turtleHit effect's transform to be at the deathPoint.
        }
        else if (startHealth == 100)// Determines between the differnt monsters (Golem = 500hp, Turtle = 200hp, Slime = 100hp)
        {
            GameObject slimeHit = Instantiate(slimeHitVFX) as GameObject; // Instantiates the slimeHit effect as a gameobject 
            slimeHit.transform.position = deathPoint.transform.position; // Sets the slimeHit effect's transform to be at the deathPoint.
        }
    }

    private void CheckEffects() //function that is called within the update function.
    { // used to determine whether effects should still be applied.
        if(slowed && DateTime.Now.Subtract(slowedAt).TotalSeconds > slowDuration) // Determines whether the monster should still be slowed.
        {
            MoveSpeed = MoveSpeed * 2; // Sets the movement speed to be its original amount.
            slowed = false; // Changes the boolean slowed to be false.
        }
    }
    private void SlowColorChange() // Function that is called once per frame 
    {
        if (isBlue) // if isBlue == true
        {
            if (startHealth == 500) // Determines between the different mosnters (Golem = 500hp, Turtle = 200hp, Slime = 100hp)
            {
                var golemRenderer = golem.GetComponent<Renderer>();// Gets the renderer component of the Golem gameobject.
                golemRenderer.material.SetColor("Albedo", Color.blue);// Changes the albedo (color) to blue.
            }
            if (startHealth == 200) // Determines between the different mosnters (Golem = 500hp, Turtle = 200hp, Slime = 100hp)
            {
                var turtleRenderer = turtle.GetComponent<Renderer>(); // Gets the renderer component of the turtle gameobject.
                turtleRenderer.material.SetColor("Albedo", Color.blue); // Changes the albedo (color) to blue.
            }
            if (startHealth == 100) // Determines between the different mosnters (Golem = 500hp, Turtle = 200hp, Slime = 100hp)
            {
                var slimeRenderer = slime.GetComponent<Renderer>();// Gets the renderer component of the Slime gameobject.
                slimeRenderer.material.SetColor("Albedo", Color.blue);// Changes the albedo (color) to blue.
            }
        }
        
    }
}
