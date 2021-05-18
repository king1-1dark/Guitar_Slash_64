﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitMenu()
    {

        SceneManager.LoadScene("Menu");
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
