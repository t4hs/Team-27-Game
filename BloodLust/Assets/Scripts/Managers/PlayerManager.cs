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
    public GameObject player2prefab;
    public static PlayerManager instance;
    public Player player1, player2;
    private Photon.Realtime.Player player;
    public bool bothPlayersHaveSelected = false;
    PhotonView PV;
    private ExitGames.Client.Photon.Hashtable customProps = new ExitGames.Client.Photon.Hashtable();
    private Dictionary<int,String> selectedCardTypes;
    private Dictionary<int,int> damages;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                instance = this;
            }
        }
        PV=GetComponent<PhotonView>();
        DontDestroyOnLoad(transform.gameObject);
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

    private void Start()
    {
        selectedCardTypes = new Dictionary<int,String>();
        damages = new Dictionary<int,int>();
    }    
    
    public void SendData(GameObject card)
    {
        Player targetPlayer = PhotonNetwork.IsMasterClient ? player1 : player2;
        String type = card.GetComponent<Card>().type;
        int damage = card.GetComponent<Card>().damage;
        if(!selectedCardTypes.ContainsKey(targetPlayer.PlayerId) && !damages.ContainsKey(targetPlayer.PlayerId))
        {
            selectedCardTypes.Add(targetPlayer.PlayerId,type);
            damages.Add(targetPlayer.PlayerId,damage);
        }
        if(PhotonNetwork.IsMasterClient && PV.IsMine)
        {
            PV.RPC(nameof(RPC_Player2Turn), RpcTarget.Others, selectedCardTypes, damages);
            PV.RPC(nameof(RPC_Turn2State), RpcTarget.All);
        }else
        {
            Debug.Log("RPC turn done attempt to send");
            PV.RPC(nameof(RPC_Turn2Done), RpcTarget.MasterClient, selectedCardTypes, damages);
        }
    }

    public void Player1Turn()
    {
        if(selectedCardTypes.Count == 2 && damages.Count == 2)
        {
            selectedCardTypes.Clear();
            damages.Clear();
        }
        player1.GetHand().Activate();
        if(PV.IsMine && PhotonNetwork.IsMasterClient)
        {
            PV.RPC(nameof(RPC_DisablePlayer2Cards), RpcTarget.Others);
        }
    }

    public void Comparison(DamageHandler damageHandler)
    {
        Debug.Log("function call?");
        Player targetPlayer = PhotonNetwork.IsMasterClient ? player1 : player2;
        int playerId = targetPlayer.PlayerId;
        int damage1 = damages[playerId];
        string type1 = selectedCardTypes[playerId];
        int remotePlayerId = PhotonNetwork.PlayerList[1].ActorNumber;
        int damage2 = damages[remotePlayerId];
        string type2 = selectedCardTypes[remotePlayerId];
        int [] damageArr = damageHandler.Compare(type1, type2, damage1, damage2);
        switch(damageArr[1])
        {
            case 0:
                //Display some text
                Debug.Log("nothing happens");
                if(PhotonNetwork.IsMasterClient && PV.IsMine)
                {
                    PV.RPC(nameof(RPC_RestartTurns), RpcTarget.All);
                }
                break;
            case 1:
                if(damageArr[0] == -1)
                {
                    Debug.Log("player heal");
                    player1.ChosenCharacter.Heal(100);
                }else
                {
                    Debug.Log($"Health before damage taken {player1.ChosenCharacter.health}");
                    bool isDead = player1.ChosenCharacter.TakeDamage(damageArr[0]);
                    Debug.Log($"health after taking damage {player1.ChosenCharacter.health}");
                    if(!isDead)
                    {
                        player1.GetHand().Activate();
                        GameManager.instance.ChangeState(GameState.Player1Turn);
                    }else
                    {
                        GameManager.instance.ChangeState(GameState.Player2Win);
                    }
                }
                break;
            case 2:
                if(damageArr[0] == -1)
                {
                    if(PV.IsMine)
                    {
                        PV.RPC(nameof(RPC_Player2Heal), RpcTarget.Others, 100);
                    }
                }else
                {
                    if(PV.IsMine)
                    {
                        Debug.Log("Player 2 taking damage");
                        PV.RPC(nameof(RPC_Player2TakeDamage), RpcTarget.Others, damageArr[0]);
                    }
                }
                break;
            case 3:
                if(damageArr[0] == -1)
                {
                    Debug.Log("both player heals");
                    player1.ChosenCharacter.Heal(100);
                    if(PV.IsMine && PhotonNetwork.IsMasterClient)
                    {
                        PV.RPC(nameof(RPC_Player2Heal), RpcTarget.Others, 100);
                    }
                }
                break;
        }
    }    

    public void LoseScreenPlayer2()
    {
        if(PhotonNetwork.IsMasterClient && PV.IsMine)
        {
            PV.RPC(nameof(RPC_Player2Lose), RpcTarget.Others);
        }
    }

    public void WinScreenPlayer2()
    {
        if(PhotonNetwork.IsMasterClient && PV.IsMine)
        {
            PV.RPC(nameof(RPC_Player2Win), RpcTarget.Others);
        }
    }

    [PunRPC]

    void RPC_DisablePlayer2Cards()
    {
        player2.GetHand().Deactivate();
    }

    [PunRPC]

    void RPC_Player2Turn(Dictionary<int,String> selectedCardTypes, Dictionary<int,int> damages)
    {
        this.selectedCardTypes = selectedCardTypes;
        this.damages = damages;
        player2.GetHand().Activate();
    }

    [PunRPC]

    void RPC_Turn2State()
    {
        GameManager.instance.ChangeState(GameState.Player2Turn);
    }

    [PunRPC]
    
    void RPC_Turn2Done(Dictionary<int,String> selectedCardTypes, Dictionary<int,int> damages)
    {
        this.selectedCardTypes = selectedCardTypes;
        this.damages = damages;
        GameManager.instance.ChangeState(GameState.Comparison);
    }

    [PunRPC]

    void RPC_Player2TakeDamage(int amount)
    {
       bool isDead = player2.ChosenCharacter.TakeDamage(amount);
       PV.RPC(nameof(RPC_CheckDeath), RpcTarget.MasterClient, isDead);
    }

    [PunRPC]

    void RPC_Player2Heal(int amount)
    {
        player2.ChosenCharacter.Heal(amount);
    }

    [PunRPC]

    void RPC_CheckDeath(bool isDead)
    {
        Debug.Log("checking for death");
        if(!isDead)
        {
            player1.GetHand().Activate();
            PV.RPC(nameof(RPC_RestartTurns), RpcTarget.All);
        }else
        {
            GameManager.instance.ChangeState(GameState.Player1Win);
        }
    }

    [PunRPC]

    void RPC_RestartTurns()
    {
        //Display some text
        Debug.Log("restaring turns");
        GameManager.instance.ChangeState(GameState.Player1Turn);
    }

    [PunRPC]

    void RPC_Player2Win()
    {
        player2.showWinScreen();
    }

    [PunRPC]

    void RPC_Player2Lose()
    {
        player2.showLoseScreen();
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
            player2 = player2prefab.GetComponent<Player>();
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
