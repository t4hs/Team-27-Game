using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField]
    private Text message;

    void Start()
    {
        message.text = "Waiting for other players to Join...";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
