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
}
