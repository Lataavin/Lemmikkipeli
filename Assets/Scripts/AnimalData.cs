using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalData : ScriptableObject
{
    [SerializeField]
    private List<Animal.PatternDefinition> _patterns = new List<Animal.PatternDefinition>();
    [SerializeField]
    private Material _defaultPattern;
    [SerializeField]
    private List<Animal.SpriteDefinition> _sprites = new List<Animal.SpriteDefinition>();
    [SerializeField]
    private Sprite _defaultSprite;

    public Material GetPattern(string name)
    {
        for (var i = 0; i < _patterns.Count; ++i)
        {
            if (_patterns[i].Name == name)
            {
                return _patterns[i].Material;
            }
        }
        return _defaultPattern;
    }

    public Sprite GetSprite(string name)
    {
        for (var i = 0; i < _sprites.Count; ++i)
        {
            if (_sprites[i].Name == name)
            {
                return _sprites[i].Sprite;
            }
        }
        return _defaultSprite;
    }
}
