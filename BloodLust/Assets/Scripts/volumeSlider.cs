using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volumeSlider : MonoBehaviour
{
    [SerializeField] string parameter = "MasterVolume";
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;
    [SerializeField] float multiplier = 30f;


    private void Awake()
    {
        slider.onValueChanged.AddListener(inputValue);
    }

    private void inputValue(float input)
    {
        mixer.SetFloat(parameter, Mathf.Log10(input) * multiplier);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(parameter, slider.value);
    }

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(parameter, slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
