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
    [SerializeField] private GameObject lobbyPanel, roomPanel;
    [SerializeField] private Text roomName;
    [SerializeField] private byte maxPlayers = 2;
    [SerializeField] private Text roomMessage;
    [SerializeField] private GameObject roomItem;
    private Button roomButton;
    [SerializeField] private Transform content;
    private List<RoomItem> roomItems;
    private RoomItem room;
    public static LobbyManager instance;

    void Awake()
    {
        if(LobbyManager.instance == null)
        {
            LobbyManager.instance = this;
        }else
        {
            if(LobbyManager.instance !=this)
            {
                Destroy(LobbyManager.instance.gameObject);
                LobbyManager.instance = this;
            }
        }
    }

    void Start()
    {
        createRoomButton.onClick.AddListener(()=>{
            string roomName = PhotonNetwork.NickName + "'s room";
            this.CreateRoom(roomName);
        });

        this.roomMessage.text = "No room to display";
        roomItems = new List<RoomItem>();
    }

    // Fires when a player enters in the room
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if(PhotonNetwork.CurrentRoom.PlayerCount == this.maxPlayers && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("CharacterScene");
        }
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
        roomMessage.text = "Join one of the rooms down below!!!";
    }

    public void UpdateRoomList(List<RoomInfo> roomList)
    {
        Debug.Log("room update run");
        foreach(RoomItem item in roomItems)
        {
            Destroy(item.gameObject);
        }
        roomItems.Clear();

        foreach(RoomInfo info in roomList)
        {
            GameObject roomGo = Instantiate(roomItem, content);
            RoomItem item = roomGo.GetComponent<RoomItem>();
            item.SetRoomName(info.Name);
            roomItems.Add(item);
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
        options.IsVisible = true;
        PhotonNetwork.CreateRoom(roomName, options);
    }

    //Fires when a room is joined
    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        this.roomName.text= string.Format("Room name: {0}", PhotonNetwork.CurrentRoom.Name);
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
