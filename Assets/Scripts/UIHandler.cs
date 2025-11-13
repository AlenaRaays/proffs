using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _balanceText;
    private void Start()
    {
        BalanceUIUpdate(MoneyManager.Balance);
    }

    private void OnEnable()
    {
        MoneyManager.Changed += BalanceUIUpdate;
    }

    private void OnDisable()
    {
        MoneyManager.Changed -= BalanceUIUpdate;
    }

    private void BalanceUIUpdate(decimal ammony)
    {
        if (ammony >= 1000)
        {
            _balanceText.text = $"{ammony / 1000}k";
        }
        else
        {
            _balanceText.text = ammony.ToString();
        }
    }
}
