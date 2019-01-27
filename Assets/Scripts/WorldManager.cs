﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;
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
    }
    public float worldSize;

    public UnityEvent OnLose;

    private int reputationScore = 10;
    public int ReputationScore { get { return reputationScore; } set { reputationScore = value; if (reputationScore <= 0) { OnLose.Invoke(); } } }
    public int ExtraScore = 0;
    public int GameScore = 0;
    public void RipCamera()
    {
        PlayerPrefs.SetInt("Score", GameScore);
        SceneManager.LoadScene("EndScene");
    }
}
