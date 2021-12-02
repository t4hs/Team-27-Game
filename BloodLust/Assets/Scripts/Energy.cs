using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public float maxEnergy;
    public float currentEnergy;
    // Start is called before the first frame update
    void Start()
    {
        maxEnergy = 10;
        currentEnergy = 0;
    }
    //increase skill points
    void addSkillPoints(int combo)
    {
        currentEnergy += combo;
    }
}
