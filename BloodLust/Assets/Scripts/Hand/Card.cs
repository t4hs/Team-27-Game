using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;
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
    damage = UnityEngine.Random.Range(50, 150);
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
       //isSelected = false;
       //selected.text = "I'm Not Selected";
       GetComponent<CanvasGroup>().alpha = 0.4f;
   }

   public void enableCard()
   {
       //isSelected = true;
       //selected.text = "I'm Selected";
       GetComponent<CanvasGroup>().alpha = 1f;
   }

   public void setInvisibleCard() {
       GetComponent<RectTransform>().localScale = Vector3.zero;
       GetComponent<CanvasGroup>().alpha = 0;
   }
   
   public void OnPointerClick(PointerEventData eventData)
   {
       setCardc?.Invoke(this.gameObject);
   }
}
