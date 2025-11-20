using System;
using UnityEngine;
using UnityEngine.UI;

public class UIScrollerIndex : MonoBehaviour
{
    [SerializeField] private ItemsSpawner spawner;
    [SerializeField] private Button button;
    private void Start()
    {
        if (spawner == null)
        {
            spawner = FindFirstObjectByType<ItemsSpawner>();
        }

    }

    public void Send(int ind)
    {
        
        SpawnManager.Instance.SpawnAtActiveSpawner(ind);
    }


}
