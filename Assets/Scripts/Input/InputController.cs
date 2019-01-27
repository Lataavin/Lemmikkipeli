using System.Collections.Generic;
using UnityEngine;

public class TouchD
{
    public int fingerId = -2;
    public Vector2 startPoint = Vector2.zero;
    public Vector2 prevPoint = Vector2.zero;
    public Vector2 curPoint = Vector2.zero;
    public float duration = 0;
    public Creature creature = null;
}

public class InputController : MonoBehaviour
{
    private Camera _camera;
    private Vector2 _touchOrigin;
    public float TurningMultiplier = 0.1f;
    public float grabMoveMultiplier = 0.5f;
    public float extraVelocityReduction = 1f;
    public float extraVelocityForce = 1f;
    private Vector2 _mousePreviousPosition;
    public LayerMask rayMask;

    private List<TouchD> touches = new List<TouchD>();

    public static InputController instance;
    public GameObject TouchEffectPrfab;

    public void Awake()
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
        _camera = Camera.main;
    }

    public void Update()
    {
        UpdateInput();
    }

    private float extraVelocity = 0;
    private float extraVelocityDir = 1;
    private float velocity = 0.0f;
    private void UpdateInput()
    {
        velocity = 0f;

        #if UNITY_ANDROID || UNITY_IPHONE || UNITY_IOS

             /*   if (Input.touches.Length > 0)
                {
                    var touchDelta = Input.touches[0].deltaPosition;
                    velocity = touchDelta.x * -TurningMultiplier;
                }*/
                foreach (Touch t in Input.touches)
                {
                    switch (t.phase)
                    {
                        case TouchPhase.Began:
                            if (TouchEffectPrfab != null)
                            {
                                Instantiate(TouchEffectPrfab, Camera.main.ScreenToWorldPoint(new Vector3(t.position.x, t.position.y, 10)), TouchEffectPrfab.transform.localRotation);
                            }
                            TouchD newTouch = new TouchD();
                            newTouch.fingerId = t.fingerId;
                            newTouch.startPoint = t.position;
                            newTouch.prevPoint = t.position;
                            newTouch.curPoint = t.position;
                            newTouch.duration = 0;
                            newTouch.creature = TryRay(t.position);
                            if (newTouch.creature == null) { extraVelocity = 0; }
                            else
                            {
                                newTouch.creature.Grab(newTouch);
                            }
                            touches.Add(newTouch);
                            break;

                        case TouchPhase.Stationary:
                        case TouchPhase.Moved:
                            for (int i = 0; i < touches.Count; i++)
                            {
                                if (touches[i].fingerId == t.fingerId)
                                {
                                    touches[i].prevPoint = touches[i].curPoint;
                                    touches[i].curPoint = t.position;
                                    touches[i].duration += Time.deltaTime;
                                    if (touches[i].creature == null)
                                    {
                                        velocity = (touches[i].prevPoint.x - touches[i].curPoint.x) * TurningMultiplier;
                                        TurnCamera(velocity);
                                    }
                                    else
                                    {
                        velocity = GrabMove(touches[i]);
                                        touches[i].creature.Move(touches[i]);
                                    }
                                    break;
                                }
                            }
                            break;

                        case TouchPhase.Canceled:
                        case TouchPhase.Ended:
                            for (int i = touches.Count - 1; i >= 0; i--)
                            {
                                if (touches[i].fingerId == t.fingerId)
                                {
                                    touches[i].prevPoint = touches[i].curPoint;
                                    touches[i].curPoint = t.position;
                                    touches[i].duration += Time.deltaTime;
                                    EndTouch(touches[0]);
                                    touches.RemoveAt(i);
                                    break;
                                }
                            }
                            break;
                    }
                }
        #else

        if (Input.GetMouseButtonDown(0))
        {
            TouchD newTouch = new TouchD();
            newTouch.fingerId = -1;
            newTouch.startPoint = Input.mousePosition;
            newTouch.prevPoint = Input.mousePosition;
            newTouch.curPoint = Input.mousePosition;
            newTouch.duration = 0;
            newTouch.creature = TryRay(Input.mousePosition);
            if (newTouch.creature == null) { extraVelocity = 0; }
            else
            {
                newTouch.creature.Grab(newTouch);
            }
            touches.Add(newTouch);
        }
        if (Input.GetMouseButton(0) && touches.Count > 0)
        {
            touches[0].prevPoint = touches[0].curPoint;
            touches[0].curPoint = Input.mousePosition;
            touches[0].duration += Time.deltaTime;
            if (touches[0].creature == null)
            {
                velocity = (touches[0].prevPoint.x - touches[0].curPoint.x) * TurningMultiplier;
            }
            else
            {
                velocity = GrabMove(touches[0]);
                touches[0].creature.Move(touches[0]);
            }
        }
        if (Input.GetMouseButtonUp(0) && touches.Count > 0)
        {
            touches[0].prevPoint = touches[0].curPoint;
            touches[0].curPoint = Input.mousePosition;
            touches[0].duration += Time.deltaTime;

            EndTouch(touches[0]);

            touches.RemoveAt(0);
        }
        #endif
        TurnCamera(velocity);


    }

    public void CheckSwipeControl()
    {
    }

    public void TurnCamera(float velocity)
    {
        if (extraVelocity > 0)
        {
            extraVelocity -= Time.deltaTime * extraVelocityReduction;
            if (extraVelocity <= 0) { extraVelocity = 0; }
            velocity += (extraVelocity * extraVelocityForce * extraVelocityDir * Time.deltaTime);
        }
        float temp = _camera.transform.localPosition.x + velocity;
        temp = Mathf.Clamp(temp, 0, WorldManager.instance.worldSize);
        _camera.transform.localPosition = new Vector3(temp, _camera.transform.localPosition.y, _camera.transform.localPosition.z);

    }

    public Creature TryRay(Vector2 point)
    {
        Ray ray = Camera.main.ScreenPointToRay(point);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1000, rayMask);
        if (hit.collider != null)
        {
            try
            {
                return hit.collider.GetComponent<CreatureLink>().parent;
            }
            catch
            {
                return null;
            }
        }
        return null;
    }

    public float GrabMove(TouchD t)
    {
        /*  float half = Screen.width / 2f;
          float temp = (touches[0].curPoint.x - touches[0].startPoint.x);
          if(temp > half) { temp = half; }
          temp /= half;*/
        float half = Screen.width / 2f;

        float temp = (touches[0].curPoint.x - touches[0].startPoint.x);

        if (temp < 0) { temp /= touches[0].startPoint.x; }
        else if (temp > 0) { temp /= (Screen.width - touches[0].startPoint.x); }
        else { temp = 0; }
        
        return temp * grabMoveMultiplier;
    }

    public void EndTouch(TouchD t)
    {
        if (t.creature == null)
        {
            extraVelocity = ((t.startPoint.x - t.curPoint.x) / Screen.width) / t.duration;
            extraVelocityDir = extraVelocity / Mathf.Abs(extraVelocity);
            extraVelocity = Mathf.Abs(extraVelocity);
        }
        else
        {
            t.creature.Drop(t);
        }
    }
    public void AbortTouch(TouchD t)
    {
        touches.Remove(t);
    }
}
