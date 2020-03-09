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
    public Text txt;
    public float MoveSpeed = 4;
    public float MaxDist = 10;
    public float MinDist = 5;
    public Image healthBar;
    public float startHealth = 100;
    private float health;
    public float damage = 20;
    private bool isDead = false;
    public int goldDrop;
    void Start()
    {
        health = startHealth;
        Player = GameObject.FindGameObjectWithTag("PlayerMain").transform;
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

            }
        }
    }



    void Die()
    {
        isDead = true;
        Destroy(gameObject);
        
        var stats = GameObject.FindObjectOfType<PlayerStats>(); // Getting the playerMoney script from the player gameobject
        stats.TotalMoney += goldDrop; //adds the value of goldDrop to the total gold of the player
        stats.MonstersKilled++;
        var objGold = GameObject.FindGameObjectsWithTag("txtGold").FirstOrDefault();
        var txtGold = objGold.GetComponent<TextMeshProUGUI>();
        txtGold.SetText(stats.TotalMoney.ToString());
    }

    //void OnTriggerEnter(Collider col)
    //{
    //    if (col.gameObject.CompareTag("Projectile"))
    //    {
    //        Destroy(col);
    //        health -= 15;
    //        healthBar.fillAmount = health / startHealth;

    //    }


    //    if (health <= 0 && !isDead)
    //    {
    //        Die();
    //    }
    //}


    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }
}
