using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BloodLustPlayer : MonoBehaviourPunCallbacks {

    private int score = 0;
    private Character character;
    private bool isWinner;
    private List<Card> hand;
    [SerializeField] private string playerName;
    [SerializeField] private string characterName;
    [SerializeField] private int playerId;
    private bool isLocal;

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
        set{this.character = value;}

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

    public void GenerateCards()
    {
        hand = new List<Card>();

        while(hand.Count < 8)
        {
            Card card = new Card();
            hand.Add(card);
        }
    }

    public void SetWinner(bool winner)
    {
        this.isWinner = winner;
    }

    public bool IsWinner()
    {
        return this.isWinner;
    }

}


