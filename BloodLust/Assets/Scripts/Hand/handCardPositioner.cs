using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class handCardPositioner : MonoBehaviour
{
    [Header("Variables For Adjusting Cards Positioning")]
    //Fields for postitioning Cards
    public float maxAngle;
    public float spacing;
    public float xOffset;
    public float yOffset;
    public float Height;

    [Header("Variables For Adjusting Speeds")]
    public float animSpeed;
    private void Start() {
        GetComponent<Hand>().updateCardPositions += bendCards;
        Debug.Log("I Started");
    }

    public void bendCards(List<GameObject> hand) {
        float fullAngle = -maxAngle;
        float anglePerCard = fullAngle / hand.Count;
        float firstAngle = -(fullAngle / 2) + fullAngle * 0.1f;
        float yPosition = 0;
        
        for (int i = 0; i < hand.Count; i++) {
            //gets Children after they are instantiated
            Transform card = gameObject.transform.GetChild(i);
            cardPositioner cardPos = card.GetComponent<cardPositioner>();

            float angle = firstAngle + i * anglePerCard;

            float xPosition = spacing * (i - ((float)hand.Count - 1)/2);
            yPosition = 0 - (Mathf.Abs(angle) * Height);
            
            Vector3 rotation = new Vector3(0, 0, angle);
            Vector2 position = new Vector2(xPosition + xOffset, yPosition + yOffset);
            
            cardPos.setStart(position,rotation);
            cardPos.moveTo(position,(float)animSpeed);
            cardPos.rotateTo(rotation,(float)animSpeed);
            drawCard(cardPos, 1f);
        }
    }

    private void drawCard(cardPositioner card, float speed) {
        card.fadeTo(1, speed);
        card.scaleTo(Vector3.one, speed);
    }
}
