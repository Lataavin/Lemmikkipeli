using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal
{
    [Serializable]
    public class PatternDefinition
    {
        public string Name;
        public Material Material;
    }

    [Serializable]
    public class SpriteDefinition
    {
        public string Name;
        public Sprite Sprite;
    }

    public PatternDefinition Pattern;
    public SpriteDefinition Sprite;

    public void SetVisuals(SpriteRenderer renderer)
    {
        renderer.sprite = Sprite.Sprite;
        renderer.material = Pattern.Material;
    }
}
