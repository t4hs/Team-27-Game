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
    private Button connectButton;
    [SerializeField]


    // Start is called before the first frame update
    void Start()
    {
        connectButton.onClick.AddListener(()=>{
            if(userNameField.text.Length >= 1)
            {
                PhotonNetwork.NickName = userNameField.text;
                Debug.Log("connecting to the server");
                PhotonNetwork.ConnectUsingSettings();
            }
        });
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master. Joining a lobby");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("LobbyScene", LoadSceneMode.Single);
        Debug.Log("Joined lobby");
    }

}
