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
    private Vector2 hiddenForce = Vector2.zero;
    public float hiddenForceReduction = 1f;
    public float hiddenForceForce = 1f;

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
        if (hiddenForce == Vector2.zero)
        {
            if (moveDir != 0 && MSpeed != 0)
            {
                Move(Time.deltaTime * moveDir * MSpeed);
            }
        }
        else
        {
            Vector2 temp = new Vector2(hiddenForce.x, hiddenForce.y);
            if (hiddenForce.x != 0)
            {
                hiddenForce = new Vector2(hiddenForce.x - (Time.deltaTime * hiddenForceReduction), hiddenForce.y - (Time.deltaTime * hiddenForceReduction));
            }
            else
            {
                hiddenForce = new Vector2(hiddenForce.x, hiddenForce.y - (Time.deltaTime * hiddenForceReduction));
            }
            if ((temp.x > 0 && hiddenForce.x <= 0) || (temp.x < 0 && hiddenForce.x >= 0))
            {
                hiddenForce.x = 0;
                rotation = transform.localPosition.x;
            }
            transform.localPosition = new Vector3(transform.localPosition.x + (hiddenForce.x * hiddenForceForce * Time.deltaTime), transform.localPosition.y + (hiddenForce.y * hiddenForceForce * Time.deltaTime), transform.localPosition.z);
            if (transform.localPosition.y <= 0) { hiddenForce = Vector2.zero; GetComponent<Creature>().Rend.sortingOrder = 651; rotation = transform.localPosition.x; }
            else if (transform.localPosition.x <= 0) { hiddenForce.x = 0; rotation = transform.localPosition.x; }
            else if (transform.localPosition.x >= WorldManager.instance.worldSize) { hiddenForce.x = 0; rotation = transform.localPosition.x; }
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

    public void AddHiddenForce(TouchD t)
    {
        hiddenForce = new Vector2(-((t.startPoint.x - t.curPoint.x) / Screen.width) / t.duration, -((t.startPoint.y - t.curPoint.y) / Screen.width) / t.duration);
        if(hiddenForce.y == 0) { hiddenForce.y = -1; }
    }
}
