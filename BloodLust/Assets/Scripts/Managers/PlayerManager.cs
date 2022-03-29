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
    public Player player1, player2;
    private Photon.Realtime.Player player;
    public bool bothPlayersHaveSelected = false;
    PhotonView PV;
    private ExitGames.Client.Photon.Hashtable customProps = new ExitGames.Client.Photon.Hashtable();
    private Dictionary<int,String> selectedCardTypes;
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
    }    
    
    public void SendData(GameObject card)
    {
        Player targetPlayer = PhotonNetwork.IsMasterClient ? player1 : player2;
        String type = card.GetComponent<Card>().type;
        int damage = card.GetComponent<Card>().damage;
        selectedCardTypes.Add(targetPlayer.PlayerId,type);
        if(PhotonNetwork.IsMasterClient && PV.IsMine)
        {
            PV.RPC(nameof(RPC_Player2Turn), RpcTarget.Others, selectedCardTypes);
            PV.RPC(nameof(RPC_Turn2State), RpcTarget.All);
        }else
        {
            Debug.Log("RPC turn done attempt to send");
            PV.RPC(nameof(RPC_Turn2Done), RpcTarget.MasterClient, selectedCardTypes);
        }
    }

    public void Player1Turn()
    {
        if(PV.IsMine && PhotonNetwork.IsMasterClient)
        {
            PV.RPC(nameof(RPC_DisablePlayer2Cards), RpcTarget.Others);
        }
    }    


    [PunRPC]

    void RPC_DisablePlayer2Cards()
    {
        player2.GetHand().Deactivate();
    }

    [PunRPC]

    void RPC_Player2Turn(Dictionary<int,String> selectedCardTypes)
    {
        this.selectedCardTypes = selectedCardTypes;
        player2.GetHand().Activate();
        Debug.Log("Attemting to activate the cards");
    }

    [PunRPC]

    void RPC_Turn2State()
    {
        GameManager.instance.ChangeState(GameState.Player2Turn);
    }

    [PunRPC]
    
    void RPC_Turn2Done(Dictionary<int,String> selectedCardTypes)
    {
        this.selectedCardTypes = selectedCardTypes;
        Debug.Log($"expected cards count 2 and the cards count is {selectedCardTypes.Count}");
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
