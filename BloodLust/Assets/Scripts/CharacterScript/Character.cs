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
    [SerializeField] private const int MAX_HEALTH = default;
    [SerializeField] private const int MAX_ENERGY = default;
    public string CharacterName => characterName;
    public int health;
    public int energy;
    public GameObject CharacterPrefab => characterPrefab;
    public GameObject CardPrefab => cardPrefab;

    public void Attack()
    {

    }

    public void Grapple()
    {

    }

    public void Heal(int amount)
    {

    }

    public void Dodge()
    {

    }

    public void Counter()
    {

    }

    public bool TakeDamage(int amount)
    {
        return false; // ToDo implement this function after During the Damage Handler refactoring
    }


}
