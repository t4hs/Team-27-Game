using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energybar : MonoBehaviour
{
    public Slider slider;

    void setEnergybar(Character character)
    {
        slider.maxValue = character.getMaxEnergy();
        slider.value = 0;
    }

    // Update is called once per frame
    void updateEnergybar(int energy)
    {
        slider.value = energy;
    }
}
