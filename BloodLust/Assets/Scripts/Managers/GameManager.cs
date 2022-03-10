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
    [SerializeField] private GameObject playerManagerGo;
    private event Action<GameState> GameStateChange;
    public GameObject[] cards;
    [SerializeField] private Transform[] spawnPoints;

    private ExitGames.Client.Photon.Hashtable roomProps = new ExitGames.Client.Photon.Hashtable();
    PhotonView pV;
    PlayerManager playerManager;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        GameStateChange+=OnGameStateChange;
        DontDestroyOnLoad(transform.gameObject);
    }

    void OnDestroy()
    {
        GameStateChange-=OnGameStateChange;
    }

    // Load the player prefab for each client
    public void Start()
    {
        ChangeState(GameState.GameStart);
    }

    private void OnGameStateChange(GameState state)
    {
     this.state = state;
     switch (state)
     {
        case GameState.GameStart:
            HandleGameStart();
            break;
        case GameState.Player1Turn:
            HandlePlayer1Turn();
            break;
        case GameState.Player2Turn:
            HandlePlayer2Turn();
            break;
        case GameState.Player1Win:
        break;
        case GameState.Player2Win:
        break;
        default:
        throw new ArgumentOutOfRangeException(nameof(state), state, null);
    }
}


public void ChangeState(GameState state)
{
    GameStateChange(state);
}



public void HandleGameStart()
{
    //set up relevant cards and spawn the player characters
    PlayerManager.instance.SpawnPlayers();
}

private void HandlePlayer1Turn()
{
    Debug.Log("Handling player1 turn");
   PlayerManager.instance.Player1Turn();
}

private void HandlePlayer2Turn()
{
    Debug.Log("Handling player 2 Turn");
    PlayerManager.instance.Player2Turn();
}

}

public enum GameState
{
    GameStart,
    Player1Turn,
    Player2Turn,
    Player1Win,
    Player2Win
}