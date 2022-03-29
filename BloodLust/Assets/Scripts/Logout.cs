using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logout : MonoBehaviour
{
    public GameObject logoutButton;

    public void activateLogoutButton()
    {
        logoutButton.SetActive(DBManager.LoggedIn);
    }
    public void logout()
    {
        DBManager.Logout();
    }
}
