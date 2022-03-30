using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


[CreateAssetMenu(fileName ="Character", menuName ="Character")]
public class Character : ScriptableObject
{
    [SerializeField] private string characterName = default;
    [SerializeField] private GameObject characterPrefab = default;
    [SerializeField] private GameObject cardPrefab = default;
    [SerializeField] private GameObject characterGridPrefab = default;
    [SerializeField] private Sprite characterSprite = default;
    [SerializeField] private const int MAX_HEALTH = default;
    [SerializeField] private const int MAX_ENERGY = default;
    public string CharacterName => characterName;
    public int health;
    public int energy;
    public GameObject CharacterPrefab => characterPrefab;
    public GameObject CharacterGridPrefab => characterGridPrefab;
    public GameObject CardPrefab => cardPrefab;
    public Sprite CharacterSprite => characterSprite;

    public void Heal(int amount)
    {
        health+=amount;
        CharacterInfo.instance.UpdateHealthInfo(health);
    }

    public bool TakeDamage(int amount)
    {
        health-=amount;
        CharacterInfo.instance.UpdateHealthInfo(health);
        return health <= 0; 
    }

    public int getMaxEnergy()
    {
        return MAX_ENERGY;
    }
    public int getMaxHealth()
    {
        return MAX_HEALTH;
    }
}
