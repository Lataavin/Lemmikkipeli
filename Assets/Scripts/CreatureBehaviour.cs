using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBehaviour : MonoBehaviour
{
    public float movementSpeed = 10f;
    private float mSpeed = 0;
    public float MSpeed { get { return mSpeed; } set { mSpeed = 360 / value; } }
    private float rotation = 0;

    void Start()
    {
        MSpeed = movementSpeed;


        moveDir = Random.Range(0, 2);
        if (moveDir == 0) { moveDir = -1; }
        rotation = Random.Range(0, 360);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotation, transform.localEulerAngles.z);
    }


    private int moveDir = 1;
    void Update()
    {
        if (moveDir != 0 && MSpeed != 0)
        {
            Move(Time.deltaTime * moveDir * MSpeed);
        }
    }

    public void Move(float amount)
    {
        rotation += amount;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotation, transform.localEulerAngles.z);
    }
}
