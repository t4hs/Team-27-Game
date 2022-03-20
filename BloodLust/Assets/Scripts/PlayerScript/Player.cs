using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Player: MonoBehaviourPunCallbacks{

    public Character character;
    // Damage handler refactoring needed for public Hand hand;
    public Hand hand;
    [SerializeField] private string playerName;
    [SerializeField] private string characterName;
    [SerializeField] private int playerId;

    //Assign character to players
    public void AssignCharacters(Character character)
    {
        ChosenCharacter = character;
        hand.baseCard = ChosenCharacter.cardPrefab;
    }

    //Assign players attributes to players
    public  void AssignPlayers(string playerName, int actorNumber, bool isLocal)
    {
        NickName = playerName;
        PlayerId = actorNumber;
        IsLocal = isLocal;
    }
    
    public int PlayerId
    {
        set{this.playerId = value; }
        get{return this.playerId; }
    }

    public string NickName
    {
        set{this.playerName = value; }
        get{ return this.playerName; }
    }

    public bool IsLocal { set; get; }

    public Character ChosenCharacter
    {
        set{
            this.character = value;
            CharacterName = this.character.characterName;
        }

        get{return this.character;}
    }

    public string CharacterName
    {
        set{this.characterName = value;}

        get{return this.characterName; }
    }

   
}


