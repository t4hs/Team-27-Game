using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Player: MonoBehaviourPunCallbacks{

    public Character character;
    // Damage handler refactoring needed for public Hand hand;
    private Hand hand;
    [SerializeField] private string playerName;
    [SerializeField] private string characterName;
    [SerializeField] private int playerId;
    private bool hasSelected;
    private Card selectedCard;
    private bool isLocal;


    
    //Assign character to players
    public void AssignCharacters(Character character)
    {
        ChosenCharacter = character;
    }

    //Assign players attributes to players
    public  void AssignPlayers(string playerName, int actorNumber, bool isLocal)
    {
        NickName = playerName;
        PlayerId = actorNumber;
        IsLocal = isLocal;
    }

    public Hand PlayerHand
    {
        set { this.hand = value; }
        get { return this.hand; }
    }
    public bool HasSelected
    {
        set { this.hasSelected = value;  }
        get { return this.hasSelected; }
    }

    public Card SelectedCard
    {
        set { this.selectedCard = value; }
        get { return this.selectedCard; }
    }

    public bool Equals(Player otherPlayer)
    {
        return this.PlayerId == otherPlayer.PlayerId;
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
            CharacterName = this.character.CharacterName;
        }

        get{return this.character;}
    }

    public string CharacterName
    {
        set{this.characterName = value;}

        get{return this.characterName; }
    }

   
}


