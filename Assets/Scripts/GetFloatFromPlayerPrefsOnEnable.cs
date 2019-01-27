using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFloatFromPlayerPrefsOnEnable : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI _text;
    [SerializeField]
    private string _id;

    public void OnEnable()
    {
        _text.text = PlayerPrefs.GetFloat(_id, 0).ToString();
    }
}
