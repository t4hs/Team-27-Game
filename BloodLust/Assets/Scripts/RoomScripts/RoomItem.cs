using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button roomButton;
    [SerializeField] private Text btnText;

    void Start()
    {
        roomButton.onClick.AddListener(()=>{
            LobbyManager.instance.JoinRoom(btnText.text);
        });
    }


    public void SetRoomName(string roomName)
    {
        btnText.text = roomName;
    }



}
