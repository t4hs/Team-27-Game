using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Energy energy;
    public Image fillImage;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fillImage.enabled == false && slider.value >= slider.minValue) { fillImage.enabled = true; }
        if (slider.value <= slider.minValue) { fillImage.enabled = false; }
        slider.value = energy.currentEnergy / energy.maxEnergy;
    }
}
