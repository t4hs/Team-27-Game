using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Player: MonoBehaviourPunCallbacks{
    [Header("Set Before Runtime")] 
    public Transform[] characterSpawns;
    //public PlayerBars bars;
    public GameObject player1Hp;
    public GameObject player2Hp;
    
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
        this.characterSpawns = PlayerInfo.instance.characterSpawns;
        GameObject characterPref = ChosenCharacter.CharacterPrefab;
        //sets up Hand
        hand = PlayerInfo.instance.hand;
        hand.baseCard = ChosenCharacter.CardPrefab;
        hand.generateCards(startCardAmount);
        //Initialises Player Bars
        player1Hp = PlayerInfo.instance.healthBar1;
        player2Hp = PlayerInfo.instance.healthBar2;
        //instantiates Characters
        playerPref = PhotonNetwork.Instantiate(characterPref.name,characterSpawns[spawnIndex].position,
                characterSpawns[spawnIndex].rotation,0);
    }

    public void dealDamage(int amount, int player) {
        
    }
    
    //Assign character to players
    public void AssignCharacters(Character character)
    {
        ChosenCharacter = character;
        CharacterInfo.instance.characterName = ChosenCharacter.CharacterName;
        CharacterInfo.instance.characterEnergy = ChosenCharacter.energy;
        CharacterInfo.instance.characterHealth = ChosenCharacter.health;
    }

    //Assign players attributes to players
    public  void AssignPlayers(string playerName, int actorNumber, bool isLocal)
    {
        NickName = playerName;
        PlayerId = actorNumber;
        IsLocal = isLocal;
    }

    public void showWinScreen() {
        PlayerInfo.instance.winScreen.SetActive(true);
    }
    
    public void showLoseScreen() {
        PlayerInfo.instance.LoseScreen.SetActive(true);
    }
    
    
    //----GETTERS AND SETTERS-------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------
    public bool HasSelected
    {
        set { this.hasSelected = value;  }
        get { return this.hasSelected; }
    }

    public Hand GetHand()
    {
        return this.hand;
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


