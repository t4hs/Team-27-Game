using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    //information about current Cards
    public List<GameObject> hand;
    public GameObject selectedCard;
    public bool hasSelected = false;
    
    //card prefabs taken from Character
    public GameObject baseCard { get; set; }
    public GameObject superCard { get; set; }
    private GameObject baseCardInstance;

    [SerializeField] private GameObject testCard;
    private void Start() {
        //generateCardsTest(6);
        //StartCoroutine(test());
    }

    //Call this to add Amount cards the Player's hand
    public void generateCards(int amount) {
        for (int i = 0; i < amount; i++)
        {
            baseCardInstance = Instantiate(baseCard, transform);
            baseCardInstance.GetComponent<Card>().generateCard();
            baseCardInstance.GetComponent<Card>().setCardc += setCard;
            hand.Add(baseCardInstance);
        }
    }
    
    //For Testing
    public void generateCardsTest(int amount) {
        for (int i = 0; i < amount; i++)
        {
            baseCardInstance = Instantiate(testCard, transform);
            baseCardInstance.GetComponent<Card>().generateCard();
            baseCardInstance.GetComponent<Card>().setCardc += setCard;
            hand.Add(baseCardInstance);
        }
    }

    public void Deactivate()
    {
        foreach(GameObject card in hand)
        {
            card.GetComponent<Card>().disableCard();
            card.GetComponent<Card>().setCardc-=setCard;
        }
    }

    public void Activate()
    {
        foreach(GameObject card in hand)
        {
            card.GetComponent<Card>().enableCard();
            card.GetComponent<Card>().setCardc+=setCard;
        }
    }
        
    //Use this to add a card to a Player's hand
    public void addCard() {
        baseCard = Instantiate(baseCard, transform);
        hand.Add(baseCard);
        baseCard.GetComponent<Card>().generateCard();
        baseCard.GetComponent<Card>().setCardc += setCard;
    }
    

    
    //Use this to remove a certain card from the Player's Hand
    public void removeCard(GameObject card) {
        foreach (GameObject c in hand) {
            if (c == card) {
                c.GetComponent<Card>().setCardc -= setCard;
                hand.Remove(c);
                Destroy(c);
            }
        }
    }

    //Event Function That Is Called When a Card is Clicked
    public void setCard(GameObject card) {
        selectedCard = card;
        foreach (GameObject c in hand)
        {
            c.GetComponent<Card>().setCardc-=setCard;
            c.GetComponent<Card>().disableCard();
        }
        selectedCard.GetComponent<Card>().enableCard();
        PlayerManager.instance.SendData(selectedCard);
    }
    
    IEnumerator test() {
        yield return new WaitForSeconds(3f);
        Debug.Log("Fat Idiot 1");
        addCard();
        yield return new WaitForSeconds(3f);
        Debug.Log("Fat Fingered 2");
        removeCard(hand.ElementAt(2));
    }
}
