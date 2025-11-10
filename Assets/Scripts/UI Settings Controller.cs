using System;
using TMPro;
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

        int resIndexes = System.Array.IndexOf(Resolutiondropdown.options.ToArray(), settings.Resolution);


        Resolutiondropdown.onValueChanged.AddListener(OnResolutionChanged);
        WindowModeToggle.isOn = settings.WindowModeEnabled;
        MusicEnableToggel.isOn = settings.MusicEnabled;
        LanguageDropDown.onValueChanged.AddListener(OnLanguageChanged);
    }

    void OnResolutionChanged(int index)
    {
        SettingsManager.instance.currentSettings.Resolution = Resolutiondropdown.options[index].text;
        SettingsManager.instance.SaveSettings();
    }

    void OnLanguageChanged(int index)
    {
        SettingsManager.instance.currentSettings.Language = LanguageDropDown.options[index].text;
        SettingsManager.instance.SaveSettings();
    }
}
