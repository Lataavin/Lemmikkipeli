using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverMode : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    public void SetValue(float value)
    {
        var color = _image.color;
        color.a = value;
        _image.color = color;
    }
}
