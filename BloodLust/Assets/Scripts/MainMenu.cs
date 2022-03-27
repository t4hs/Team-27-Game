using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void goToLeaderboard()
    {
        SceneManager.LoadScene("LeaderboardScene");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
