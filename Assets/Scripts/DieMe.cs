using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DieMe : MonoBehaviour
{
    private float phase = 0f;
    public float duration = 1f;

    public bool positionEnabled = false;
    public bool positionCurrent = false;
    public AnimationCurve pX;
    public AnimationCurve pY;
    public AnimationCurve pZ;

    public bool rotationEnabled = false;
    public bool rotationCurrent = false;
    public AnimationCurve rX;
    public AnimationCurve rY;
    public AnimationCurve rZ;

    public bool scaleEnabled = false;
    public bool scaleCurrent = false;
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
        if (pX.length == 0) { pX.AddKey(0, transform.localPosition.x); }
        if (pY.length == 0) { pY.AddKey(0, transform.localPosition.y); }
        if (pZ.length == 0) { pZ.AddKey(0, transform.localPosition.z); }
        if(positionCurrent)
        {
            pX.MoveKey(0,new Keyframe(0, transform.localPosition.x));
            pY.MoveKey(0, new Keyframe(0, transform.localPosition.y));
            pZ.MoveKey(0, new Keyframe(0, transform.localPosition.z));
        }

        if (sX.length == 0) { sX.AddKey(0, transform.localScale.x); }
        if (sY.length == 0) { sY.AddKey(0, transform.localScale.y); }
        if (sZ.length == 0) { sZ.AddKey(0, transform.localScale.z); }
        if (scaleCurrent)
        {
            sX.MoveKey(0, new Keyframe(0, transform.localScale.x));
            sY.MoveKey(0, new Keyframe(0, transform.localScale.y));
            sZ.MoveKey(0, new Keyframe(0, transform.localScale.z));
        }

        if (rX.length == 0) { rX.AddKey(0, transform.localEulerAngles.x); }
        if (rY.length == 0) { rY.AddKey(0, transform.localEulerAngles.y); }
        if (rZ.length == 0) { rZ.AddKey(0, transform.localEulerAngles.z); }
        if (rotationCurrent)
        {
            rX.MoveKey(0, new Keyframe(0, transform.localEulerAngles.x));
            rY.MoveKey(0, new Keyframe(0, transform.localEulerAngles.y));
            rZ.MoveKey(0, new Keyframe(0, transform.localEulerAngles.z));
        }
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
