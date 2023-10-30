using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

public class OptionsScript : MonoBehaviour
{
    public AudioMixer audioMixer;

    public GameObject settingsPanel;
    public GameObject videoSettingsPanel;
    public GameObject volumeSettingsPanel;
    public GameObject keySettingsPanel;
    public GameObject buttonSettingsPanel;

    private bool _isOptions = false;
    
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] _resolutions;
    
    void Start()
    {
        GetResolution();
        Screen.fullScreen = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isOptions)
        {
            OpenOptions();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isOptions)
        {
            CloseOptions();
        }
    }

    // Options Function
    private void OpenOptions()
    {
        settingsPanel.SetActive(true);
        _isOptions = true;
    }
    
    public void CloseOptions()
    {
        settingsPanel.SetActive(false);
        _isOptions = false;
    }

    public void OpenVideoSettings()
    {
        buttonSettingsPanel.SetActive(false);
        videoSettingsPanel.SetActive(true);
    }
    
    public void CloseVideoSettings()
    {
        buttonSettingsPanel.SetActive(true);
        videoSettingsPanel.SetActive(false);
    }
    
    public void OpenVolumeSettings()
    {
        buttonSettingsPanel.SetActive(false);
        volumeSettingsPanel.SetActive(true);
    }
    
    public void CloseVolumeSettings()
    {
        buttonSettingsPanel.SetActive(true);
        volumeSettingsPanel.SetActive(false);
    }
    
    public void OpenKeySettings()
    {
        buttonSettingsPanel.SetActive(false);
        keySettingsPanel.SetActive(true);
    }
    
    public void CloseKeySettings()
    {
        buttonSettingsPanel.SetActive(true);
        keySettingsPanel.SetActive(false);
    }
    
    // Utils Function
    private void GetResolution()
    {
        _resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
                currentResolutionIndex = i;
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
