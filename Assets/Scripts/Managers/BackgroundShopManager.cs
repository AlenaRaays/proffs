using System.IO;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.iOS;

public class BackgroundShopScript : MonoBehaviour
{
    [SerializeField] TMP_Text pricetext;
    [SerializeField] Button button;
    [SerializeField] Toggle toggle;
    [SerializeField] GameObject pricegroup;
    [SerializeField] private string backgroundID;

    private string shopstatspath;
    private bool wasbought;
    private decimal price;

    private static Dictionary<string, BackgroundShopScript> allBackgrounds = new Dictionary<string, BackgroundShopScript>();
    void Start()
    {
        ColorDefinition();
        price = decimal.Parse(pricetext.text);

        shopstatspath = Path.Combine(Application.persistentDataPath, "shopstats.json");

        if (!string.IsNullOrEmpty(backgroundID))
        {
            allBackgrounds[backgroundID] = this;
        }

        LoadStatement();
        SetupButtonListener();
        ColorDefinition();
    }

    private void SetupButtonListener()
    {
        button.onClick.RemoveAllListeners();

        if (!wasbought)
        {
            button.onClick.AddListener(BuyBackground);
        }
        else
        {
            button.onClick.AddListener(SelectBackground);
        }
    }
    private void ColorDefinition()
    {
        if (!wasbought)
        {
            pricetext.color = MoneyManager.Balance >= price ? Color.white : Color.red;

            pricegroup.SetActive(true);
            toggle.interactable = false;
        }
        else
        {
            pricegroup.SetActive(false);
            toggle.interactable = true;
        }

    }

    void BuyBackground()
    {
        if (wasbought) return;

        if (MoneyManager.Balance < price)
        {
            Debug.Log($"Денег недостаточно. Баланс: {MoneyManager.Balance}, Необходимо: {pricetext.text}");
            return;
        }

        wasbought = true;
        toggle.isOn = true;
        toggle.interactable = true;
        pricegroup.SetActive(false);

        MoneyManager.Instance.RemoveBalance(price);
        Debug.Log($"Покупка прошла успешно. Баланс: {MoneyManager.Balance}");

        SaveStatement();
        SetupButtonListener();
    }

    void SelectBackground()
    {
        if (wasbought)
        {
            toggle.isOn = true;
            ApplyBackground();
        }
    }

    private void ApplyBackground()
    {
        Debug.Log($"Примененный фон: {backgroundID}");
    }

    private ShopData GetShopData()
    {
        ShopData shopData = new ShopData();
        shopData.backgroundStates = new Dictionary<string, BackgroundState>();

        foreach (var bg in allBackgrounds)
        {
            shopData.backgroundStates[bg.Key] = new BackgroundState
            {
                wasBought = bg.Value.wasbought,
                isSelected = bg.Value.toggle.isOn
            };
        }

        return shopData;
    }

    private void LoadShopData(ShopData data)
    {
        if (data.backgroundStates == null) return;

        foreach (var bgState in data.backgroundStates)
        {
            if (allBackgrounds.ContainsKey(bgState.Key))
            {
                var background = allBackgrounds[bgState.Key];
                background.wasbought = bgState.Value.wasBought;
                background.pricegroup.SetActive(!bgState.Value.wasBought);
                background.toggle.isOn = bgState.Value.isSelected;
                background.toggle.interactable = bgState.Value.wasBought;

                background.SetupButtonListener();
                background.ColorDefinition();
            }
        }
    }

    void SaveStatement()
    {
        ShopData shopData = GetShopData();
        string json = JsonUtility.ToJson(shopData, true);
        File.WriteAllText(shopstatspath, json);
        Debug.Log("Покупка сохранена");

    }

    void LoadStatement()
    {
        if (File.Exists(shopstatspath))
        {
            string json = File.ReadAllText(shopstatspath);
            ShopData shopData = JsonUtility.FromJson<ShopData>(json);
            LoadShopData(shopData);
        }
    }

    private void Update()
    {
        if (!wasbought)
        {
            ColorDefinition();
        }
    }

    [System.Serializable]

    private class ShopData
    {
        public Dictionary<string, BackgroundState> backgroundStates;
    }
    private class BackgroundState
    {
        public bool wasBought;
        public bool isSelected;
    }
}

