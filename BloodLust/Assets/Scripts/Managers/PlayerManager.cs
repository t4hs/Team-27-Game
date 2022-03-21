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
        Debug.Log("SpawnPlayers function called");
        if(PhotonNetwork.IsMasterClient)
        {
            player1.spawnCharacters(0);
        }else
        {
            player2.spawnCharacters(1);
        }
        GameManager.instance.ChangeState(GameState.Player1Turn);
    }
    
    //Functions Called In Character Selection-------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------

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
