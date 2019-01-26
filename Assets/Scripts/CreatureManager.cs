using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    public static CreatureManager instance;

    private List<Creature> creatures = new List<Creature>();
    public List<Creature> Creatures { get { return creatures; } set { creatures = value; } }

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

    void Start()
    {

    }

    void Update()
    {

    }
}
