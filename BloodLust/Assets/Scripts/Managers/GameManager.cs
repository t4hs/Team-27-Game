using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine.UI;
public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public GameState state { get; private set; }
    private GameObject playerPrefab;
    private event Action<GameState> GameStateChange;
    public Transform[] spawnPoints;
    [SerializeField] private Button[] buttons;
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

    public void Start()
    {
        ChangeState(GameState.GameStart);
        HandleClickButtons();
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

public void HandleClickButtons()
    {
        foreach(Button button in buttons)
        {
            button.onClick.AddListener(() =>
            {
                Debug.Log("clicked");
                Debug.Log(button.gameObject.GetComponent<cardManager>().card.type);
            });
        }
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