using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor.IMGUI.Controls;
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

    [Header("Used For Testing")]
    [SerializeField] private GameObject testCard;

    public event Action<List<GameObject>> updateCardPositions;
    private void Start() {
        //generateCardsTest(7);
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
        updateCardPositions?.Invoke(hand);
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
        baseCard.GetComponent<Card>().setInvisibleCard();
        baseCard.GetComponent<cardPositioner>().setSpawn();
        baseCard.GetComponent<Card>().generateCard();
        baseCard.GetComponent<Card>().setCardc += setCard;
        
        updateCardPositions?.Invoke(hand);
    }
    

    
    //Use this to remove a certain card from the Player's Hand
    public void removeCard(GameObject card) {
        GameObject ca;
        hand.RemoveAll(c => c == card);//fix positioning after removing a Card
        foreach (var c in hand.Where(c => c == card)) {
            c.GetComponent<Card>().setCardc -= setCard;
            DOTween.Kill(c.GetComponent<cardPositioner>());
            Destroy(c);
        }
        //hand.RemoveAll(c => c == card);
        updateCardPositions?.Invoke(hand);
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

    //For Testing
    public void generateCardsTest(int amount) {
        for (int i = 0; i < amount; i++)
        {
            baseCardInstance = Instantiate(testCard, transform);
            hand.Add(baseCardInstance);
            baseCardInstance.GetComponent<Card>().generateCard();
            baseCardInstance.GetComponent<Card>().setCardc += setCard;
        }
        updateCardPositions?.Invoke(hand);
    }
    
    IEnumerator test() {
        yield return new WaitForSeconds(3f);
        Debug.Log("Fat Idiot 1");
        removeCard(hand.ElementAt(1));
        //addCard();
        yield return new WaitForSeconds(3f);
        removeCard(hand.ElementAt(2));
        yield return new WaitForSeconds(2f);
        removeCard(hand.ElementAt(2));
    }
}
