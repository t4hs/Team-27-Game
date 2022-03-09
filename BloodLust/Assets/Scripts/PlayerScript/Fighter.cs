using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Fighter : MonoBehaviourPunCallbacks {

    private int score = 0;
    public Character character;
    private bool isWinner;
    public List<Card> hand;
    [SerializeField] private string playerName;
    [SerializeField] private string characterName;
    [SerializeField] private int playerId;
    private bool isLocal;

    //Assign character to players
    public void AssignCharacters(Character character)
    {
        ChosenCharacter = character;
    }

    //Assign players attributes to players
    public void AssignPlayers(string playerName, int actorNumber, bool isLocal)
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

    public bool IsLocal
    {
        set{ this.isLocal = value; }
        get{ return this.isLocal; }
    }

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

    public void UpdateScore(int point)
    {
        score+= point;
    }

    public int GetScore()
    {
        return this.score;
    }

    //Set the winner when someone has 0 Hp
    public void SetWinner(bool winner)
    {
        this.isWinner = winner;
    }

    //Check from the winner
    public bool IsWinner()
    {
        return this.isWinner;
    }

}


