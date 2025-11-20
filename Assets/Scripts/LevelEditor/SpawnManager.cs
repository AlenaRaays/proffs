using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [SerializeField] private ItemsSpawner ActiveSpawner;
    public System.Action<ItemsSpawner> OnSpawnerSelected;

         
    private void Awake()
    {
        Instance = this;
    }
    public void SetActiveSpawner(ItemsSpawner spawner)
    {
        if (ActiveSpawner != null)
        {
            ActiveSpawner.setSelected(false);
        }
        ActiveSpawner = spawner;

        if (ActiveSpawner != null)
        {
            ActiveSpawner.setSelected(true);
        }

        Debug.Log($"Active spawner set to: {(spawner != null ? spawner.name : "null")}");
        OnSpawnerSelected?.Invoke(spawner);
    }

    public void SpawnAtActiveSpawner(int index)
    {
        if (ActiveSpawner != null)
        {
            ActiveSpawner.SpawnRequest(index);
        }
        else
        {
            Debug.Log("No active spawner selected!");
        }
    }

    

}
