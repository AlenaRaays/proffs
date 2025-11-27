using System;
using UnityEngine;
using UnityEngine.UI;

public class UIScrollerIndex : MonoBehaviour
{
    [SerializeField] private ItemsSpawner spawner;
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

        //на остальные toggle тоже навесить скрипт чтобы отправляли индекс
    }


}
