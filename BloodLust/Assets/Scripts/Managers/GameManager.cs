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
    public Transform[] spawnPoints;
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameUIManager gameUIManager;
    PhotonView PV;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        GameStateChange+=OnGameStateChange;
        DontDestroyOnLoad(transform.gameObject);
        PV = GetComponent<PhotonView>();
    }

    void OnDestroy()
    {
        GameStateChange-=OnGameStateChange;
    }

    void Start()
    {
        ChangeState(GameState.GameStart);
    }

    void Update()
    {
        if(PlayerManager.instance.bothPlayersHaveSelected)
        {
            //Change to comparison state
        }
    }

    public void TogglePlayerButton(bool value)
    {
        foreach(Button button in buttons)
        {
            gameUIManager.SetInteractableButtons(button, value);
        }
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
        Card selectedCard = buttons[index].GetComponent<cardManager>().card;
        PlayerManager.instance.AddSelectedCard(selectedCard);
        TogglePlayerButton(false);
    }

public void HandleGameStart()
{
    //set up relevant cards and spawn the player characters
        PlayerManager.instance.SpawnPlayers();
        ChangeState(GameState.Player1Turn); 
}

private void HandlePlayer1Turn()
    {
        Debug.Log("Handling player 1 turn");
        if(PhotonNetwork.IsMasterClient && PV.IsMine)
        {
            PV.RPC(nameof(RPC_DisableCards), RpcTarget.Others);
        }
    }

    [PunRPC]

    void RPC_DisableCards()
    {
        TogglePlayerButton(false);
    }
    
    public void ShowWinScreen()
    {
        //ToDo implement this function
    }

    public void ShowLooseScreen()
    {
        //ToDo implement this function
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