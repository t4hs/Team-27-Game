using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Player: MonoBehaviourPunCallbacks{
    
    // Damage handler refactoring needed for public Hand hand;
    
    [Header("Set Before Runtime")] 
    public Transform[] characterSpawns;
    private Hand hand;
    
    [Header("Networking Information")]
    [SerializeField] private int playerId;

    [Header("Game Information - Set At Runtime")]
    public Character character;
    [SerializeField] private string playerName;
    [SerializeField] private string characterName;
    private bool hasSelected;
    private Card selectedCard;
    private bool isLocal;
    private GameObject playerPref;

    public void spawnCharacters(int spawnIndex, int startCardAmount)
    {
        GameObject characterPref = ChosenCharacter.CharacterPrefab;
        //sets up Hand
        hand = PlayerInfo.instance.hand;
        hand.baseCard = ChosenCharacter.CardPrefab;
        hand.generateCards(startCardAmount);
        
        //instantiates Characters
        playerPref = PhotonNetwork.Instantiate(characterPref.name,characterSpawns[spawnIndex].position,
                characterSpawns[spawnIndex].rotation,0);
    }
    
    //Assign character to players
    public void AssignCharacters(Character character)
    {
        ChosenCharacter = character;
        characterSpawns = PlayerInfo.instance.characterSpawns;

        Debug.Log(ChosenCharacter.name + " " + ChosenCharacter.cardPrefab.GetComponent<Card>().damage.ToString());
        hand.baseCard = character.cardPrefab;
        hand.show();
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


