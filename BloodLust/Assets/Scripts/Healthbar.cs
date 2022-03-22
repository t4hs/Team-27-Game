using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public  Slider slider;

    void setHealthbar(Character character)
    {
        slider.maxValue = character.getMaxHealth();
        slider.value = character.health;
    }
    void updateHealthbar(int hp)
    {
        slider.value = hp;
    }
}
