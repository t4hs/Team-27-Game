using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Registeration : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    public TMP_InputField confirmPasswordField;
    public Button registerButton;

    public void callRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("http://localhost/sqlconnect/register.php", form);
        yield return www;
        if(www.text == "0")
        {
            Debug.Log("User created successfully");
        }
        else { Debug.Log("ERROR: User creation was unsuccessful. error #" + www.text); }
    }

    public void verifyUserInput()
    {
        registerButton.interactable = (nameField.text.Length > 5 && passwordField.text.Length > 5 && passwordField.text == confirmPasswordField.text);
    }
}
