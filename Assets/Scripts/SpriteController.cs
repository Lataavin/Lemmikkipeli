using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class SpriteController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;

    public void SetSprite(Sprite sprite)
    {
        _renderer.sprite = sprite;
    }

    public void SetColor(Color color)
    {
        _renderer.color = color;
    }

    public void SetMaterial(Material material)
    {
        _renderer.material = material;
    }
}
