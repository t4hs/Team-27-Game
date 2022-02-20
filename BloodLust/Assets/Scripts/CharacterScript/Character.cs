using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


[CreateAssetMenu(fileName ="Character", menuName ="Character")]
public class Character : ScriptableObject
{
    [SerializeField] private string characterName = default;
    [SerializeField ] private GameObject characterPrefab = default;
    public string CharacterName => characterName;
    public GameObject CharacterPrefab => characterPrefab;

    public void Attack()
    {

    }

    public void Grapple()
    {

    }

    public void Heal()
    {

    }

    public void Dodge()
    {

    }

    public Health health
    {
        set;
        get;
    }

    public Energy energy
    {
        set;
        get;
    }
}
