using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    private SpriteRenderer rend;
    public SpriteRenderer Rend { get { return rend; } }
    private Animator anim;
    public Animator Anim { get { return anim; } }
    private bool isInstantiated = false;

    public void OnInstantiate()
    {
        isInstantiated = true;
        Setup();
    }

    void Start()
    {
        if (!isInstantiated)
        {
            Setup();
        }
    }

    public void Setup()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        CreatureManager.instance.AnimData.SetVisuals(anim, rend);
        CreatureManager.instance.Creatures.Add(this);
    }

    void Update()
    {

    }

    public void Drop()
    {

    }
    public void Leave()
    {

    }
}
