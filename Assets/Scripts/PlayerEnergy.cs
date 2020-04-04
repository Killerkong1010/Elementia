using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerEnergy : MonoBehaviour
{
    public Image energyBar;
    public float energy;
    public float startEnergy;
    public float energyRegeneration;
    public float energyDrain;
    public bool HasEnergy { get { return energy > 0; } }

    public void OnEnergyHeal(int energyHeal)
    {
        if (energy < 100)
        {
            var difference = 100 - energy;
            if (energyHeal > difference)
                energy = 100;
            else
                energy = energy + energyHeal;

            Update();

        }

    }

    public void EnergyRegen()
    {
        var difference = 100 - energy;
        if (energyRegeneration > difference)
        {
            energy = 100;
        }
        else
        {
            energy += energyRegeneration;
        }
    }

    public void DrainEnergy()
    {
        energy = energy - energyDrain;
    }

    internal void Update()
    {
        energyBar.fillAmount = energy / startEnergy;
    }
}