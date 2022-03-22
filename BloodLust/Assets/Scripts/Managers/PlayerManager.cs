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
    public Player player1, player2;
    private Photon.Realtime.Player player;
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
        DontDestroyOnLoad(transform.gameObject);
    }

    public bool BothHaveCard(Player player1, Player player2)
    {
        return player1.HasSelected && player2.HasSelected;
    }

    //Display the player cards in the game scene
    public void DisplayPlayerCards(Hand hand)
    {
        Player targetPlayer = PhotonNetwork.IsMasterClient ? player1 : player2;
        targetPlayer.PlayerHand = hand;
        targetPlayer.PlayerHand.baseCard = targetPlayer.ChosenCharacter.CardPrefab;
        targetPlayer.PlayerHand.generateCards(5);
    }

    //Set the selected card to the target player
    public void SetPlayerCard(Card selectedCard)
    {
        Player targetPlayer = PhotonNetwork.IsMasterClient ? player1 : player2;
        targetPlayer.SelectedCard = selectedCard;
        targetPlayer.HasSelected = true;
        GameManager.instance.GetTargetPlayer(targetPlayer);
    }

    //Spawn the player avatars accross the screen
    public void SpawnPlayers()
    {
        Debug.Log("Spawing Characters");
        if(PhotonNetwork.IsMasterClient)
        {
            player1.spawnCharacters(0, 5);
        }else
        {
            player2.spawnCharacters(1, 5);
        }
        GameManager.instance.ChangeState(GameState.Player1Turn);
    }

    //This event events when a player has selected a card
    private void OnUpdateSelectedCards(Dictionary<Player,Card> selectedCards)
    {

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
            GameObject characterPref = player1.ChosenCharacter.characterPrefab;
            spawnIndex = 0;
            playerPref = PhotonNetwork.Instantiate(characterPref.name,GameManager.instance.spawnPoints[spawnIndex].position,
                GameManager.instance.spawnPoints[spawnIndex].rotation,0);
        }else
        {
            GameObject characterPref = player2.ChosenCharacter.characterPrefab;
            spawnIndex = 1;
            playerPref = PhotonNetwork.Instantiate(characterPref.name,GameManager.instance.spawnPoints[spawnIndex].position,
                GameManager.instance.spawnPoints[spawnIndex].rotation,0);
        }
        GameManager.instance.ChangeState(GameState.Player1Turn);
    }   
    
    
    //--------------FUNCTIONS CALLED IN CHARACTER SELECTION ---------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------------------
    

    // function called in the CharacterSelection script
    public void InitPlayers()
    {
        player = PhotonNetwork.LocalPlayer;
        if (PhotonNetwork.IsMasterClient)
        {
            player1 = playerPrefab.GetComponent<Player>();
            player1.AssignPlayers(player.NickName, player.ActorNumber, player.IsLocal);
            playerPrefab.GetComponent<Text>().text = player1.NickName;
        }
        else
        {
            player2 = playerPrefab.GetComponent<Player>();
            player2.AssignPlayers(player.NickName, player.ActorNumber, player.IsLocal);
            playerPrefab.GetComponent<Text>().text = player2.NickName;
        }
    }
<<<<<<< HEAD
    
=======
>>>>>>> 547d36e030283c35694c2faaaacab73fcc4896f1
    // Initialize the character an assign to whatever player chosen it
    public void InitCharacters(Character chosenCharacter, int currentCharacter)
    {
        if (PhotonNetwork.IsMasterClient)
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

        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if (!player.CustomProperties.ContainsKey("chosenCharacter"))
                readyToFight = false;
        }
        return readyToFight;
    }
}
