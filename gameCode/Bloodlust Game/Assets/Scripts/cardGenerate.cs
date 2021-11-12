using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardGenerate : MonoBehaviour
{
    public Card card;
    public Text damageText;
    public Text damageTypeText;
    public Text typeText;
    public Image artworkImage;

    // Start is called before the first frame update
    void Start()
    {
        createCardStats();
        //get cards onto the UI
        damageText.text = card.damage.ToString();
        damageTypeText.text = card.damageType;
        typeText.text = card.type;
        artworkImage.sprite = card.artwork;
    }

    //generate a completely random card
    void createCardStats()
    {
        int rndDamage = Random.Range(500, 900);
        int rndType = Random.Range(0, 2);
        int rndDamgeType = Random.Range(0, 3);
        card.damage = rndDamage;
        if (rndType == 0) card.type = "attack"; else card.type = "counter";
        if (rndDamgeType == 0) card.damageType = "fire";
        if (rndDamgeType == 0) card.artwork= Resources.Load<Sprite>("fire");
        if (rndDamgeType == 1) card.damageType = "water";
        if (rndDamgeType == 1) card.artwork = Resources.Load<Sprite>("water");
        if (rndDamgeType == 2) card.damageType = "ice";
        if (rndDamgeType == 2) card.artwork = Resources.Load<Sprite>("ice");
    }
}
