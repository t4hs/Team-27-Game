using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public Transform[] characterSpawns;
    public Hand hand;
    public GameObject playerPrefab;
    public static PlayerInfo instance;

    public void Awake()
    {
        if (instance == null)
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
    }
}
