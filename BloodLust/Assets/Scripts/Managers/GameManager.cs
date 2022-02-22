using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public GameState state { get; private set; }
    private GameObject playerPrefab;
    public Transform[] spawnPoints;
    private int spawnPicker;
    private ExitGames.Client.Photon.Hashtable roomProps = new ExitGames.Client.Photon.Hashtable();
    PhotonView pV;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(transform.gameObject);
    }

    // Load the player prefab for each client
    public void Start()
    {
        playerPrefab = Resources.Load("PlayerPrefab") as GameObject;
        changeState(GameState.GameSetup);
    }

    public void handleLogic()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            //Todo handle player click

        }
    }



    public void changeState(GameState state)
    {
        this.state = state;
        switch (state)
        {
            case GameState.GameSetup:
                HandleGameSetup();
                break;
            case GameState.WaitingInput:
                break;
            case GameState.Player1Win:
                break;
            case GameState.Player2Win:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }



    public void HandleGameSetup()
    {
        //set up relevant cards and spawn the player characters
        if(PhotonNetwork.IsConnected)
        {
           if(PhotonNetwork.IsMasterClient)
           {
                spawnPicker = 0;
                Fighter fighter = playerPrefab.GetComponent<Fighter>();
                GameObject characterPrefab = fighter.ChosenCharacter.CharacterPrefab;
                GameObject fighterGo = PhotonNetwork.Instantiate(characterPrefab.name, spawnPoints[spawnPicker]
                    .position, spawnPoints[spawnPicker].rotation,0);
           }else
           {
                spawnPicker = 1;
                Fighter otherFighter = playerPrefab.GetComponent<Fighter>();
                GameObject characterPrefab = otherFighter.ChosenCharacter.CharacterPrefab;
                GameObject fighterGo = PhotonNetwork.Instantiate(characterPrefab.name, spawnPoints[spawnPicker]
                    .position, spawnPoints[spawnPicker].rotation,0);
           }
        }
    }
}

public enum GameState
{
    GameSetup,
    WaitingInput,
    Player1Win,
    Player2Win
}