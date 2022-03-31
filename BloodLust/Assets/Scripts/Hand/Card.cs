using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;
using TMPro;
using UnityEditor.Experimental;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [Header("Set At Runtime")]
    public string type;
    public int damage;
    [SerializeField] private bool isSelected;

    [Header("Text Fields/Set Before Runtime")]
    //[SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI typeText;
    //[SerializeField] private TextMeshProUGUI selected;
    [SerializeField] private Image icon;
    [SerializeField] private List<nameIcons> iconsList; 
    public event Action<GameObject> setCardc;

     
    void Start() {
        iconsList = PlayerInfo.instance.typeIcons;
        generateCard();
    }

    //create a card
   public void generateCard()
   {
    damage = UnityEngine.Random.Range(500, 900);
    int rndType = UnityEngine.Random.Range(0, 4);

    switch (rndType) {
        case 0:
            type = "attack";
            setIcon("attack");
            break;
        case 1:
            type = "counter";
            setIcon("counter");
            break;
        case 2:
            type = "dodge";
            setIcon("dodge");
            break;
        case 3:
            type = "grapple";
            setIcon("grapple");
            break;
        case 4:
            type = "heal";
            setIcon("heal");
            break;
    }
    if(type.Equals("dodge") || type.Equals("heal"))
    {
       damage = 0;
    }
    typeText.text = type;
   }
    //Sets the Card State, So That Its Not Usable
   public void disableCard()
   {
       isSelected = false;
       //selected.text = "I'm Not Selected";
       GetComponent<CanvasGroup>().alpha = 0.4f;
   }
//Allows the Card To be Usable Again
   public void enableCard()
   {
       isSelected = true;
       //selected.text = "I'm Selected";
       GetComponent<CanvasGroup>().alpha = 1f;
   }
   //Used When Drawing a Card
   public void setInvisibleCard() {
       GetComponent<RectTransform>().localScale = Vector3.zero;
       GetComponent<CanvasGroup>().alpha = 0;
   }

   private void setIcon(string name) {
       foreach (nameIcons i in iconsList) {
           if (i.name.Equals(name)) {
               icon.sprite = i.icon;
           }
       }
   }
   
   public void OnPointerClick(PointerEventData eventData)
   {
       setCardc?.Invoke(this.gameObject);
   }
}

[Serializable]
public struct nameIcons
{
    public string name;
    public Sprite icon;
}
