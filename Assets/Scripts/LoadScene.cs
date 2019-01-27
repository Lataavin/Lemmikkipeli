using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private string _sceneName;

    public void GoToScene()
    {
        AudioManager.instance.PlayBlobSound();
        SceneManager.LoadScene(_sceneName);
    }

}
