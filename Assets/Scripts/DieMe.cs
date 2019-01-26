using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DieMe : MonoBehaviour
{
    private float phase = 0f;
    public float duration = 1f;

    public bool positionEnabled = false;
    public AnimationCurve pX;
    public AnimationCurve pY;
    public AnimationCurve pZ;

    public bool rotationEnabled = false;
    public AnimationCurve rX;
    public AnimationCurve rY;
    public AnimationCurve rZ;

    public bool scaleEnabled = false;
    public AnimationCurve sX;
    public AnimationCurve sY;
    public AnimationCurve sZ;


    public Transform main;

    public UnityEvent OnLaunch;
    public UnityEvent OnDestroy;

    void Start()
    {
        this.enabled = false;
    }

    public void Launch()
    {
        this.enabled = true;
        OnLaunch.Invoke();
    }

    void Update()
    {
        phase += Time.deltaTime / duration;
        if (rotationEnabled)
        {
            transform.localEulerAngles = new Vector3(rX.Evaluate(phase), rY.Evaluate(phase), rZ.Evaluate(phase));
        }
        if (scaleEnabled)
        {
            transform.localScale = new Vector3(sX.Evaluate(phase), sY.Evaluate(phase), sZ.Evaluate(phase));
        }
        if (positionEnabled)
        {
            transform.localPosition = new Vector3(pX.Evaluate(phase), pY.Evaluate(phase), pZ.Evaluate(phase));
        }

        if (phase >= 1)
        {
            Destroy(main.gameObject);
            OnDestroy.Invoke();
        }
    }
}
