﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int firstLevelIndex;
    public void PlayGame()
    {
        SceneManager.LoadScene(firstLevelIndex);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}