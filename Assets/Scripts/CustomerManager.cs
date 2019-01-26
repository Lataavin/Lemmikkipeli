using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager instance;
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

    public CustomerData CustomerData;

    private List<Customer> customers = new List<Customer>();
    public List<Customer> Customers { get { return customers; } set { customers = value; } }

    public float falsePetMultiply = 1f;
    public float falsePetSubtract = 1f;
    public float customerAngle = 5f;

    public AnimationCurve hpR;
    public AnimationCurve hpG;
    public AnimationCurve hpB;
    public AnimationCurve hpW;

    public float customerToCreatureDistance = 1f;

    public Color32 GetHpColor(float p)
    {
        return new Color32((byte)hpR.Evaluate(p), (byte)hpG.Evaluate(p), (byte)hpB.Evaluate(p), 255);
    }

    void Start()
    {

    }
    float timer = 0.1f;
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            NextCustomer();

            timer = 99999999;
            //   this.enabled = false;
        }
    }

    public void TryMatch(TouchD t)
    {
        if (t.creature.Rend.sprite.name == Customers[0].Want.Rend.sprite.name && t.creature.Rend.material.name == Customers[0].Want.Rend.material.name)
        {
            Customers[0].Leave(true);
            t.creature.Leave();
        }
        else
        {
            t.creature.Drop(t);
            Customers[0].PatienceTimer *= falsePetMultiply;
            Customers[0].PatienceTimer += falsePetSubtract;
        }
        InputController.instance.AbortTouch(t);
    }
    public void NextCustomer(Customer c)
    {
        Customers.Remove(c);
        TestSpawner.instance.SpawnType(spawnable.customer);
        NextCustomer();
    }

    public void NextCustomer()
    {
        if (Customers.Count > 0)
        {
            Customers[0].SetFirst();
        }
        for (int i = 0; i < Customers.Count; i++)
        {
            Customers[i].MoveStep(i, (WorldManager.instance.worldSize / 2) + (i * customerAngle));
        }
    }

    public void CheckDistance(TouchD t, Transform trans)
    {
        if(Vector2.Distance(trans.position,Customers[0].distform.position) <= customerToCreatureDistance)
        {
            TryMatch(t);
        }
    }
}
