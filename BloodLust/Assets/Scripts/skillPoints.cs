using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillPoints : MonoBehaviour
{
    public int maxSkillPoints;
    public int currentSkillPoints;
    // Start is called before the first frame update
    void Start()
    {
        maxSkillPoints = 10;
        currentSkillPoints = 0;
    }
    //increase skill points
    void addSkillPoints(int combo)
    {
        currentSkillPoints += combo;
    }
}
