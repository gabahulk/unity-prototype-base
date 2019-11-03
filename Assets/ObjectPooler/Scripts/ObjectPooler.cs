using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A simple ObjectPooler class for GameObjects
/// Usage: 
/// Instantiate a pool passing your prefab.
/// Instead of instantiating your prefab, get one from the pool using the GetObject() method.
/// Instead of killing your object, just set it inactive using the SetActive() method.
/// </summary>
public class ObjectPooler
{
    private List<GameObject> pooledObjects;
    private GameObject pooledObjectPrefab;

    public ObjectPooler(int initialNumberOfInstances, GameObject pooledObjectPrefab)
    {
        this.pooledObjectPrefab = pooledObjectPrefab;
        pooledObjects = new List<GameObject>(initialNumberOfInstances);
        for (int i = 0; i < initialNumberOfInstances; i++)
        {
            var item = Object.Instantiate(pooledObjectPrefab);
            pooledObjects.Add(item);
            item.SetActive(false);
        }
    }

    public GameObject GetObject()
    {
        if (hasAvailableObject())
        {
            return GetAvailableObject();
        }

        return CreateNewPooledObject();
    }

    private GameObject GetAvailableObject()
    {
        foreach (GameObject item in pooledObjects)
        {
            if (!item.activeSelf)
            {
                item.SetActive(true);
                return item;
            }
        }

        return null;
    }


    private GameObject CreateNewPooledObject()
    {
        GameObject obj = Object.Instantiate(pooledObjectPrefab);
        obj.SetActive(true);
        pooledObjects.Add(obj);

        return obj;
    }

    private bool hasAvailableObject()
    {
        foreach (GameObject item in pooledObjects)
        {
            if (!item.activeSelf)
            {
                return true;
            }
        }

        return false;
    }
}
