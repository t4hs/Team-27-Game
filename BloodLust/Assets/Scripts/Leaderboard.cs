using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    public GameObject bronzeLeaderboard;
    public GameObject silverLeaderboard;
    public GameObject goldLeaderboard;
    public GameObject platinumLeaderboard;
    public GameObject diamondLeaderboard;
    int positionQuantity;
    int positionsInRank;

    void Start()
    {
        positionQuantity = 50;
        positionsInRank = 10;
        StartCoroutine(getLeaderboard());
    }
    public void goToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void displayLeaderboard(string[] values)
    {
        //getting the top 100 values
        string[] topPlayers = new string[positionQuantity*2];

        //enter users and scores into an array
        for (int i = 1; i < (positionQuantity*2)+1; i++)
        {
            if (i< values.Length -1) { topPlayers[i-1] = values[i];}
            else { topPlayers[i - 1] = ""; }
        }
        //output the leaderboard
        int topPlayerItterator = 0;
        GameObject entries = diamondLeaderboard.transform.GetChild(1).gameObject;
        outputLeaderboard(entries, topPlayers, ref topPlayerItterator);
        entries = platinumLeaderboard.transform.GetChild(1).gameObject;
        outputLeaderboard(entries, topPlayers, ref topPlayerItterator);
        entries = goldLeaderboard.transform.GetChild(1).gameObject;
        outputLeaderboard(entries, topPlayers, ref topPlayerItterator);
        entries = silverLeaderboard.transform.GetChild(1).gameObject;
        outputLeaderboard(entries, topPlayers, ref topPlayerItterator);
        entries = bronzeLeaderboard.transform.GetChild(1).gameObject;
        outputLeaderboard(entries, topPlayers, ref topPlayerItterator);

    }

    IEnumerator getLeaderboard()
    {
        WWW www = new WWW("riseoffighters.000webhostapp.com/leaderboardGet.php");
        yield return www;
        if (www.text[0] == '0')
        {
            Debug.Log(www.text);
            string[] leaderboardArray = www.text.Split('\t');
            displayLeaderboard(leaderboardArray);
        }
        else
        {
            Debug.Log("Fetching leaderboard failed. Error" + www.text);
        }
    }

    void outputLeaderboard(GameObject entries, string[] players, ref int itterator)
    {
        for (int pos = 0; pos < positionsInRank; pos++)
        {
            GameObject entry = entries.transform.GetChild(pos).gameObject;
            GameObject usernameText = entry.transform.GetChild(1).gameObject;
            GameObject scoreText = entry.transform.GetChild(2).gameObject;
            usernameText.GetComponent<TMP_Text>().text = players[itterator];
            scoreText.GetComponent<TMP_Text>().text = players[itterator + 1];
            itterator += 2;

        }
    }
}
