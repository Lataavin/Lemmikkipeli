using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnThing
{
    public GameObject prefab;
    public int amount;
}
[System.Serializable]
public class SpawnRandom
{
    public List<GameObject> prefabs;
    public int amount;
}

public class TestSpawner : MonoBehaviour
{
    public List<SpawnThing> spawnAll = new List<SpawnThing>();
    public List<SpawnRandom> spawnRandom = new List<SpawnRandom>();


    void Start()
    {
        for (int i = 0; i < spawnAll.Count; i++)
        {
            for (int j = 0; j < spawnAll[i].amount; j++)
            {
                GameObject newCreature = Instantiate(spawnAll[i].prefab, Vector3.zero, Quaternion.identity);
            }
        }
        for (int i = 0; i < spawnRandom.Count; i++)
        {
            if (spawnRandom[i].prefabs.Count > 0)
            {
                for (int j = 0; j < spawnAll[i].amount; j++)
                {
                    GameObject newCreature = Instantiate(spawnRandom[i].prefabs[Random.Range(0, spawnRandom[i].prefabs.Count)], Vector3.zero, Quaternion.identity);
                }
            }
        }
    }
}
