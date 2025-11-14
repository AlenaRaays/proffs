using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsController : MonoBehaviour
{
    public TMP_Dropdown Resolutiondropdown;
    public Toggle WindowModeToggle;
    public Toggle MusicEnableToggel;
    public TMP_Dropdown LanguageDropDown;


    void Start()
    {
        GameSettings settings = SettingsManager.instance.currentSettings;

        int resIndex = FindDropdownIndex(Resolutiondropdown, settings.Resolution);
        Resolutiondropdown.value = resIndex;
        Resolutiondropdown.onValueChanged.AddListener(OnResolutionChanged);

        WindowModeToggle.isOn = settings.WindowModeEnabled;
        WindowModeToggle.onValueChanged.AddListener(OnWindowModeChanged);

        MusicEnableToggel.isOn = settings.MusicEnabled;
        MusicEnableToggel.onValueChanged.AddListener(OnMusicEnableChanged);

        int tagIndex = FindDropdownIndex(LanguageDropDown, settings.Language);
        LanguageDropDown.value = tagIndex;
        LanguageDropDown.onValueChanged.AddListener(OnLanguageChanged);

    }

    void OnResolutionChanged(int index)
    {
        
        SettingsManager.instance.currentSettings.Resolution = Resolutiondropdown.options[index].text;
    }
    void OnWindowModeChanged(bool isOn)
    {
        SettingsManager.instance.currentSettings.WindowModeEnabled = isOn;
    }

    void OnMusicEnableChanged(bool isOn)
    {
        SettingsManager.instance.currentSettings.MusicEnabled = isOn;
    }

    void OnLanguageChanged(int index)
    {
        SettingsManager.instance.currentSettings.Language = LanguageDropDown.options[index].text;

    }

    private int FindDropdownIndex(TMP_Dropdown Resolutiondropdown, string value)
    {
        for (int i = 0; i < Resolutiondropdown.options.Count; i++)
        {
            if (Resolutiondropdown.options[i].text == value)
            {
                return i;
            }
        }
        return -1;
    }
}
