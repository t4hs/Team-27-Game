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
    public event Action<Player> PlayerSelectedCard;
    public Transform[] spawnPoints;
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameUIManager gameUIManager;
    PhotonView PV;
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
        PlayerSelectedCard += OnPlayerSelectedCard;
        DontDestroyOnLoad(transform.gameObject);
        PV = GetComponent<PhotonView>();
    }

    void OnDestroy()
    {
        GameStateChange-=OnGameStateChange;
        PlayerSelectedCard -= OnPlayerSelectedCard;
    }

    void Start()
    {
        ChangeState(GameState.GameStart);
    }

    void Update()
    {
        //if(PlayerManager.instance.bothPlayersHaveSelected)
        {
            ChangeState(GameState.Comparison);
        }
    }

    public void GetTargetPlayer(Player targetPlayer)
    {
        PlayerSelectedCard(targetPlayer);
    }
    private void OnPlayerSelectedCard(Player targetPlayer)
    {
        if(PlayerManager.instance.player1!=null)
        {
            if (targetPlayer.Equals(PlayerManager.instance.player1))
            {
                PV.RPC(nameof(RPC_Player2Turn), RpcTarget.Others, true);
            }
        }else if(PlayerManager.instance.player2!=null)
        {
            if(targetPlayer.Equals(PlayerManager.instance.player2))
            {
                PV.RPC(nameof(RPC_TurnDone), RpcTarget.MasterClient, PlayerManager.instance.player2);
            }
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
        PlayerManager.instance.SetPlayerCard(selectedCard);
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

    private void HandlePlayer2Turn()
    {
        Debug.Log("Handling player 2 Turn");
    }

    [PunRPC]
    void RPC_DisableCards()
    {
        TogglePlayerButton(false);
    }

    [PunRPC]

    void RPC_Player2Turn(bool value)
    {
        TogglePlayerButton(value);
    }
    
    [PunRPC]

    void RPC_TurnDone(Player targetPlayer)
    {
        PlayerManager.instance.bothPlayersHaveSelected=PlayerManager.instance.BothHaveCard(PlayerManager.instance.player1, targetPlayer);
    }

    [PunRPC]

    void RPC_ComparisonState()
    {
        ChangeState(GameState.Comparison);
    }

    private void HandleComparison()
    {
        //ToDo handle comparison
    }
    public void ShowWinScreen()
    {
        //ToDo implement this function
    }

    public void ShowLooseScreen()
    {
        //ToDo implement this function
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