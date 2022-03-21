using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    public List<GameObject> hand;
    public GameObject baseCard { get; set; }
    public GameObject superCard { get; set; }
    private GameObject baseCardInstance;
    
    /*private void Start()
    {
        generateCards(5);
        Debug.Log(hand);
    }*/

    public void generateCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            baseCardInstance = Instantiate(baseCard, transform);
            hand.Add(baseCardInstance);
            baseCardInstance.GetComponent<Card>().generateCard();
        }
    }

}
