using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


[CreateAssetMenu(fileName ="Character", menuName ="Character")]
public class Character : ScriptableObject
{
     public string characterName;
     public int health;
     public int energy;
     public GameObject characterPrefab;
     public GameObject cardPrefab;

    public bool TakeDamage(int amount)
    {
        return false; // ToDo implement this function after During the Damage Handler refactoring
    }
}
