﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioClip mainMusic;
    [SerializeField] private AudioClip creditsMusic;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _blobSound;

    [SerializeField] private AudioClip _successSound;

    [SerializeField] private AudioClip _failSound;

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
        var pitchTarget = 1.0f + (Mathf.Sin(Time.timeSinceLevelLoad * 2.0f) * FeverAmount);
        _audioSource.pitch = Mathf.Lerp(_audioSource.pitch, pitchTarget, Time.deltaTime);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= StartSceneMusic;
    }

    public void PlayBlobSound()
    {
        _audioSource.PlayOneShot(_blobSound);
    }

    public void PlayCreatureSound(string animalAnimName)
    {
        var animalSound = CreatureManager.instance.animData.GetAnimalSound(animalAnimName);
        if (animalSound != null)
        {
            _audioSource.PlayOneShot(animalSound);
        }
    }

    public void PlayAnimalGiveSound(bool success)
    {
        if (success)
            _audioSource.PlayOneShot(_successSound);
        else
        {
            _audioSource.PlayOneShot(_failSound);
        }
    }
}