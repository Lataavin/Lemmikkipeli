using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    private Camera _camera;
    private Vector2 _touchOrigin;
    public float TurningMultiplier = 0.1f;
    private Vector2 _mousePreviousPosition;

    public void Awake()
    {
        _camera = Camera.main;
    }

    public void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        var velocity = 0.0f;
        
        if (Input.touches.Length > 0)
        {
            var touchDelta = Input.touches[0].deltaPosition;
            velocity = touchDelta.x * -TurningMultiplier;
        }
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            _mousePreviousPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            velocity = (_mousePreviousPosition.x -Input.mousePosition.x ) * TurningMultiplier;
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
}