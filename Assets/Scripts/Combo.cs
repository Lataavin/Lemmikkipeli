using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI _text;
    [SerializeField]
    private float _angle = 15f;

    private void Start()
    {
        transform.localEulerAngles = new Vector3(0, 0, _angle * UnityEngine.Random.Range(-1f, 1f));
    }

    public void SetCombo(int number)
    {
        _text.text = string.Format("COMBO {0}", number);
    }


}
