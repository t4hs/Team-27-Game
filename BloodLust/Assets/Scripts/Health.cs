using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100000;
        currentHealth = maxHealth;
    }
    //decrease health
    void reduceHealth(int damage)
    {
        currentHealth -= damage;
    }
}
