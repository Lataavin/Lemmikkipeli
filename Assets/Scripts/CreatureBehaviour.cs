using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBehaviour : MonoBehaviour
{
    public float movementSpeed = 10f;
    [SerializeField]
    private SpriteRenderer _renderer;
    private float mSpeed = 0;
    public float MSpeed { get { return mSpeed; } set { mSpeed = 360 / value; } }
    private float rotation = 0;

    void Start()
    {
        MSpeed = movementSpeed;


        moveDir = Random.Range(0, 2);
        if (moveDir == 0) { moveDir = -1; }
        _renderer.flipX = moveDir >= 0;
        rotation = Random.Range(0, WorldManager.instance.worldSize);
       // transform.localPosition = new Vector3(rotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
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
        if (rotation < 0 || rotation > WorldManager.instance.worldSize)
        {
            moveDir *= -1;
            _renderer.flipX = moveDir >= 0;
            Mathf.Clamp(rotation, 0, WorldManager.instance.worldSize);
        }
        transform.localPosition = new Vector3(rotation, transform.localPosition.y, transform.localPosition.z);
    }
}
