using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnpoint;
    [SerializeField] private Camera cam;

    private LevelItems target;
    private GameObject item;
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
    }

    private void OnMouseDown()
    {
        SpawnManager.Instance.SetActiveSpawner(this);
    }

    public void SpawnRequest(int index)
    {
        //проверка на подписку на событие??
        ObjectSpawn(index);
    }

    public void ObjectSpawn(int index)
    {
        if (target != null && target.prefabs != null &&
            index >= 0 && index < target.prefabs.Count &&
            target.prefabs[index] != null)
        {
            item = Instantiate(target.prefabs[index], Coords(), CoordsRotation());
            //тут отписка от предыдуещго??
        }

        if (cam != null && isSelected)
        {
            MoveCameraAfterSpawn(item);
        }
    }

    private void MoveCameraAfterSpawn(GameObject SpawnedItem)
    {

        cam.transform.position = new Vector3(
            spawnpoint.transform.position.x,
            cam.transform.position.y,
            cam.transform.position.z
        );
    }

    public Vector3 Coords()
    {
        return spawnpoint.transform.position;
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
