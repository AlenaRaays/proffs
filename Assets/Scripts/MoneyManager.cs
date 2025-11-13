using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private static decimal _balance = 500;

    public static MoneyManager Instance;

    public static decimal Balance { get { return _balance; } set { _balance = value; } }

    public static event Action<decimal> Changed;

    private void Awake()
    {
        Instance = this;
    }

    public bool RemoveBalance(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            Changed?.Invoke(Balance);
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
    }
}
