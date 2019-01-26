using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    public static CreatureManager instance;
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

    private List<Creature> creatures = new List<Creature>();
    public List<Creature> Creatures { get { return creatures; } set { creatures = value; } }

    public AnimalData animData;
    public AnimalData AnimData { get { return animData; } set { animData = value; } }

    public Creature GetRandomCreature()
    {
        return Creatures[Random.Range(0, Creatures.Count)];
    }
}
