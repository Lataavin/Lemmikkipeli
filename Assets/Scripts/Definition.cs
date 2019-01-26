using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Definition<T> where T : UnityEngine.Object
{
    public T Value;
    public int Weight;

    public static T GetDefinition<T1>(List<T1> list) where T1 : Definition<T>
    {
        int total = 0;
        int current = 0;
        for (var i = 0; i < list.Count; ++i)
        {
            total += list[i].Weight;
        }
        var random = UnityEngine.Random.Range(0, total);
        for (var i = 0; i < list.Count; ++i)
        {
            current += list[i].Weight;
            if (random < current)
            {
                return list[i].Value;
            }
        }
        Debug.Log("null");
        return null;
    }
}

[Serializable]
public class PatternDefinition : Definition<Material> { }

[Serializable]
public class SpriteDefinition : Definition<AnimatorOverrideController> { }
