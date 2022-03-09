using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

[CreateAssetMenu(fileName = "new card", menuName = "card")]
public class Card : ScriptableObject
{
    public string type;
    public int damage;
    int rndType;

    // Start is called before the first frame update
   void Start()
   {
   }

    // Update is called once per frame
   void Update()
   {

   }

    //create a card
   public void generateCard()
   {
    damage = UnityEngine.Random.Range(500, 900);
    rndType = UnityEngine.Random.Range(0, 4);
    if (rndType == 0) type = "attack";
    else if (rndType == 1) type = "counter";
    else if (rndType == 2) type = "dodge";
    else if (rndType == 3) type = "grapple";
    else if (rndType == 4) type = "heal";
    }

}
