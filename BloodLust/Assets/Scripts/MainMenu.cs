using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void goToRegister()
    {
        SceneManager.LoadScene("Register");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
