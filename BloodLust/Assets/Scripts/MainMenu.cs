using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public ServerConnection con;
    public GameObject menu;
    public GameObject loginMenu;
    public void goToLeaderboard()
    {
        SceneManager.LoadScene("LeaderboardScene");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void playClicked()
    {
        if (DBManager.LoggedIn)
        {
            con.connectPhoton();
        }
        else
        {
            menu.SetActive(false);
            loginMenu.SetActive(true);
        }
    }
}
