using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void CompareCards(Card card1, Card card2, Fighter p1, Fighter p2)
    {
        //combined damage after all combos and reductions
        int totalDamage = 0;
        damageHandler dmg = new damageHandler();
        //attack vs attack
        if (card1.type == "attack" && card2.type == "attack")
        {
            if (card1.damage > card2.damage)
            {
                foreach (Card card in p1.hand)
                {
                    if (card.type == "attack") totalDamage += card.damage;
                }
                dealDamage(p2.character, totalDamage);

            }
            else if (card1.damage < card2.damage)
            {
                foreach (Card card in p2.hand)
                {
                    if (card.type == "attack") totalDamage += card.damage;
                }
                dealDamage(p1.character, totalDamage);
            }
            else
            {
                foreach (Card card in p1.hand)
                {
                    if (card.type == "attack") totalDamage += card.damage;
                }
                dealDamage(p2.character, totalDamage);
                totalDamage = 0;
                foreach (Card card in p2.hand)
                {
                    if (card.type == "attack") totalDamage += card.damage;
                }
                dealDamage(p1.character, totalDamage);
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
                foreach (Card card in p1.hand)
                {
                    if (card.type == "grapple") totalDamage += card.damage;
                }
                dealDamage(p2.character, totalDamage);

            }
            else if (card1.damage < card2.damage)
            {
                foreach (Card card in p2.hand)
                {
                    if (card.type == "grapple") totalDamage += card.damage;
                }
                dealDamage(p1.character, totalDamage);
            }
            else
            {
                foreach (Card card in p1.hand)
                {
                    if (card.type == "grapple") totalDamage += card.damage;
                }
                dealDamage(p2.character, totalDamage);
                totalDamage = 0;
                foreach (Card card in p2.hand)
                {
                    if (card.type == "grapple") totalDamage += card.damage;
                }
                dealDamage(p1.character, totalDamage);
            }
            //play animation
        }
        //heal vs heal
        if(card1.type == "heal" && card2.type == "heal")
        {
            foreach (Card card in p1.hand)
            {
                if (card.type == "heal") totalDamage += card.damage;
            }
            heal(p1.character, totalDamage);
            totalDamage = 0;
            foreach (Card card in p2.hand)
            {
                if (card.type == "heal") totalDamage += card.damage;
            }
            heal(p2.character, totalDamage);
            //play animation
        }

        //attack vs counter
        if(card1.type == "attack" && card2.type == "counter")
        {
            foreach (Card card in p1.hand)
            {
                if (card.type == "attack") totalDamage += card.damage;
            }
            dealDamage(p1.character, totalDamage);
            //play animation
        }

        //attack vs dodge
        if (card1.type == "attack" && card2.type == "dodge")
        {
            foreach (Card card in p2.hand)
            {
                if (card.type == "attack") totalDamage += card.damage;
            }
            dealDamage(p1.character, totalDamage);
            //play animation
        }
        //attack vs heal
        if (card1.type == "attack" && card2.type == "dodge")
        {
            foreach (Card card in p2.hand)
            {
                if (card.type == "attack") totalDamage += card.damage;
            }
            dealDamage(p1.character, totalDamage);
            //play animation
        }

        //attack vs grapple
        if (card1.type == "attack" && card2.type == "grapple")
        {
            foreach (Card card in p1.hand)
            {
                if (card.type == "attack") totalDamage += card.damage;
            }
            dealDamage(p2.character, totalDamage);
            //play animation
        }
        //counter vs attack
        if (card1.type == "counter" && card2.type == "attack")
        {
            foreach (Card card in p2.hand)
            {
                if (card.type == "attack") totalDamage += card.damage;
            }
            dealDamage(p2.character, totalDamage);
            //play animation
        }
        //counter vs dodge
        if (card1.type == "counter" && card2.type == "dodge")
        {
            foreach (Card card in p2.hand)
            {
                if (card.type == "attack") totalDamage += card.damage;
            }
            dealDamage(p1.character, totalDamage);
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
            foreach (Card card in p1.hand)
            {
                if (card.type == "grapple") totalDamage += card.damage;
            }
            dealDamage(p1.character, totalDamage);
            //play animation
        }
        //dodge vs attack
        if (card1.type == "dodge" && card2.type == "attack")
        {
            foreach (Card card in p1.hand)
            {
                if (card.type == "attack") totalDamage += card.damage;
            }
            dealDamage(p2.character, totalDamage);
            //play animation
        }
        //dodge vs counter
        if (card1.type == "dodge" && card2.type == "counter")
        {
            foreach (Card card in p1.hand)
            {
                if (card.type == "attack") totalDamage += card.damage;
            }
            dealDamage(p2.character, totalDamage);
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
            foreach (Card card in p2.hand)
            {
                if (card.type == "grapple") totalDamage += card.damage;
            }
            dealDamage(p1.character, totalDamage);
            //play animation
        }
        //heal vs attack
        if (card1.type == "heal" && card2.type == "attack")
        {
            foreach (Card card in p2.hand)
            {
                if (card.type == "attack") totalDamage += card.damage;
            }
            dealDamage(p1.character, totalDamage);
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
            heal(p1.character, card2.damage);
        }
        //grapple vs attack
        if (card1.type == "grapple" && card2.type == "attack")
        {
            foreach (Card card in p2.hand)
            {
                if (card.type == "attack") totalDamage += card.damage;
            }
            dealDamage(p1.character, totalDamage);
            //play animation
        }
        //grapple vs counter
        if (card1.type == "grapple" && card2.type == "counter")
        {
            foreach (Card card in p2.hand)
            {
                if (card.type == "grapple") totalDamage += card.damage;
            }
            dealDamage(p2.character, totalDamage);
            //play animation
        }
        //grapple vs dodge
        if (card1.type == "grapple" && card2.type == "dodge")
        {
            foreach (Card card in p1.hand)
            {
                if (card.type == "grapple") totalDamage += card.damage;
            }
            dealDamage(p2.character, totalDamage);
            //play animation
        }
        //grapple vs heal
        if (card1.type == "grapple" && card2.type == "heal")
        {
            heal(p2.character, card1.damage);
        }
    }
}
