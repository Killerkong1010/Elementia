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
    //public Vector2 spawnLocation;
    private int mobChoice;
    Vector3 originPoint;
    void Start()
    {
        InvokeRepeating("ObjectSpawn", spawnTime, spawnDelay);//used to call a method over and over with an initial time and a delay
        Player = GameObject.FindGameObjectWithTag("PlayerMain").transform;
        
    }


    public void ObjectSpawn()
    {
        var stats = GameObject.FindObjectOfType<PlayerStats>(); // Getting the playerMoney script from the player gameobject
        if (stats.MonstersKilled <= 10)
        {
            //spawnLocation = Random.insideUnitCircle* 5;
            originPoint = Player.gameObject.transform.position;
            originPoint.x += Random.Range(-spawnRadius, spawnRadius);
            originPoint.y += spawnRadius + 5;
            Instantiate(slime, originPoint, transform.rotation);
        }
        else if (stats.MonstersKilled <= 15)
        {
            //stopSpawn = true;
            mobChoice = Random.Range(1, 3);
            originPoint = Player.gameObject.transform.position;
            originPoint.x += Random.Range(-spawnRadius, spawnRadius);
            originPoint.y += spawnRadius + 5;
            if (mobChoice == 1)
            {
                Instantiate(slime, originPoint, transform.rotation);
            }
            else
            {
                Instantiate(turtle, originPoint, transform.rotation);
            }
        }
        else if (stats.MonstersKilled <= 25)
        {
            mobChoice = Random.Range(1, 4);
            originPoint = Player.gameObject.transform.position;
            originPoint.x += Random.Range(-spawnRadius, spawnRadius);
            originPoint.y += spawnRadius + 5;
            if (mobChoice == 1)
            {
                Instantiate(slime, originPoint, transform.rotation);
            }
            else if (mobChoice == 2)
            {
                Instantiate(turtle, originPoint, transform.rotation);
            }
            else if (mobChoice == 3)
            {
                Instantiate(golem, originPoint, transform.rotation);
            }
        }
        if (stopSpawn) //stops 
        {
            CancelInvoke("ObjectSpawn");
        }
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
