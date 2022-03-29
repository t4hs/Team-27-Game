using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [Header("Set At Runtime")]
    public string type;
    public int damage;
    [SerializeField] private bool isSelected;
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
    int rndType = UnityEngine.Random.Range(0, 5);
    if (rndType == 0) type = "attack";
    else if (rndType == 1) type = "counter";
    else if (rndType == 2) type = "dodge";
    else if (rndType == 3) type = "grapple";
    else if (rndType == 4) type = "heal";

    if(type.Equals("dodge") || type.Equals("heal"))
    {
        damage = 0;
    }
    damageText.text = damage.ToString();
    typeText.text = type;
   }

   public void disableCard()
   {
       //selected.text = "I'm Not Selected";
       Color32 temp = GetComponent<Image>().color;
       temp.a = 150;
       GetComponent<Image>().color = temp;
   }

   public void enableCard()
   {
       //selected.text = "I'm Selected";
       Color32 temp = GetComponent<Image>().color;
       temp.a = 255;
       GetComponent<Image>().color = temp;
   }
   
   public void OnPointerClick(PointerEventData eventData)
   {
       setCardc?.Invoke(this.gameObject);
   }
}
