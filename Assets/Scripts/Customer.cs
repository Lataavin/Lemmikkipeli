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

    public Transform distform;

    public GameObject HeartEffectPrfab;
    public GameObject AngryEffectPrfab;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private AngryParticle _angryParticle;
    [SerializeField]
    private AnimationCurve _angryCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    private int _anmAngryness = Animator.StringToHash("Angryness");

    public void OnInstantiate()
    {
        transform.localPosition = new Vector3(WorldManager.instance.worldSize * 1.2f, 0, 0);
        CustomerManager.instance.Customers.Add(this);
        CheckWant();
        isInstantiated = true;
        wantRendGameObject.SetActive(false);
        CustomerManager.instance.CustomerData.SetCustomerVisuals(myRend);
        _angryParticle.gameObject.SetActive(false);
        WorldManager.instance.SetFever();
    }

    void Start()
    {
        if (!isInstantiated)
        {
            OnInstantiate();
        }
    }

    private bool firstCustomer = false;
    void Update()
    {
        if (firstCustomer)
        {
            PatienceTimer += Time.deltaTime / patience;
            var visualPatience = _angryCurve.Evaluate(PatienceTimer);
            _animator.SetFloat(_anmAngryness, visualPatience);
            _angryParticle.SetAngriness(visualPatience);

        }
        if (aPhase < 1)
        {
            aPhase += Time.deltaTime;
            if (aPhase >= 1) { aPhase = 1; }
            transform.localPosition = new Vector3(Mathf.Lerp(aAngle, bAngle, aPhase), 0, 0);
            if (aPhase >= 1)
            {
                aPhase += 98;
            }
        }
    }

    public void CheckWant()
    {
        if (Want == null)
        {
            Want = CreatureManager.instance.GetRandomCreature();
        }
    }
    public string CheckWantError(bool updateWant)
    {
        if (Want == null)
        {
            Want = CreatureManager.instance.GetRandomCreature();
            if(updateWant) { UpdateWant(); }
            return "No want found";
        }
        return "Want found";
    }

    public void Leave(bool happy)
    {
        CustomerManager.instance.NextCustomer(this);
        if (happy)
        {
            if (HeartEffectPrfab != null)
            {
                Instantiate(HeartEffectPrfab, distform.position, HeartEffectPrfab.transform.localRotation);                    
                AudioManager.instance.PlayAnimalGiveSound(true);

            }
            WorldManager.instance.ReputationScore += 1;
            WorldManager.instance.ExtraScore += 1;
            WorldManager.instance.ShowCombo();
            WorldManager.instance.SetFever();
            WorldManager.instance.GameScore += 1 + WorldManager.instance.ExtraScore;
        }
        else
        {
            if (AngryEffectPrfab != null)
            {
                Instantiate(AngryEffectPrfab, distform.position, AngryEffectPrfab.transform.localRotation);
            }
            WorldManager.instance.ReputationScore -= 1;
            WorldManager.instance.ExtraScore = 0;
            WorldManager.instance.SetFever();
        }
        wantRendGameObject.SetActive(false);
        hpPivot.gameObject.SetActive(false);
        GetComponentInChildren<DieMe>().Launch();
        this.enabled = false;
    }

    private float aAngle = 0;
    private float bAngle = 0;
    private float aPhase = 99;
    public void MoveStep(int nr, float angle)
    {
        myRend.sortingOrder = -3000 - nr;
        aAngle = transform.localPosition.x;
        bAngle = angle;
        aPhase = 0;
    }

    public void SetFirst()
    {
        CheckWant();
        patience = (patience / 2) + (patience / WorldManager.instance.ReputationScore);
        UpdateWant();
        hpBar.enabled = true;
        hpBar.color = CustomerManager.instance.GetHpColor(0);
        _angryParticle.SetAngriness(0);
        _angryParticle.gameObject.SetActive(true);
        firstCustomer = true;
    }

    public void UpdateHp()
    {
        hpBar.color = CustomerManager.instance.GetHpColor(PatienceTimer);
        hpPivot.localScale = new Vector3(1, 1 - PatienceTimer, 1);
    }

    public void UpdateWant()
    {
        wantRend.sprite = Want.Rend.sprite;
        wantRend.material = Want.Rend.material;
        wantRendGameObject.SetActive(true);
    }
}
