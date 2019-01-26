using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMyself : MonoBehaviour
{
    [SerializeField]
    private float _duration = 1f;
    private float _timer = 0f;

    public void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _duration)
        {
            Destroy(gameObject);
        }
    }
}
