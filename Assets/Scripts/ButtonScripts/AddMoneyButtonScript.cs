using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AddMoneyButtonScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyAmount;
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        decimal moneyAmo = decimal.Parse(moneyAmount.text);
        MoneyManager.Instance.AddBalance(moneyAmo);
    }
}
