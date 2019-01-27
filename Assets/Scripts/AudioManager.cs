using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioClip mainMusic;
    [SerializeField]
    private AudioClip creditsMusic;
    
    [SerializeField]
    private AudioSource _audioSource;

    public bool FeverSoundsOn;
    public float FeverAmount = 0.0f;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += StartSceneMusic;
    }

    public void StartSceneMusic(Scene scene, LoadSceneMode mode)
    {
        var sceneName = scene.name;

        if (sceneName == "Credits")
        {
            _audioSource.clip = creditsMusic;
            _audioSource.Play();
        }
        else if (sceneName == "StartUpScene" && _audioSource.clip != mainMusic)
        {
            _audioSource.clip = mainMusic;
            _audioSource.Play();
        }
    }

    public void Update()
    {
        UpdateFever();
    }

    public void UpdateFever()
    {
        var pitchTarget = 0.0f;

        if (FeverSoundsOn)
        {
            pitchTarget = 1.0f + (Mathf.Sin(Time.timeSinceLevelLoad * 2.0f) * FeverAmount);
        }
        else
        {
            pitchTarget = 1;
        }

        _audioSource.pitch = Mathf.Lerp(_audioSource.pitch, pitchTarget, Time.deltaTime);

    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= StartSceneMusic;
    }

}