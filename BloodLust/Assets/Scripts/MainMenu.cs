using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public ServerConnection con;
    public GameObject menu;
    public GameObject loginMenu;
    [SerializeField] AudioMixer mixer;
    public Slider volume;
    public Slider music;
    public Slider sfx;

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
    public void Start()
    {
        volume.value = PlayerPrefs.GetFloat("MasterVolume", volume.value);
        music.value = PlayerPrefs.GetFloat("MusicVolume", music.value);
        sfx.value = PlayerPrefs.GetFloat("SFXVolume", sfx.value);
        mixer.SetFloat("MasterVolume", Mathf.Log10(volume.value) * 30);
        mixer.SetFloat("MusicVolume", Mathf.Log10(music.value) * 30);
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfx.value) * 30);
    }
}
