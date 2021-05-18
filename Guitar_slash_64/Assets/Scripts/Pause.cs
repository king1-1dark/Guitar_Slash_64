using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    public GameObject panel;
    public void ContinueGame()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState =  CursorLockMode.Locked;
        panel.SetActive(false);
    }
    public void Configure()
    {

    }
    public void ExitMenu()
    {

        SceneManager.LoadScene("Menu");
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
