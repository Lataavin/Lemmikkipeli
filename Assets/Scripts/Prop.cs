using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    void Start()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Random.Range(0, 360), transform.localEulerAngles.z);
        Destroy(GetComponent<Prop>());
    }
}
