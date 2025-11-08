using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Coroutine _coroutine;

    [SerializeField] float delay = 2f;
    private string TipText()
    {
        switch (gameObject.name)
        {
            case "Button_Play": return "Начать игру";
            case "Button_Editor": return "Редактор уровней";
            case "Button_Exit": return "Выйти из игры";
            case "Button_Settings": return "Настройки";
            case "Button_PlusMoney": return "Магазин";
            case "Button_Shop": return "Магазин фонов";
            case "Button_Paint": return "Изменить фон";
            default: return ""; //else если по простому
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        _coroutine = StartCoroutine(ShowAfterDelay(delay)); //запускаем корутин
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(_coroutine);
        TooltipSystem.instance.HideToolTip();
    }

    private IEnumerator ShowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // ждем задержку
        TooltipSystem.instance.ShowToolTip(TipText()); // вытащили метод из другого скрипта
    }
}
