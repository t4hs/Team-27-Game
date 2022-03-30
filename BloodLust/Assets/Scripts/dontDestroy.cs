using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dontDestroy : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene") Destroy(transform.gameObject);
    }
}
