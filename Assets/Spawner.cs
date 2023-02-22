using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour
{
    public List<BoxCollider2D> spawnZones;

    public GameObject spawnPrefab;
    
    public List<GameObject> SpawnWithinBounds(GameObject parent = null, int numberOfInstances = 1) {

        List<GameObject> spawnedObjects = new List<GameObject>();

        for(int i = 0; i < numberOfInstances; i++) {

            Bounds bounds = spawnZones[Random.Range(0, spawnZones.Count -1)].bounds;
            Vector2 position = GetRandomPositionInBounds(bounds);

            List<GameObject> newObjects = Spawn(position, parent);

            spawnedObjects = spawnedObjects.Concat<GameObject>(newObjects).ToList();

        }

        return spawnedObjects;

    }

    public List<GameObject> Spawn(Vector2 position, GameObject parent = null, int numberOfInstances = 1) {

        List<GameObject> spawnedObjects = new List<GameObject>();

        for(int i = 0; i < numberOfInstances; i++) {

            GameObject spawnedObject = Instantiate(spawnPrefab, position, Quaternion.identity);
            ISpawnable spawnable = spawnedObject.GetComponent<ISpawnable>();
            if(parent != null) {
                spawnedObject.transform.SetParent(parent.transform, false);    
            }

            spawnedObjects.Add(spawnedObject);
        }

        return spawnedObjects;
    }

    private Vector2 GetRandomPositionInBounds(Bounds bounds) {

        Vector2 spawnPoint = new Vector2(Random.Range(bounds.min.x, bounds.max.x),
                                         Random.Range(bounds.min.y, bounds.max.y));

        return bounds.ClosestPoint(spawnPoint);
    }
}
