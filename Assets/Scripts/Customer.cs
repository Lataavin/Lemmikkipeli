using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private bool isInstantiated = false;
    private Creature want;
    public Creature Want { get { return want; } set { want = value; } }
    public float patience = 15f;

    private float patienceTimer = 0;
    public float PatienceTimer { get { return patienceTimer; } set { patienceTimer = value; UpdateHp(); if (patienceTimer >= 1) { Leave(false); } } }

    public SpriteRenderer myRend;
    public SpriteRenderer wantRend;
    public GameObject wantRendGameObject;

    public Transform hpPivot;
    public SpriteRenderer hpBar;

    public void OnInstantiate()
    {
        CustomerManager.instance.Customers.Add(this);
        CheckWant();
        isInstantiated = true;
        this.enabled = false;
        wantRendGameObject.SetActive(false);
        CustomerManager.instance.CustomerData.SetCustomerVisuals(myRend);
    }

    void Start()
    {
        if (!isInstantiated)
        {
            OnInstantiate();
        }
    }

    void Update()
    {
        PatienceTimer += Time.deltaTime / patience;
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
        CustomerManager.instance.NextCustomer(this);
        Destroy(gameObject);
    }

    public void MoveStep(int nr, float angle)
    {
        myRend.sortingOrder = -3000 - nr;
        transform.localPosition = new Vector3(angle, 0, 0);
    }

    public void SetFirst()
    {
        CheckWant();
        wantRend.sprite = Want.Rend.sprite;
        wantRend.material = Want.Rend.material;
        wantRendGameObject.SetActive(true);
        hpBar.enabled = true;
        hpBar.color = CustomerManager.instance.GetHpColor(0);

        this.enabled = true;
    }

    public void UpdateHp()
    {
        hpBar.color = CustomerManager.instance.GetHpColor(PatienceTimer);
        hpPivot.localScale = new Vector3(1, 1 - PatienceTimer, 1);
    }
}
