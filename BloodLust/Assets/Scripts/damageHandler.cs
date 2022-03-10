using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class damageHandler : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public damageHandler()
    {

    }
    private void dealDamage(Character character, int dmg)
    {
        character.health -= dmg;
    }
    private void heal(Character character, int value)
    {
        character.health += value;
    }
    public void CompareCards(Card card1, Card card2, Player p1, Player p2)
    {
        //net damage after all combos and reductions
        int finalDamage = 0;
        damageHandler dmg = new damageHandler();
        //attack vs attack
        if (card1.type == "attack" && card2.type == "attack")
        {
            if (card1.damage > card2.damage)
            {
                int[] temp = comboDamage(p1.hand, "attack");
                finalDamage = temp[0];
                p1.character.energy += temp[1];
                dealDamage(p2.character, finalDamage);

            }
            else if (card1.damage < card2.damage)
            {
                int[] temp = comboDamage(p2.hand, "attack");
                finalDamage = temp[0];
                p2.character.energy += temp[1];
                dealDamage(p1.character, finalDamage);
            }
            else
            {
                int[] temp = comboDamage(p1.hand, "attack");
                finalDamage = temp[0];
                p1.character.energy += temp[1];
                dealDamage(p2.character, finalDamage);
                finalDamage = 0;
                temp = comboDamage(p2.hand, "attack");
                finalDamage = temp[0];
                p2.character.energy += temp[1];
                dealDamage(p1.character, finalDamage);
            }
            //play animation
        }

        //counter vs counter
        //play animation

        //dodge vs dodge
        //play animation

        //grapple vs grapple
        if (card1.type == "grapple" && card2.type == "grapple")
        {
            if (card1.damage > card2.damage)
            {
                int[] temp = comboDamage(p1.hand, "grapple");
                finalDamage = temp[0];
                p1.character.energy += temp[1];
                dealDamage(p2.character, finalDamage);

            }
            else if (card1.damage < card2.damage)
            {
                int[] temp = comboDamage(p2.hand, "grapple");
                finalDamage = temp[0];
                p2.character.energy += temp[1];
                dealDamage(p1.character, finalDamage);
            }
            else
            {
                int[] temp = comboDamage(p1.hand, "grapple");
                finalDamage = temp[0];
                p1.character.energy += temp[1];
                dealDamage(p2.character, finalDamage);
                finalDamage = 0;
                temp = comboDamage(p2.hand, "grapple");
                finalDamage = temp[0];
                p2.character.energy += temp[1];
                dealDamage(p1.character, finalDamage);
            }
            //play animation
        }
        //heal vs heal
        if(card1.type == "heal" && card2.type == "heal")
        {
            finalDamage = comboDamage(p1.hand, "heal")[0];
            heal(p1.character, finalDamage);
            finalDamage = 0;
            finalDamage = comboDamage(p2.hand, "heal")[0];
            heal(p2.character, finalDamage);
            //play animation
        }

        //attack vs counter
        if(card1.type == "attack" && card2.type == "counter")
        {
            finalDamage = comboDamage(p1.hand, "attack")[0];
            dealDamage(p1.character, finalDamage);
            //play animation
        }

        //attack vs dodge
        if (card1.type == "attack" && card2.type == "dodge")
        {
            int[] temp = comboDamage(p2.hand, "attack");
            finalDamage = temp[0];
            p2.character.energy += temp[1];
            dealDamage(p1.character, finalDamage);
            //play animation
        }
        //attack vs heal
        if (card1.type == "attack" && card2.type == "heal")
        {
            heal(p2.character, card2.damage);
            //play animation
        }

        //attack vs grapple
        if (card1.type == "attack" && card2.type == "grapple")
        {
            int[] temp = comboDamage(p1.hand, "attack");
            finalDamage = temp[0];
            p1.character.energy += temp[1];
            dealDamage(p2.character, finalDamage);
            //play animation
        }
        //counter vs attack
        if (card1.type == "counter" && card2.type == "attack")
        {
            finalDamage = comboDamage(p2.hand, "attack")[0];
            dealDamage(p2.character, finalDamage);
            //play animation
        }
        //counter vs dodge
        if (card1.type == "counter" && card2.type == "dodge")
        {
            int[] temp = comboDamage(p2.hand, "attack");
            finalDamage = temp[0];
            p2.character.energy += temp[1];
            dealDamage(p1.character, finalDamage);
            //play animation
        }
        //counter vs heal
        if (card1.type == "counter" && card2.type == "heal")
        {
            heal(p1.character, card2.damage);
        }
        //counter vs grapple
        if (card1.type == "counter" && card2.type == "grapple")
        {
            finalDamage = comboDamage(p1.hand, "grapple")[0];
            dealDamage(p1.character, finalDamage);
            //play animation
        }
        //dodge vs attack
        if (card1.type == "dodge" && card2.type == "attack")
        {
            int[] temp = comboDamage(p1.hand, "attack");
            finalDamage = temp[0];
            p1.character.energy += temp[1];
            dealDamage(p2.character, finalDamage);
            //play animation
        }
        //dodge vs counter
        if (card1.type == "dodge" && card2.type == "counter")
        {
            int[] temp = comboDamage(p1.hand, "attack");
            finalDamage = temp[0];
            p1.character.energy += temp[1];
            dealDamage(p2.character, finalDamage);
            //play animation
        }
        //dodge vs heal
        if (card1.type == "dodge" && card2.type == "heal")
        {
            heal(p2.character, card2.damage * 2);
        }
        //dodge vs grapple
        if (card1.type == "dodge" && card2.type == "grapple")
        {
            int[] temp = comboDamage(p2.hand, "grapple");
            finalDamage = temp[0];
            p2.character.energy += temp[1];
            dealDamage(p1.character, finalDamage);
            //play animation
        }
        //heal vs attack
        if (card1.type == "heal" && card2.type == "attack")
        {
            heal(p1.character, card1.damage);
            //play animation
        }
        //heal vs counter
        if (card1.type == "heal" && card2.type == "counter")
        {
            heal(p2.character, card1.damage);
        }
        //heal vs dodge
        if (card1.type == "heal" && card2.type == "dodge")
        {
            heal(p1.character, card1.damage*2);
        }
        //heal vs grapple
        if (card1.type == "heal" && card2.type == "grapple")
        {
            int[] temp = comboDamage(p2.hand, "grapple");
            finalDamage = temp[0];
            heal(p1.character, finalDamage);
        }
        //grapple vs attack
        if (card1.type == "grapple" && card2.type == "attack")
        {
            int[] temp = comboDamage(p2.hand, "attack");
            finalDamage = temp[0];
            p2.character.energy += temp[1];
            dealDamage(p1.character, finalDamage);
            //play animation
        }
        //grapple vs counter
        if (card1.type == "grapple" && card2.type == "counter")
        {
            int[] temp = comboDamage(p1.hand, "grapple");
            finalDamage = temp[0];
            p1.character.energy += temp[1];
            dealDamage(p2.character, finalDamage);
            //play animation
        }
        //grapple vs dodge
        if (card1.type == "grapple" && card2.type == "dodge")
        {
            int[] temp = comboDamage(p1.hand, "grapple");
            finalDamage = temp[0];
            p1.character.energy += temp[1];
            dealDamage(p2.character, finalDamage);
            //play animation
        }
        //grapple vs heal
        if (card1.type == "grapple" && card2.type == "heal")
        {
            int[] temp = comboDamage(p1.hand, "grapple");
            finalDamage = temp[0];
            heal(p2.character, finalDamage);
        }
    }
    private int[] comboDamage(List<Card> hand, string type)
    {
        int totalDamage = 0;
        int combos = 0;
        foreach (Card card in hand)
        {
            if (card.type == type)
            {
                totalDamage += card.damage;
                combos += 1;
                hand.Remove(card);
            }
        }
        int[] values= {totalDamage, combos};
        return values;
    }
}
