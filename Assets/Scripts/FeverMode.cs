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
        _image.CrossFadeAlpha(value, 0.5f, true);
    }
}
