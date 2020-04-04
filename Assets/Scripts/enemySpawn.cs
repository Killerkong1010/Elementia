using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject slime;
    public GameObject turtle;
    public GameObject golem;
    public Transform Player;
    public bool stopSpawn = false;
    public float spawnTime;
    public float spawnDelay;
    public float MaxDist = 10;
    public float MinDist = 5;
    public float MoveSpeed = 4;
    public float spawnRadius = 20;
    public int SpawnCount = 0;
    public int SlimeCount = 0;
    public int TurtleCount = 0;
    public int GolemCount = 0;
    //public Vector2 spawnLocation;
    //Vector3 originPoint;
    void Start()
    {
        InvokeRepeating("ObjectSpawn", spawnTime, spawnDelay);//used to call a method over and over with an initial time and a delay
        Player = GameObject.FindGameObjectWithTag("PlayerMain").transform;

    }


    public void ObjectSpawn()
    {
        SpawnCount++;
         var stats = GameObject.FindObjectOfType<PlayerStats>();

        var originPoint = Player.gameObject.transform.position;
        originPoint.x += UnityEngine.Random.Range(-spawnRadius, spawnRadius);
        originPoint.y += spawnRadius + 5;

        var mobChoice = UnityEngine.Random.Range(1, 4);

        //if (rng < 0.5)//|| stats.MonstersKilled <= 10)
        //{
        //    CreateSlime(originPoint, transform);
        //}
        //else if (rng >= 0.5 && rng < 0.8)//|| stats.MonstersKilled <= 25)
        //{
        //    mobChoice = UnityEngine.Random.Range(1, 3);
        //}
        //else if (rng > 0.8) //|| stats.MonstersKilled <= 35)
        //{
        //    mobChoice = UnityEngine.Random.Range(1, 4);
        //}

        switch (mobChoice)
        {
            case 1:
                CreateSlime(originPoint, transform);
                break;
            case 2:
                CreateTurtle(originPoint, transform);
                break;
            case 3:
                CreateGolem(originPoint, transform);
                break;
        }
    }

    private void CreateTurtle(Vector3 originPoint, Transform transform)
    {
        Spawn(turtle, originPoint, transform);
        TurtleCount++;
    }

    private void CreateSlime(Vector3 originPoint, Transform transform)
    {
        Spawn(slime, originPoint, transform);
        SlimeCount++;
    }

    private void CreateGolem(Vector3 originPoint, Transform transform)
    {
        Spawn(golem, originPoint, transform);
        GolemCount++;
    }

    private void Spawn(GameObject enemy, Vector3 originPoint, Transform transform)
    {
        Instantiate(enemy, originPoint, transform.rotation);
    }


    void Update()
    {
        transform.LookAt(Player);
        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            {

            }
        }
    }

}
