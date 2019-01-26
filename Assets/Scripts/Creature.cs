using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    private SpriteRenderer rend;
    public SpriteRenderer Rend { get { return rend; } }
    private bool isInstantiated = false;

    public void OnInstantiate()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
        CreatureManager.instance.Creatures.Add(this);
        isInstantiated = true;
    }

    void Start()
    {
        if (!isInstantiated)
        {
            rend = GetComponentInChildren<SpriteRenderer>();
            CreatureManager.instance.Creatures.Add(this);
        }
    }

    void Update()
    {

    }
}
