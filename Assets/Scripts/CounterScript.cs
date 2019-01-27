using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterScript : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        var size = WorldManager.instance.worldSize;
        if (_renderer.sprite == null)
        {
            return;
        }
        var spriteSize = _renderer.sprite.bounds.size.x;
        var scale = transform.localScale;
        scale.x = size / spriteSize + 2;
        transform.localScale = scale;
        var pos = transform.localPosition;
        pos.x = -spriteSize;
        transform.localPosition = pos;
    }
}
