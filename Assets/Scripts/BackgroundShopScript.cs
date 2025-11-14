using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundShopScript : MonoBehaviour
{
    [SerializeField] TMP_Text pricetext;
    [SerializeField] Button button;


    void Start()
    {
        button.onClick.AddListener(Byingbackground);

    }

    void Byingbackground()
    {
        if (MoneyManager.Balance < decimal.Parse(pricetext.text))
        {
            pricetext.color = Color.red;
            Debug.Log($"Денег недостаточно. Баланс: {MoneyManager.Balance}, Необходимо: {pricetext}");
        }
        else
        {
            pricetext.color = Color.white;
            MoneyManager.Instance.RemoveBalance(MoneyManager.Balance);
            Debug.Log($"Покупка совершена! Баланс: {MoneyManager.Balance}");
        }
    }
}
