using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerEnergy : MonoBehaviour
{
    public Image energyBar; // Accesses the energy bar
    public float energy; // Float that holds the current energy
    public float startEnergy; // float that holds the start energy
    public float energyRegeneration; // Float that holds the amount of energy regenerated per second
    public float energyDrain; // Float that holds the amount of energy taken for each second sprinting.
    public bool HasEnergy { get { return energy > 0; } } // Determines whether the player has energy and is able to sprint.

    public void OnEnergyHeal(int energyHeal) // OnEnergyHeal function that is called in another script
    {
        if (energy < 100) // If the energy is less than 100.
        {
            var difference = 100 - energy; // Calculates the difference between 100 and the current energy
            if (energyHeal > difference) // if the EnergyHeal is bigger than the difference between 100 and the current energy
                energy = 100; // Sets the energy to 100
            else
                energy = energy + energyHeal; // Otherwise, the energy is set to energy + energyHeal

            Update(); //Updates the energybar to adjust to the changes.

        }

    }

    public void EnergyRegen() // Function called whenever not sprinting
    {
        var difference = 100 - energy; // Calculates the difference between max energy (100) and the current energy
        if (energyRegeneration > difference) // If the amount regenerated is bigger than the difference 
        {
            energy = 100; // Sets energy to 100
        }
        else // If the amount regenerated is less than the difference 
        {
            energy += energyRegeneration; // Sets the energy to equal energy + energyRegeneration.
        }
    }

    public void DrainEnergy()//Function that is called whenever the player sprints in the PlayerMovement Script
    {
        energy = energy - energyDrain; // Sets the energy to equal the energy - the energy drain
    }

    internal void Update() // Function called in this script and other scripts
    {
        energyBar.fillAmount = energy / startEnergy; // Changes the fillAmount (% of the bar filled) of the energy bar
    }
}