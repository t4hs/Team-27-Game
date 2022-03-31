using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInfo : MonoBehaviour
{
    [Header("Set From The Game Scene")]
    public Transform[] characterSpawns;
    public Hand hand;
    public GameObject healthBar1, healthBar2, energyBar1, energyBar2;
    public GameObject winScreen, LoseScreen;
    public GameObject cardSpawn;

    [Header("Drag From Resources/Icons Folder")]
    public List<nameIcons> typeIcons;
    
    public static PlayerInfo instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                instance = this;
            }
        }
    }



    public GameObject GetCard(int id, string type, int damage)
    {
        /*GameObject card = characterDatabase[id].CardPrefab;
        
        return card;*/
        return null;
    }


}
