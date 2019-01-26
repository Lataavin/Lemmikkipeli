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
    public Transform pivoty;

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

        int dist = Random.Range(-150, 150);
        rend.sortingOrder = 500 + dist;
        pivoty.localPosition = new Vector3(pivoty.localPosition.x, pivoty.localPosition.y, pivoty.localPosition.z - ((float)dist / 100));

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
