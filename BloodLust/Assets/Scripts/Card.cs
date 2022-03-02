using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
<<<<<<< HEAD
public class Card : MonoBehaviour,IComparable<Card>, IPointerClickHandler
=======
public class Card : ScriptableObject,IComparable<Card>, IPointerClickHandler
>>>>>>> dorianBranch
{
    public string type;
    public int damage;
    int rndType;

    public Card()
    {
        generateCard();
    }

    public void Awake()
    {
       damage = UnityEngine.Random.Range(500, 900);
       rndType = UnityEngine.Random.Range(0, 4);
   }


   public void OnPointerClick(PointerEventData data)
   {
        Debug.Log(data);
   }

    // Start is called before the first frame update
   void Start()
   {

   }

    // Update is called once per frame
   void Update()
   {

   }
    //create a card
   void generateCard()
   {

    if (rndType == 0) type = "attack";
    else if (rndType == 1) type = "counter";
    else if (rndType == 2) type = "dodge";
    else if (rndType == 3) type = "grapple";
    else if (rndType == 4) type = "heal";
}

    public int CompareTo(Card otherCard)
    {
        //ToDo implement card comparison
        return 0;
    }



}
