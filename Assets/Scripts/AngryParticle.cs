using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryParticle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particle;
    [SerializeField]
    private float _minAngry = 1f;
    [SerializeField]
    private float _maxAngry = 20f;

    public void SetAngriness(float normalized)
    {
        var emission = _particle.emission;
        emission.rateOverTimeMultiplier = Mathf.Lerp(_minAngry, _maxAngry, normalized);
    }
}
