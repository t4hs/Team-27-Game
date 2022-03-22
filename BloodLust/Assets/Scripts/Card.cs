using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class Card : MonoBehaviour, IPointerClickHandler
{
    public string type;
    public int damage;

    public event Action<GameObject> setCardc;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI selected;
     
    void Start()
    {
        generateCard();
    }

    //create a card
   public void generateCard()
   {
    damage = UnityEngine.Random.Range(500, 900);
    int rndType = UnityEngine.Random.Range(0, 4);
    if (rndType == 0) type = "attack";
    else if (rndType == 1) type = "counter";
    else if (rndType == 2) type = "dodge";
    else if (rndType == 3) type = "grapple";
    else if (rndType == 4) type = "heal";

    damageText.text = damage.ToString();
    typeText.text = type;
   }

   public void disableCard()
   {
       selected.text = "I'm Not Selected";
   }

   public void enableCard()
   {
       selected.text = "I'm Selected";
   }
   
   public void OnPointerClick(PointerEventData eventData)
   {
        setCardc?.Invoke(this.gameObject);
        //Debug.Log("You clicked me");
   }
}
