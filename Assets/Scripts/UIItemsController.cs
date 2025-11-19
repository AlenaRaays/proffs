using System;
using UnityEngine;

public class UIItemsController : MonoBehaviour
{
    public static event Action<int> OnSelected;

    public void OnHeld(int index)
    {
        OnSelected?.Invoke(index);


    }
}
