using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Hand : MonoBehaviour
{

    [SerializeField] private string defaultName;

    public void Start()
    {
        SetDefaultName();
    }


    public void SetDefaultName()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            this.defaultName = "dorian";
        }else
        {
            this.defaultName = "hamman";
        }
        Debug.LogFormat("your name is {0}", this.defaultName);
    }
}
