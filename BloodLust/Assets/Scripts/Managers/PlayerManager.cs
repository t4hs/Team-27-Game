using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;
    private int spawnPicker;
    Fighter fighterPlayer;
    [SerializeField] private Button p1Button, p2Button;
    public bool IsPlayer1Turn {private set; get;}
    public bool IsPlayer2Turn {private set; get;}
    private PhotonView PV;
    private GameObject go;
    public void Init(GameObject playerPref)
    {
        this.playerPrefab = playerPref;
        Debug.Log("Player prefab init");
        fighterPlayer = playerPrefab.GetComponent<Fighter>();
    }

    private void Start()
    {
        this.IsPlayer1Turn=true;
        this.IsPlayer2Turn=false;
        p2Button.gameObject.SetActive(false);
        p1Button.gameObject.SetActive(false);
        if(PhotonNetwork.IsMasterClient)
        {
            p1Button.gameObject.SetActive(true);
        }

        PV = GetComponent<PhotonView>();
    }

    // In this function we check whether it's player 1 or player 2 turn each frame
    private void Update()
    {
        if(IsPlayer2Turn && !PhotonNetwork.IsMasterClient)
        {
            p2Button.gameObject.SetActive(true);
        }
        else if(IsPlayer1Turn && PhotonNetwork.IsMasterClient)
        {
            p1Button.gameObject.SetActive(true);
        }
    }


    //Spawn the player avatars accross the screen
    public void SpawnPlayers()
    {
        GameObject characterPref = fighterPlayer.ChosenCharacter.CharacterPrefab;
        Debug.Log("SpawnPlayers function called");
        if(PhotonNetwork.IsMasterClient)
        {
            spawnPicker = 0;
            go = PhotonNetwork.Instantiate(characterPref.name,spawnPoints[spawnPicker].position,
                spawnPoints[spawnPicker].rotation,0);
        }else
        {
            spawnPicker = 1;
            go = PhotonNetwork.Instantiate(characterPref.name,spawnPoints[spawnPicker].position,
                spawnPoints[spawnPicker].rotation,0);
        }
        GameManager.instance.ChangeState(GameState.Player1Turn);
    }


    //For now it requests the player 1 to click
    public void Player1Turn()
    {
        p1Button.onClick.AddListener(()=>{
            if(PV.IsMine)
            {
                IsPlayer1Turn = false;
                p1Button.gameObject.SetActive(false);
                PV.RPC("RPC_Player2Turn", RpcTarget.Others,true);
                PV.RPC("RPC_Player2TurnState", RpcTarget.All);
            }
        });
    }

    //For now it requests the player 2 to click
    public void Player2Turn()
    {
        p2Button.onClick.AddListener(()=>{
            Debug.Log("clicked");
            if(!PV.IsMine)
            {
                IsPlayer2Turn = false;
                p2Button.gameObject.SetActive(false);
                PV.RPC("RPC_Player1Turn", RpcTarget.MasterClient, true);
            }
        });
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
