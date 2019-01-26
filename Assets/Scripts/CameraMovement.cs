using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rotateSpeed = 1f;
    private float rotation = 0;


    void Start()
    {
        transform.position = new Vector3(WorldManager.instance.worldSize / 2, (GetComponent<Camera>().orthographicSize*0.95f), 8);
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Rotate(-Time.deltaTime * (360 / rotateSpeed));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Rotate(Time.deltaTime * (360 / rotateSpeed));
        }
    }


    public void Rotate(float amount)
    {
        rotation += amount;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotation, transform.localEulerAngles.z);
    }
}
