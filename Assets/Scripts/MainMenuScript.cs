using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuScript : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    public GameObject settingsMenu;

    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        //if playerpref has no value for music volume, set it to 1
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1f);
        }
        //if playerpref has no value for sfx volume, set it to 1
        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1f);
        }

        audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("sfxVolume"));
        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("musicVolume"));
        backgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void SettingsMenu()
    {
        if (settingsMenu.activeInHierarchy)
        {
            //if scene is "Game" timescale 1
            if (SceneManager.GetActiveScene().name == "Game")
            {
                Time.timeScale = 1;
            }
            settingsMenu.SetActive(false);
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "Game")
            {
                Time.timeScale = 0;
            }
            settingsMenu.SetActive(true);
            musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
        PlayerPrefs.Save();
    }
}