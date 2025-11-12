using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;
    private string settingsPath;
    public GameSettings currentSettings;

    private void Awake()
    {
        instance = this;

        settingsPath = Path.Combine(Application.persistentDataPath, "settings.json");

        LoadSettings();
    }

    public void SaveSettings()
    {
        try
        {
            string json = JsonUtility.ToJson(currentSettings, true);
            File.WriteAllText(settingsPath, json);
            Debug.Log($"Настройки сохранены: {json}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка сохранения настроек: {e.Message}");
        }
    }

    public void LoadSettings()
    {
        if (File.Exists(settingsPath))
        {
            string json = File.ReadAllText(settingsPath);
            currentSettings = JsonUtility.FromJson<GameSettings>(json);
            Debug.Log($"Настройки загружены: {json}");
        }
        else
        {
            currentSettings = new GameSettings();

            currentSettings.Resolution = "1920:1080";
            currentSettings.WindowModeEnabled = false;
            currentSettings.MusicEnabled = true;
            currentSettings.Language = "English";

            SaveSettings();
        }


    }
}
[SerializeField]
public class GameSettings
{
    public string Resolution;
    public bool WindowModeEnabled;
    public bool MusicEnabled;
    public string Language;
}
