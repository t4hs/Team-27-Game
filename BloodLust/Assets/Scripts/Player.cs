using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Card> hand;
    public int health;
    public int energy;

    public Player()
    {
        health = 10000;
        energy = 0;
        while (hand.Count < 8)
        {
            Card card = new Card();
            hand.Add(card);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
