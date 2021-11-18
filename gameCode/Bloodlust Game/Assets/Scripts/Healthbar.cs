using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Health health;
    public Image fillImage;
    private Slider slider;
    Color colour;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
        colour = fillImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (fillImage.enabled == false && slider.value >= slider.minValue) { fillImage.enabled = true; }
        if (slider.value <= slider.minValue) { fillImage.enabled = false;}
        slider.value = health.currentHealth / health.maxHealth;
        if (health.currentHealth / health.maxHealth <= slider.maxValue / 3) { fillImage.color = Color.red; }
        if (health.currentHealth / health.maxHealth > slider.maxValue / 3) { fillImage.color = colour; }

    }
}
