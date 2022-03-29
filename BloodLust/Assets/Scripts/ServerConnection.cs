using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ServerConnection : MonoBehaviourPunCallbacks
{

    public void connectPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.NickName = DBManager.username;
            Debug.Log("connecting to the server");
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master. Joining a lobby");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("LobbyScene");
        Debug.Log("Joined lobby");
    }

}
