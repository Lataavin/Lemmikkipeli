using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    void Start()
    {
        transform.localPosition = new Vector3(Random.Range(0, WorldManager.instance.worldSize), transform.localPosition.y, transform.localPosition.z);
        Destroy(GetComponent<Prop>());
    }
}
