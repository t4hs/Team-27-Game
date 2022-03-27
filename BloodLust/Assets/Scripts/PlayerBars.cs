using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBars : MonoBehaviour
{
    public GameObject healthBar1;
    public GameObject energyBar1;
    public GameObject healthBar2;
    public GameObject energyBar2;
    
    public void innit(bool isLocal) {
        healthBar1 = PlayerInfo.instance.healthBar1;
        energyBar1 = PlayerInfo.instance.energyBar1;
        healthBar1 = PlayerInfo.instance.healthBar2;
        energyBar2 = PlayerInfo.instance.energyBar2;
    }
}
