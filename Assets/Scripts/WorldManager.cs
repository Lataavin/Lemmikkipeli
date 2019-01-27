using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;
    [SerializeField]
    private Combo _comboPrefab;
    [SerializeField]
    private Transform _comboPosition;
    [SerializeField]
    private FeverMode _fever;

    public RectTransform stars;
    private float starsMax;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //   DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
        starsMax = stars.sizeDelta.y;
    }
    public float worldSize;

    public UnityEvent OnLose;

    private int reputationScore = 10;
    public int ReputationScore { get { return reputationScore; } set { reputationScore = value; UpdateStars(); if (reputationScore <= 0) { OnLose.Invoke(); } else if (reputationScore > 10) { reputationScore = 10; } } }
    public int ExtraScore = 0;
    public int GameScore = 0;

    public void ShowCombo()
    {
        if (_comboPrefab == null)
        {
            return;
        }
        var combo = Instantiate(_comboPrefab);
        combo.transform.SetParent(_comboPosition);
        combo.transform.localPosition = Vector3.zero;
        combo.SetCombo(ExtraScore);
    }

    public void SetFever()
    {
        if (_fever == null)
        {
            return;
        }
        _fever.SetValue(Mathf.Clamp01(ExtraScore / 5f));
        AudioManager.instance.FeverAmount = Mathf.Clamp01(Mathf.Clamp(ExtraScore - 10, 0, 100) / 5.0f) * 0.2f;
    }

    public void RipCamera()
    {
        PlayerPrefs.SetInt("Score", GameScore);
        SceneManager.LoadScene("EndScene");
    }


    public void UpdateStars()
    {
        float temp = (starsMax / 10) * ReputationScore;
        stars.sizeDelta = new Vector2(stars.sizeDelta.x, temp);
        stars.anchoredPosition = new Vector3(stars.anchoredPosition.x, -temp / 2);
    }
}
