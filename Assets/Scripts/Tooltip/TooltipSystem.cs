using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem instance; // для того, чтобы мы могли к методам обращаться!

    [SerializeField] GameObject ToolTip;
    [SerializeField] TextMeshProUGUI ToolTipText;

    private RectTransform rect; //для получения ширина тултип
    

    void Awake()
    {
        instance = this;
        HideToolTip(); //скрыли

        rect = ToolTip.GetComponent<RectTransform>();
    }

    private void UpdatePosition()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        Vector2 mousePos = Input.mousePosition;

        float width = rect.rect.width;
        float height = rect.rect.height;

        if (mousePos.y >= Screen.height / 2f)
        {
            if (mousePos.x <= Screen.width / 2f)
            {
                ToolTip.transform.position = mousePos + new Vector2(width / 4f, -40f);
            }
            else if (mousePos.x > Screen.width / 2f)
            {
                ToolTip.transform.position = mousePos + new Vector2(-width / 4f, -50f);
            }
        }
        else if (mousePos.y <= Screen.height / 2)
        {
            ToolTip.transform.position = mousePos + new Vector2(width / 4f, -30f);
        }
    }

    public void ShowToolTip(string text)
    {
        UpdatePosition();
        ToolTip.SetActive(true);
        ToolTipText.text = text; //Текст не в ручную, а через свитч в скрипте на кнопках
    }

    public void HideToolTip()
    {
        ToolTip.SetActive(false); 
    }
    void Update() // Обновляем, чтобы подсказка ехала за курсором
    {
        if (ToolTip.activeSelf)
        {
            UpdatePosition();
        }
    }

    
}
