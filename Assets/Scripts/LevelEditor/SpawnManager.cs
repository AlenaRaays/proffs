using System;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public ItemsSpawner ActiveSpawner {  get; private set; }

    public static event Action<ItemsSpawner> OnSpawnerSelected;
    public static event Action<ItemsSpawner> OnSpawnerDeselected;

         
    private void Awake()
    {
        Instance = this;
    }

    public void SetActiveSpawner(ItemsSpawner spawner)
    {
        if (ActiveSpawner == spawner)
        {
            DeselectCurrentSpawner();
            Debug.Log($"Active spawner deselected: {(spawner != null ? spawner.name : "null")}");
            return;
        }

        if (ActiveSpawner != null)
        {
            DeselectCurrentSpawner();
        }

        ActiveSpawner = spawner;

        if (ActiveSpawner != null)
        {
            ActiveSpawner.setSelected(true);
            OnSpawnerSelected?.Invoke(ActiveSpawner);
            Debug.Log($"Active spawner set to: {spawner.name}");
        }
        else
        {
            Debug.Log("No active spawner selected!");
        }

    }


    private void DeselectCurrentSpawner()
    {
        if ( ActiveSpawner != null )
        {
            ActiveSpawner.setSelected(false);
            OnSpawnerDeselected?.Invoke( ActiveSpawner );
        }

        ActiveSpawner = null;
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
