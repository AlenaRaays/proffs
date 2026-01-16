using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemsSpawner : MonoBehaviour
{
    GameObject spawnpoint;
    [SerializeField] private Camera cam;

    private LevelItems target;
    private List<GameObject> spawnedItems = new List<GameObject>();

    MeshRenderer mesh;
    GameObject newItem;
    private bool isSelected = false;

    private void Awake()
    {
        if (target  == null)
        {
            target = FindFirstObjectByType<LevelItems>();
        }
        if (spawnpoint == null)
        {
            spawnpoint = this.gameObject;
        }

        SpawnManager.OnSpawnerSelected += OnOtherSpawnerSelected;
        SpawnManager.OnSpawnerDeselected += OnOtherSpawnerDeselected;
        
    }

    private void OnDestroy()
    {
        SpawnManager.OnSpawnerSelected -= OnOtherSpawnerSelected;
        SpawnManager.OnSpawnerDeselected -= OnOtherSpawnerDeselected;
    }

    private void OnOtherSpawnerSelected(ItemsSpawner spawner)
    {
        if (spawner != null && isSelected)
        {
            setSelected(false);
        }
    }

    private void OnOtherSpawnerDeselected(ItemsSpawner spawner)
    {
        if (spawner == this)
        {
            setSelected(false);
        }
    }


    private void OnMouseDown()
    {
        SpawnManager.Instance.SetActiveSpawner(this);
    }


    public void SpawnRequest(int index)
    {
        ObjectSpawn(index);
    }

    public void ObjectSpawn(int index)
    {
        if (target != null && target.prefabs != null &&
            index >= 0 && index < target.prefabs.Count &&
            target.prefabs[index] != null)
        {
            newItem = Instantiate(target.prefabs[index], Coords(), CoordsRotation());
            spawnedItems.Add(newItem);
            spawnpoint = Instantiate(spawnpoint, newItem.transform.position + Size() , CoordsRotation());
            if (cam != null)
            {
                MoveCameraAfterSpawn(newItem);

            }

            Debug.Log($"Spawned item at {name}. Total items: {spawnedItems.Count}");

        }
        else if (index == -1)
        {
            Destroy(newItem);
            ClearLastSpawnedItem();
        }
        else
        {
            Debug.LogError("Cannot spawn item. Check prefabs list and index!");
        }
    }

    

    public void ClearLastSpawnedItem()
    {
        if (spawnedItems.Count > 0)
        {
            int lastind = spawnedItems.Count - 1;

            if (spawnedItems[lastind] != null)
            {
                Destroy(spawnedItems[lastind]);
            }
            spawnedItems.RemoveAt(lastind);
        }
    }

    private void MoveCameraAfterSpawn(GameObject SpawnedItem)
    {

        if (spawnpoint != null && cam != null)
        {
            cam.transform.position = new Vector3(
                spawnpoint.transform.position.x,
                cam.transform.position.y,
                cam.transform.position.z
            );
        }
    }

    public List<GameObject> GetSpawnedItems()
    {
        return new List<GameObject>(spawnedItems);
    }

    public Vector3 Size()
    {
        if(newItem.transform.childCount <= 0)
        {
            mesh = newItem.GetComponent<MeshRenderer>();
        }
        else
        {
            mesh = newItem.GetComponentInChildren<MeshRenderer>();
        }

        
        Debug.Log(mesh.name + mesh.bounds.size);

        float xvector = mesh.bounds.size.x;

        Vector3 vector = new Vector3(xvector, 0, 0);
     
        return vector;
    }
    public Vector3 Coords()
    {
        return spawnpoint != null ? spawnpoint.transform.position : transform.position;
    }

    public Quaternion CoordsRotation()
    { 
        return Quaternion.identity;
    }

    public void setSelected(bool selected)
    {
        isSelected = selected;
    }
}
