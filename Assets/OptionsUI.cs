using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject confirmationScreen;
    public AudioMixer mixer;
    public AudioMixer musicMixer;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI musicText;
    public Slider volumeSlider;
    public Slider musicSlider;


    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        optionsMenu.SetActive(false);
    }
    private void Update()
    {
        volumeText.text = "SFX: " + (volumeSlider.value * 100).ToString("N0");
        musicText.text = "Music: " + (musicSlider.value * 100).ToString("N0");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseOptions();
        }
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        FindObjectOfType<AudioManager>().Play("ButtonSelect");
    }
    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        //FindObjectOfType<AudioManager>().Play("ButtonSelect");
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
    }
    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
    }

    public void OpenConfirmation()
    {
        confirmationScreen.SetActive(true);
    }
    public void CloseConfirmation()
    {
        confirmationScreen.SetActive(false);
    }
}
