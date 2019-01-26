using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private bool isInstantiated = false;
    private Creature want;
    public Creature Want { get { return want; } set { want = value; } }
    public float patience = 15f;
    public float Patience { get { return patience; } set { patience = value; if (patience <= 0) { Leave(false); } } }

    public void OnInstantiate()
    {
        CustomerManager.instance.Customers.Add(this);
        CheckWant();
        isInstantiated = true;
        this.enabled = false;
    }

    void Start()
    {
        if (!isInstantiated)
        {
            CustomerManager.instance.Customers.Add(this);
            CheckWant();
            this.enabled = false;
        }
    }
    void Update()
    {
        Patience -= Time.deltaTime;
    }

    public void CheckWant()
    {
        if (Want == null)
        {
            Want = CreatureManager.instance.GetRandomCreature();
        }
    }

    public void Leave(bool happy)
    {
        CustomerManager.instance.NextCustomer();
    }

    public void MoveStep()
    {

    }
}
