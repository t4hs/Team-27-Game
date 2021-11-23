using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Card", menuName ="Card")]
public class Card : ScriptableObject
{
    public string type;
    public string damageType;
    public int damage;
    public Sprite artwork;

    //convert card data to a string
    public string cardToString()
    {
        return type+"$"+damageType+"$"+damage.ToString();
    }
}