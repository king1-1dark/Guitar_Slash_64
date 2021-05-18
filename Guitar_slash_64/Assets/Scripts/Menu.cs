using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public Scrollbar slider;
    public GameObject configurate;
    void Update()
    {
        AudioListener.volume = slider.value;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Rule");
    }
    public void ConfigurateGame()
    {
        configurate.SetActive(!configurate.activeSelf);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
