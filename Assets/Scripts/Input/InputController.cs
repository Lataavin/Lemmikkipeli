using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
    private Vector2 _mousePreviousPosition;
    public LayerMask rayMask;

    private List<TouchD> touches = new List<TouchD>();

    public void Awake()
    {
        _camera = Camera.main;
    }

    public void Update()
    {
        UpdateInput();
    }

    float velocity = 0.0f;
    private void UpdateInput()
    {
        velocity = 0f;

#if UNITY_ANDROID || UNITY_IPHONE || UNITY_IOS

        if (Input.touches.Length > 0)
        {
            var touchDelta = Input.touches[0].deltaPosition;
            velocity = touchDelta.x * -TurningMultiplier;
        }
#else

        if (Input.GetMouseButtonDown(0))
        {
            _mousePreviousPosition = Input.mousePosition;
            TouchD newTouch = new TouchD();
            newTouch.fingerId = -1;
            newTouch.startPoint = Input.mousePosition;
            newTouch.prevPoint = Input.mousePosition;
            newTouch.curPoint = Input.mousePosition;
            newTouch.duration = 0;
            newTouch.creature = TryRay(Input.mousePosition);
            touches.Add(newTouch);
        }
        if (Input.GetMouseButton(0))
        {
            touches[0].prevPoint = Input.mousePosition;
            touches[0].duration += Time.deltaTime;
            if (touches[0].creature == null)
            {
                velocity = (touches[0].prevPoint.x - Input.mousePosition.x) * TurningMultiplier;
                _mousePreviousPosition = Input.mousePosition;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _mousePreviousPosition = Input.mousePosition;
        }
#endif

        TurnCamera(velocity);
    }

    public void CheckSwipeControl()
    {
    }

    public void TurnCamera(float velocity)
    {
        _camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x + velocity, _camera.transform.localPosition.y, _camera.transform.localPosition.z);
    }

    public Creature TryRay(Vector2 point)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(point), Vector2.zero);

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
}