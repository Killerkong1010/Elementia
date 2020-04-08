using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBar; //Accesses the Image 
    public float health; //Float that holds the health value
    public float startHealth; //Float that holds the start health value

    public void onTakeDamage(int damage) //onTakeDdamage function that is called when the player comes in contact with a monster collider.
    {
        health = health - damage; //Calculates the health of player after taking damage.
        healthBar.fillAmount = health / startHealth; // Adjusts the fillamount of the healtbar slider.
        if (health <= 0) //If the player's health is less than or equal to 0
        {
            SceneManager.LoadScene(2); //Loads the death scene.
        }
    }

    public void onHeal(int heal) //onHeal function that is called after healing.
    {
        if (health < 100) // If the health is more than 100
        {
            var difference = 100 - health; // Calculates the difference between the current health and the max health
            if (heal > difference) // if the heal amount is more than the difference 
                health = 100; // Sets the health to 100
            else 
                health = health + heal; // Sets the health to be equal to health + heal amount

            healthBar.fillAmount = health / startHealth; //Adjusts the healthbar fillamount.

        }

    }

}