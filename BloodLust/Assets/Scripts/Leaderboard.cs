using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    GameObject bronzeLeaderboard;
    GameObject silverLeaderboard;
    GameObject goldLeaderboard;
    GameObject platinumLeaderboard;
    GameObject diamondLeaderboard;

    public void goToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
