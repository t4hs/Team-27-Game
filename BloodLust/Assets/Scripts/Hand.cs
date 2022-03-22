using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    //information about current Cards
    public List<GameObject> hand;
    public GameObject selectedCard;
    
    //card prefabs taken from Character
    public GameObject baseCard { get; set; }
    public GameObject superCard { get; set; }
    private GameObject baseCardInstance;
    
    [SerializeField] private GameObject testCard;
    private void Start()
    {
        generateCardsTest(5);
        Debug.Log(hand);
    }

    public void generateCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            baseCardInstance = Instantiate(baseCard, transform);
            hand.Add(baseCardInstance);
            baseCardInstance.GetComponent<Card>().generateCard();
            baseCardInstance.GetComponent<Card>().setCardc += setCard;
        }
    }

    public void setCard(GameObject card)
    {
        selectedCard = card;
        foreach (GameObject c in hand)
        {
            c.GetComponent<Card>().disableCard();
        }
        card.GetComponent<Card>().enableCard();
        Debug.Log(card.GetComponent<Card>().damage);
    }
    
    //just comment this
    public void generateCardsTest(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            baseCardInstance = Instantiate(testCard, transform);
            hand.Add(baseCardInstance);
            baseCardInstance.GetComponent<Card>().generateCard();
            baseCardInstance.GetComponent<Card>().setCardc += setCard;
        }
    }

}
