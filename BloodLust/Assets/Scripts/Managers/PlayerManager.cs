using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.IO;
using System;
public class PlayerManager : MonoBehaviourPunCallbacks
{

    public GameObject playerPrefab;
    public static PlayerManager instance;
    private int spawnIndex;
    Player player1, player2;
    public bool IsPlayer1Turn {private set; get;}
    public bool IsPlayer2Turn {private set; get;}
    private GameObject playerPref;
    public Dictionary<Player, Card> selectedCards;
    private Photon.Realtime.Player player;
    private event Action<Dictionary<Player, Card>> UpdateSelectedCards;
    public bool bothPlayersHaveSelected = false;
    private ExitGames.Client.Photon.Hashtable customProps = new ExitGames.Client.Photon.Hashtable();
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            if (instance != null && instance != this)
            {
                instance = this;
            }
        }
        UpdateSelectedCards += OnUpdateSelectedCards;
        DontDestroyOnLoad(transform.gameObject);
    }

    void OnDestroy()
    {
        UpdateSelectedCards -= OnUpdateSelectedCards;
    }
    
    // function called in the CharacterSelection script
    public void InitPlayers()
    {
        player = PhotonNetwork.LocalPlayer;
        if(PhotonNetwork.IsMasterClient)
        {
            player1 = playerPrefab.GetComponent<Player>();
            player1.AssignPlayers(player.NickName, player.ActorNumber, player.IsLocal);
            playerPrefab.GetComponent<Text>().text = player1.NickName;
        }else
        {
            player2 = playerPrefab.GetComponent<Player>();
            player2.AssignPlayers(player.NickName, player.ActorNumber, player.IsLocal);
            playerPrefab.GetComponent<Text>().text = player2.NickName;
        }
    }




    //This event events when a player has selected a card
    private void OnUpdateSelectedCards(Dictionary<Player,Card> selectedCards)
    {

    }
           
            
    // Initialize the character an assign to whatever player chosen it
    public void InitCharacters(Character chosenCharacter, int currentCharacter)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            player1.AssignCharacters(chosenCharacter);
            customProps["chosenCharacter"] = currentCharacter;
            player.SetCustomProperties(customProps);
        }
        else
        {
            player2.AssignCharacters(chosenCharacter);
            customProps["chosenCharacter"] = currentCharacter;
            player.SetCustomProperties(customProps);
        }
    }


    //Check if both players have selected a character
    public bool PlayersReady()
    {
        bool readyToFight = true;

        foreach(Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if (!player.CustomProperties.ContainsKey("chosenCharacter"))
                readyToFight = false;
        }

        return readyToFight;
    }

    //Add the selected card and the playerWho selected to the Key value pair dictionary
    public void AddSelectedCard(Card selectedCard)
    {
        Player playerKey = PhotonNetwork.IsMasterClient ? player1 : player2;
        selectedCards.Add(playerKey, selectedCard);
        UpdateSelectedCards(selectedCards);
    }

    private void Start()
    {
        this.IsPlayer1Turn=true;
        this.IsPlayer2Turn=false;
        selectedCards = new Dictionary<Player, Card>();
    }

    //Spawn the player avatars accross the screen
    public void SpawnPlayers()
    {
        Debug.Log("SpawnPlayers function called");
        if(PhotonNetwork.IsMasterClient)
        {
            GameObject characterPref = player1.ChosenCharacter.CharacterPrefab;
            spawnIndex = 0;
            playerPref = PhotonNetwork.Instantiate(characterPref.name,GameManager.instance.spawnPoints[spawnIndex].position,
                GameManager.instance.spawnPoints[spawnIndex].rotation,0);
        }else
        {
            GameObject characterPref = player2.ChosenCharacter.CharacterPrefab;
            spawnIndex = 1;
            playerPref = PhotonNetwork.Instantiate(characterPref.name,GameManager.instance.spawnPoints[spawnIndex].position,
                GameManager.instance.spawnPoints[spawnIndex].rotation,0);
        }
        GameManager.instance.ChangeState(GameState.Player1Turn);
    }   
}
