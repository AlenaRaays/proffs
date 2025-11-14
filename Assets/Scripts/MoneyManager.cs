using System;
using System.IO;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private static decimal _balance = 500;
    private string moneypath;

    public static MoneyManager Instance;

    public static decimal Balance { get { return _balance; } set { _balance = value; } }

    public static event Action<decimal> Changed;

    [System.Serializable]
    private class MoneyData
    {
        public string balance;
    }

    private void Awake()
    {
        Instance = this;

        moneypath = Path.Combine(Application.persistentDataPath, "money.json");

        LoadBalance();
    }

    public void LoadBalance()
    {

        if (File.Exists(moneypath))
        {
            string json = File.ReadAllText(moneypath);
            MoneyData data = JsonUtility.FromJson<MoneyData>(json);

            if (data != null && !string.IsNullOrEmpty(data.balance))
            {
                Balance = decimal.Parse(data.balance);
                Debug.Log($"Загруженный баланс: {Balance}");
            }
            else
            {
                Balance = _balance;
                Debug.LogWarning("Данные баланса пустые, установлен по умолчанию");
            }
        }
        else
        {
            Balance = _balance;
            SaveMoney();
            Debug.Log("Файл не найден, создан новый с балансом по умолчанию");
        }
        Changed?.Invoke(Balance);
    }

    public bool RemoveBalance(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            Changed?.Invoke(Balance);
            SaveMoney();
            return true;
        }
        else
        {
            return false;
        }
    }


    public void AddBalance(decimal amount)
    {
        Balance += amount;
        Changed?.Invoke(Balance);
        SaveMoney();
    }

    private void SaveMoney()
    {
        MoneyData moneyData = new MoneyData();
        moneyData.balance = Balance.ToString();

        string json = JsonUtility.ToJson(moneyData, true);
        File.WriteAllText(moneypath, json);
        Debug.Log($"Сохраненный баланс: {Balance} в файл {moneypath}");
    }

}
