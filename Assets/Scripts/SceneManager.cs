using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
