using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cardManager : MonoBehaviour
{
    public Card card;
    public Text type;
    public Text damage;
    // Start is called before the first frame update
    void Start()
    {
        card.generateCard();
        display();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void display()
    {
        type.text = card.type;
        damage.text = card.damage.ToString();
    }
}
