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
    public GameObject baseCard;
    public GameObject superCard { get; set; }
    private GameObject baseCardInstance;

    public void show()
    {
        Debug.Log(baseCard.GetComponent<Card>().damage.ToString());
    }
    
    //Call this to add Amount cards the Player's hand
    public void generateCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            baseCardInstance = Instantiate(baseCard, transform);
            baseCardInstance.GetComponent<Card>().generateCard();
            hand.Add(baseCardInstance);
        }
    }

    //Use this to add a card to a Player's hand
    public void addCard()
    {
        baseCard = Instantiate(baseCard, transform);
        baseCard.GetComponent<Card>().generateCard();
        hand.Add(baseCard);
    }

    //Use this to remove a certain card from the Player's Hand
    public void removeCard(GameObject card)
    {
        hand.Remove(card);
    }

}
