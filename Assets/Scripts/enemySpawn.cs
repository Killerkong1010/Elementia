using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    // Accesses the monster gameobjects
    public GameObject slime;
    public GameObject turtle;
    public GameObject golem;
    //Finds the transform (x,y,z) of the player.
    public Transform Player;
    //Boolean dictating whether spawning is on or off
    public bool stopSpawn = false;
    //Floats that hold variables
    public float spawnTime;
    public float spawnDelay;
    public float MaxDist = 10;
    public float MinDist = 5;
    public float MoveSpeed = 4;
    public float spawnRadius = 20;
    //Integers that hold the number of slimes, turtles and golems. Also holds the number of total spawns.
    public int SpawnCount = 0;
    public int SlimeCount = 0;
    public int TurtleCount = 0;
    public int GolemCount = 0;
    //public Vector2 spawnLocation;
    //Vector3 originPoint;
    void Start()//Start is called at the beginning of runtime.
    {
        InvokeRepeating("ObjectSpawn", spawnTime, spawnDelay);//used to call a method over and over with an initial time and a delay. Calling the "ObjectSpawn" method repeatedly with a delay.
        Player = GameObject.FindGameObjectWithTag("PlayerMain").transform; //Finds the GameObjects with the tag "Playermain". Then finds its transform

    }


    public void ObjectSpawn() //Objectspawn method that is called with delay.
    {
        SpawnCount++; //Adds 1 to the spawncount integer
         var stats = GameObject.FindObjectOfType<PlayerStats>(); //Accesses the PlayerStats script that holds the player's money.

        var originPoint = Player.gameObject.transform.position; // Sets the origin point for spawns where the Player's transform is
        originPoint.x += UnityEngine.Random.Range(-spawnRadius, spawnRadius); //Adds a random range to the x of the originPoint
        originPoint.y += spawnRadius + 5; //Increases the Y of the origin point so that the enemy doesn't spawn under the floor 

        var mobChoice = UnityEngine.Random.Range(1, 4); //Selects a random number between 1 and 4.

        switch (mobChoice) //Switch statement that chooses a single switch section to execute based on a pattern match with the match expression
        { //In this case the match expression is mobChoice
            case 1: //If the mobchoice matches case 1 then a slime is created
                CreateSlime(originPoint, transform);
                break;
            case 2:
                CreateTurtle(originPoint, transform); //Creates a turtle
                break;
            case 3:
                CreateGolem(originPoint, transform); //Creates a golem
                break;
        }
    }

    private void CreateTurtle(Vector3 originPoint, Transform transform) //Function that is called in the switch statement and spawns a turtle
    { //Requires the transform and the originpoint
        Spawn(turtle, originPoint, transform); //Spawns the turtle into the game using the Spawn() function.
        TurtleCount++; //Adds 1 to the total number of turtles
    }

    private void CreateSlime(Vector3 originPoint, Transform transform) //Function that is called in the switch statement and spawns a slime
    {//Requires the transform and the originpoint
        Spawn(slime, originPoint, transform);//Spawns the slime into the game using the Spawn() function.
        SlimeCount++;
    }

    private void CreateGolem(Vector3 originPoint, Transform transform) //Function that is called in the switch statement and spawns a golem
    {//Requires the transform and the originpoint
        Spawn(golem, originPoint, transform);//Spawns the golem into the game using the Spawn() function.
        GolemCount++;
    }

    private void Spawn(GameObject enemy, Vector3 originPoint, Transform transform)
    {
        Instantiate(enemy, originPoint, transform.rotation); //Instantiates the enemy into the game at the originpoint, with the transform.rotation.
    }


    void Update() //Called once per frame
    {
        transform.LookAt(Player); //Uses the unity LookAt function to adjust the transform so that it faces the desired transform in this case the player's transform
        if (Vector3.Distance(transform.position, Player.position) >= MinDist) //Used to determine whether the monster is within melee range
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime; //Moves the monster forward towards the player.

            if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            {

            }
        }
    }

}
