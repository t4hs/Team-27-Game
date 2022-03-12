using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.IO;
public class PlayerManager : MonoBehaviourPunCallbacks
{

    public GameObject playerPrefab;
    public static PlayerManager instance;
    private int spawnIndex;
    Player player1, player2;
    public bool IsPlayer1Turn {private set; get;}
    public bool IsPlayer2Turn {private set; get;}
    private PhotonView PV;
    private GameObject go;
    private Photon.Realtime.Player player;
    private ExitGames.Client.Photon.Hashtable customProps = new ExitGames.Client.Photon.Hashtable();
    public void Awake()
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

    private void Start()
    {
        this.IsPlayer1Turn=true;
        this.IsPlayer2Turn=false;
        PV = GetComponent<PhotonView>();
    }

    // In this function we check whether it's player 1 or player 2 turn each frame
    private void Update()
    {
       
    }


    //Spawn the player avatars accross the screen
    public void SpawnPlayers()
    {
        Debug.Log("SpawnPlayers function called");
        if(PhotonNetwork.IsMasterClient)
        {
            GameObject characterPref = player1.ChosenCharacter.CharacterPrefab;
            spawnIndex = 0;
            go = PhotonNetwork.Instantiate(characterPref.name,GameManager.instance.spawnPoints[spawnIndex].position,
                GameManager.instance.spawnPoints[spawnIndex].rotation,0);
        }else
        {
            GameObject characterPref = player2.ChosenCharacter.CharacterPrefab;
            spawnIndex = 1;
            go = PhotonNetwork.Instantiate(characterPref.name,GameManager.instance.spawnPoints[spawnIndex].position,
                GameManager.instance.spawnPoints[spawnIndex].rotation,0);
        }
        GameManager.instance.ChangeState(GameState.Player1Turn);
    }

    public void ShowWinScreen()
    {
        //ToDo implement this function
    }

    public void ShowLooseScreen()
    {
        //ToDo implement this function
    }

    //For now it requests the player 1 to click
    public void Player1Turn()
    {
       //ToDo implement this function
    }

    //For now it requests the player 2 to click
    public void Player2Turn()
    {
       //ToDo implement this function
    }


    [PunRPC] // Function excuted by all the clients
    void RPC_Player2TurnState()
    {
        GameManager.instance.ChangeState(GameState.Player2Turn);
    }

    [PunRPC] // Function excuted by the remote client
    void RPC_Player2Turn(bool val)
    {
        this.IsPlayer2Turn = val;
    }

    [PunRPC]// Function executed by the Master client
    void RPC_Player1Turn(bool val)
    {
        this.IsPlayer1Turn = val;
        GameManager.instance.ChangeState(GameState.Player1Turn);
    }
}
