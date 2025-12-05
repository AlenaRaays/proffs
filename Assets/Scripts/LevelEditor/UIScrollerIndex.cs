using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIScrollerIndex : MonoBehaviour
{
    [SerializeField] private int index;

    private Toggle toggle;
    private void Start()
    {
        toggle = GetComponent<Toggle>();

        if (toggle != null )
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            SendIndex();
        }
    }

    public void SendIndex()
    {
        SpawnManager.Instance.SpawnAtActiveSpawner(index);
    }

    public void Send(int ind)
    { 
        SpawnManager.Instance.SpawnAtActiveSpawner(ind);

    }


}
