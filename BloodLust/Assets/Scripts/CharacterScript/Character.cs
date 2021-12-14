using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Character", menuName ="Character")]
public class Character : ScriptableObject
{
    [SerializeField] private string characterName = default;
    [SerializeField ] private GameObject characterPrefab = default;

    public string CharacterName => characterName;
    public GameObject CharacterPrefab => characterPrefab;
}
