using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class card3Pressed : MonoBehaviour
{
    Card card;
    //function to select a card
    void setCard(string select) { card = Resources.Load<Card>(select); }
    //create string with all cards (the card the player chose is last)
    public string createCardString()
    {
        StringBuilder cardData = new StringBuilder("");
        setCard("card1");
        cardData.Append(card.cardToString());
        setCard("card2");
        cardData.Append("$" + card.cardToString());
        setCard("card4");
        cardData.Append("$" + card.cardToString());
        setCard("card5");
        cardData.Append("$" + card.cardToString());
        setCard("card3");
        cardData.Append("$" + card.cardToString());
        return cardData.ToString();
    }
    //send card data to server (for now it just prints to the console)
    public void sendString() { Debug.Log(createCardString()); }
}
