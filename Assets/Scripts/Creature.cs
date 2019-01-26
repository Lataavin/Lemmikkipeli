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
        // pivoty.localPosition = new Vector3(pivoty.localPosition.x, pivoty.localPosition.y, pivoty.localPosition.z - ((float)dist / 100));

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + ((150f - dist) / 100f), transform.localPosition.z);

    }

    void Update()
    {

    }


    public void Leave()
    {

    }

    public void Drop(TouchD t)
    {
        GetComponent<CreatureBehaviour>().enabled = true;
    }

    public void Grab(TouchD t)
    {
        GetComponent<CreatureBehaviour>().enabled = false;
        Vector2 point = Camera.main.ScreenToWorldPoint(new Vector3(t.curPoint.x, t.curPoint.y, pivoty.position.z));
        transform.position = new Vector3(point.x, point.y, transform.position.z);
    }
    public void Move(TouchD t)
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(new Vector3(t.curPoint.x, t.curPoint.y, pivoty.position.z));
        transform.position = new Vector3(point.x, point.y, transform.position.z);
        CustomerManager.instance.CheckDistance(t, pivoty);
    }
}
