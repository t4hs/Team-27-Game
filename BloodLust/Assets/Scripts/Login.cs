using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    public Button loginButton;

    public void callLogin()
    {
        StartCoroutine(LoginUser());
    }

    IEnumerator LoginUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("riseoffighters.netlify.app/sqlconnect/login.php", form);
        yield return www;
        if (www.text[0] == '0')
        {
            DBManager.username = nameField.text;
            DBManager.score = int.Parse(www.text.Split('\t')[1]);
        }
        else
        {
            Debug.Log("User login failed. Error" + www.text);
        }
    }
    public void verifyUserInput()
    {
        loginButton.interactable = (nameField.text.Length > 5 && passwordField.text.Length > 5);
    }
}