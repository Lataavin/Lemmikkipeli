using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum spawnable
{
    none = 0,
    creature = 1,
    customer = 2,
    prop = 3,
}

[System.Serializable]
public class SpawnThing
{
    public GameObject prefab;
    public int amount;
    public spawnable type;
}
[System.Serializable]
public class SpawnRandom
{
    public List<GameObject> prefabs;
    public int amount;
    public spawnable type;
}

public class TestSpawner : MonoBehaviour
{
    public static TestSpawner instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //   DontDestroyOnLoad(gameObject);
    }


    public List<SpawnThing> spawnAll = new List<SpawnThing>();
    public List<SpawnRandom> spawnRandom = new List<SpawnRandom>();

    void Start()
    {
        MainSpawn();
        CustomerManager.instance.NextCustomer();
    }

    public void MainSpawn()
    {
        for (int i = 0; i < spawnAll.Count; i++)
        {
            for (int j = 0; j < spawnAll[i].amount; j++)
            {
                Spawn(spawnAll[i].prefab, spawnAll[i].type);
            }
        }
        for (int i = 0; i < spawnRandom.Count; i++)
        {
            if (spawnRandom[i].prefabs.Count > 0)
            {
                for (int j = 0; j < spawnAll[i].amount; j++)
                {
                    Spawn(spawnRandom[i].prefabs[Random.Range(0, spawnRandom[i].prefabs.Count)], spawnRandom[i].type);
                }
            }
        }
    }

    public void SpawnType(spawnable type)
    {
        for(int i = 0; i < spawnAll.Count;i++)
        {
            if(spawnAll[i].type == type)
            {
                Spawn(spawnAll[i].prefab,type);
                return;
            }
        }
    }

    private void Spawn(GameObject prefab, spawnable type)
    {
        GameObject newCreature = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        switch(type)
        {
            case spawnable.creature:
                newCreature.GetComponent<Creature>().OnInstantiate();
                break;
            case spawnable.customer:
                newCreature.GetComponent<Customer>().OnInstantiate();
                break;
        }
    }
}
