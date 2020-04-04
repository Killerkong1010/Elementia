using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;
    public float health;
    public float startHealth;

    public void onTakeDamage(int damage)
    {
        health = health - damage;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void onHeal(int heal)
    {
        if (health < 100)
        {
            var difference = 100 - health;
            if (heal > difference)
                health = 100;
            else
                health = health + heal;

            healthBar.fillAmount = health / startHealth;

        }

    }

}