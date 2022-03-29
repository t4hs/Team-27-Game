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
    private event Action<GameState> GameStateChange;
    [SerializeField] private GameUIManager gameUIManager;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            if(instance != null && instance!=this)
            {
                Destroy(this.gameObject);
                instance = this;
            }
        }
        GameStateChange+=OnGameStateChange;
        DontDestroyOnLoad(transform.gameObject);
    }

    void OnDestroy()
    {
        GameStateChange-=OnGameStateChange;
    }

    void Start()
    {
        ChangeState(GameState.GameStart);
    }

    
    public void TogglePlayerCards(bool value)
    {
        
    }

private void OnGameStateChange(GameState state)
    {
        switch(state)
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
            case GameState.Comparison:
                break;
            case GameState.GameEnded:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }


public void ChangeState(GameState state)
{
    GameStateChange(state);
}

public void OnSelectedCard(int index)
    {
        TogglePlayerCards(false);
    }

public void HandleGameStart()
{
    //set up relevant cards and spawn the player characters
    PlayerManager.instance.SpawnPlayers();
    ChangeState(GameState.Player1Turn); 
}

    private void HandlePlayer1Turn()
    {
        PlayerManager.instance.Player1Turn();
    }

    private void HandlePlayer2Turn()
    {
        Debug.Log("Handling player 2 Turn");
    }
    
}

public enum GameState
{
    GameStart,
    Player1Turn,
    Player2Turn,
    Comparison,
    GameEnded
}