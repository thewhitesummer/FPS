﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void ReloadScene() {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1;
    }

    public void ReloadGame() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
