using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;
    public Transform deathPoint;
    GameObject deathVFX;
    GameObject rockVFX;
    GameObject turtleHitVFX;
    GameObject slimeHitVFX;
    public GameObject golem;
    public GameObject turtle;
    public GameObject slime;
    public Text txt;

    public float slowDuration = 5;
    public float MoveSpeed = 4;
    public float MaxDist = 10;
    public float MinDist = 5;

    public Image healthBar;
    public float startHealth = 100;
    private float health;
    public float damage = 20;
    private bool isDead = false;
    private bool slowed;
    private bool isBlue;
    private DateTime slowedAt;
    public int goldDrop;
    void Start()
    {
        health = startHealth;
        Player = GameObject.FindGameObjectWithTag("PlayerMain").transform;
        deathVFX = Resources.Load("deathEffect") as GameObject;
        rockVFX = Resources.Load("rockHit") as GameObject;
        turtleHitVFX = Resources.Load("turtleHit") as GameObject;
        slimeHitVFX = Resources.Load("miscHit") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            {
                //range attack here
            }
        }
        CheckEffects();
        SlowColorChange();
    }



    void Die()
    {
        isDead = true;
        Destroy(gameObject);

        GameObject deathEffect = Instantiate(deathVFX) as GameObject;
        deathEffect.transform.position = deathPoint.transform.position;
        var ps = deathEffect.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule ma = ps.main;
        ma.startColor = new Color(255, 10, 10);


        var stats = GameObject.FindObjectOfType<PlayerStats>(); // Getting the playerMoney script from the player gameobject
        stats.TotalMoney += goldDrop; //adds the value of goldDrop to the total gold of the player
        stats.MonstersKilled++;
        var objGold = GameObject.FindGameObjectsWithTag("txtGold").FirstOrDefault();
        var txtGold = objGold.GetComponent<TextMeshProUGUI>();
        var objScore = GameObject.FindGameObjectsWithTag("ScoreNum").FirstOrDefault();
        var txtScore = objScore.GetComponent<TextMeshProUGUI>();
        txtGold.SetText(stats.TotalMoney.ToString());
        txtScore.SetText(stats.MonstersKilled.ToString());
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Fireball"))
        {
            health -= 50;
            healthBar.fillAmount = health / startHealth;
            hitEffects();

            if (health <= 0 && !isDead)
            {
                Die();
            }
        }
        if (col.gameObject.CompareTag("FrostWave"))
        {
            health -= 20;
            healthBar.fillAmount = health / startHealth;
            hitEffects();
            slowed = true;
            isBlue = true;
            slowedAt = DateTime.Now;
            MoveSpeed = MoveSpeed / 2;
            if (health <= 0 && !isDead)
            {
                Die();
            }
        }
    }


    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.fillAmount = health / startHealth;
        hitEffects();
        if (health <= 0 && !isDead)
        {
            Die();

        }
    }

    public void hitEffects()
    {
        if (startHealth == 500)
        {
            GameObject rockHit = Instantiate(rockVFX) as GameObject;
            rockHit.transform.position = deathPoint.transform.position;
        }
        else if (startHealth == 200)
        {
            GameObject turtleHit = Instantiate(turtleHitVFX) as GameObject;
            turtleHit.transform.position = deathPoint.transform.position;
        }
        else if (startHealth == 100)
        {
            GameObject slimeHit = Instantiate(slimeHitVFX) as GameObject;
            slimeHit.transform.position = deathPoint.transform.position;
        }
    }

    private void CheckEffects()
    {
        if(slowed && DateTime.Now.Subtract(slowedAt).TotalSeconds > slowDuration)
        {
            MoveSpeed = MoveSpeed * 2;
            slowed = false;
        }
    }
    private void SlowColorChange()
    {
        if (isBlue)
        {
            if (startHealth == 500)
            {
                var golemRenderer = golem.GetComponent<Renderer>();
                golemRenderer.material.SetColor("Albedo", Color.blue);
            }
            if (startHealth == 200)
            {
                var turtleRenderer = turtle.GetComponent<Renderer>();
                turtleRenderer.material.SetColor("Albedo", Color.blue);
            }
            if (startHealth == 100)
            {
                var slimeRenderer = slime.GetComponent<Renderer>();
                slimeRenderer.material.SetColor("Albedo", Color.blue);
            }
        }
        
    }
}
