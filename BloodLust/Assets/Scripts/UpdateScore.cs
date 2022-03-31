using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;


public class UpdateScore : MonoBehaviour
{
    readonly string postURL = "riseoffighters.000webhostapp.com/saveData.php";

    public void callSaveData()
    {
        StartCoroutine(SavePlayerData(DBManager.username, DBManager.score.ToString()));
    }

    IEnumerator SavePlayerData(string name, string score)
    {
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("name", name));
        wwwForm.Add(new MultipartFormDataSection("score", score));

        UnityWebRequest www = UnityWebRequest.Post(postURL, wwwForm);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            if (www.downloadHandler.text == "0")
            {
                Debug.Log("Score updated");
            }
            else
            {
                Debug.Log("score update failed. Error " + www.downloadHandler.text);
            }
        }

        /*
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("score", DBManager.score);
        WWW www = new WWW("riseoffighters.000webhostapp.com/saveData.php", form);
        Debug.Log("website reached");
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("Score updated");
        }
        else
        {
            Debug.Log("score update failed");
        }
        */
    }
}
