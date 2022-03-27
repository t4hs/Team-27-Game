using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    public void callSaveData()
    {
        StartCoroutine(SavePlayerData());
    }
    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("score", DBManager.score);
        WWW www = new WWW("thisiswhereiwillputtheurllol.com", form);
        yield return www;
        if(www.text == "0")
        {
            Debug.Log("Score updated");
        }
        else
        {
            Debug.Log("score update failed");
        }
    }
}
