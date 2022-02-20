using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField] private Button createRoomButton;
    [SerializeField] private Button joinRoomButton;
    [SerializeField] private InputField roomNameInput;
    [SerializeField] private GameObject lobbyPanel, roomPanel;
    [SerializeField] private Text roomName;
    [SerializeField] private byte maxPlayers = 2;

    void Start()
    {
        createRoomButton.onClick.AddListener(()=>{
            this.CreateRoom(roomNameInput.text);
            });

        joinRoomButton.onClick.AddListener(()=>{
            this.JoinRoom(roomNameInput.text);
            });
    }

    // Fires when a player enters in the room
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if(PhotonNetwork.CurrentRoom.PlayerCount == this.maxPlayers && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("CharacterScene");
        }
    }

    //Join a room after a player clicked to the join room button
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    // Create after a player clicked to the create room button
    public void CreateRoom(string roomName)
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = this.maxPlayers;
        options.BroadcastPropsChangeToAll = true;
        PhotonNetwork.CreateRoom(roomName, options);
    }

    //Fires when a room is joined
    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        this.roomName.text = ": " + PhotonNetwork.CurrentRoom.Name;
        Debug.Log("joined room");
    }

    //Fires when a room is created
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room");
    }

    //Called when failure to join a room(e.g room name not found)
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("failed to join the room {0} error code: {1}", message, returnCode);
    }

    //Failure to create a room
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("unable to create the room {0} error code: {1}", message, returnCode);
    }
}
