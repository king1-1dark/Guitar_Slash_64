using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Rules : MonoBehaviour
{
    public GameObject rules;

    public void Game()
    {
        SceneManager.LoadScene("Game");
    }

}
