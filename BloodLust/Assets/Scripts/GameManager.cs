using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private InputField userNameField;
    [SerializeField]
    private Button joinRoomButton;
    [SerializeField]
    private Button createRoomButton;
    [SerializeField]
    private InputField roomNameField;
    [SerializeField]
    private byte MaxPlayers = 2;


    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        createRoomButton.onClick.AddListener(()=>{
            this.CreateRoom(roomNameField.text, userNameField.text);
        });
        joinRoomButton.onClick.AddListener(()=>{
            this.joinRoom(roomNameField.text);
        });
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
    }

    public void CreateRoom(string roomName, string playerName)
    {
        RoomOptions options = new RoomOptions();
        options.IsVisible  = false;
        options.MaxPlayers = this.MaxPlayers;
        PhotonNetwork.NickName = playerName;
        PhotonNetwork.CreateRoom(roomName, options, null);
    }

    public void joinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("failed to join the room. Error dode: {0}, error mesage: {1} ", returnCode, message);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("created room");
        SceneManager.LoadScene("RoomScene", LoadSceneMode.Single);
    }

    /**
     * When a room has been joined
     */
    public override void OnJoinedRoom()
    {
        Debug.Log("joined room");
        PhotonNetwork.LoadLevel("GameScene");
    }

}
