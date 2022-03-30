using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public static CharacterInfo instance;
    [Header("Character attribute")]
    public string characterName;
    public int characterHealth;
    public int characterEnergy;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance!=null && instance!=this)
        {
            Destroy(this.gameObject);
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void UpdateHealthInfo(int newHealth)
    {
        characterHealth = newHealth;
    }

    public void UpdateEnergyInfo(int newEnergy)
    {
        characterEnergy = newEnergy;
    }
}
