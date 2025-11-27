using System;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [SerializeField] private ItemsSpawner ActiveSpawner;

    public static event Action<ItemsSpawner> OnSpawnerSelected;

         
    private void Awake()
    {
        Instance = this;
    }
    public void SetActiveSpawner(ItemsSpawner spawner)
    {
        if (ActiveSpawner == null)
        {
            ActiveSpawner = spawner;
            ActiveSpawner.setSelected(true);
            OnSpawnerSelected?.Invoke(spawner);
            //подпись
        }
        if (ActiveSpawner != null)
        {
            ActiveSpawner.setSelected(false);
            //отписка
        }

        Debug.Log($"Active spawner set to: {(spawner != null ? spawner.name : "null")}");
        
    }

    public void SpawnAtActiveSpawner(int index)
    {
        if (ActiveSpawner !=null) 
        {
            ActiveSpawner.SpawnRequest(index);
        }
        else
        {
            Debug.Log("No active spawner selected!");
        }
           

       
    }

    

}
