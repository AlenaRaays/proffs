using System.IO;
using UnityEngine;

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
        if (File.Exists(settingsPath))
        {
            string json = JsonUtility.ToJson(currentSettings, true);
            File.WriteAllText(settingsPath, json);
            Debug.Log($"Примененные настройки: {json}");
        }
    }

    public void LoadSettings()
    {
        if (File.Exists(settingsPath))
        {
            string json = File.ReadAllText(settingsPath);
            currentSettings = JsonUtility.FromJson<GameSettings>(json);
        }
        else
        {
            currentSettings = new GameSettings();
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
