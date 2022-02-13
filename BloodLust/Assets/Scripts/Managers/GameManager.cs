using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public GameState state { get; private set; }

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }

    public void Start()
    {
        changeState(GameState.CharacterSelection);
    }

    public void changeState(GameState state)
    {
        this.state = state;
        switch (state)
        {
            case GameState.CharacterSelection:
                HandleCharacterSelection();
                break;
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

    public void HandleCharacterSelection()
    {
        //Load character selection scene, you can also have the UI in the same scene as the game
        changeState(GameState.GameSetup);
    }

    public void HandleGameSetup()
    {
        //set up relevant cards
    }
}

public enum GameState
{
    CharacterSelection,
    GameSetup,
    WaitingInput,
    Player1Win,
    Player2Win
}