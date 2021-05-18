using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsEvents : MonoBehaviour
{
    public CharacterController controller;

    void Start()
    {
        controller = this.GetComponent<CharacterController>();

    }
    public void Hit()
    {

    }
    public void FootR()
    {

    }
    public void FootL()
    {

    }
    public void StartRoll()
    {
        controller.center = new Vector3(0, 0.25f, 0);
        controller.height = 0.5f;
    }
    public void EndRoll()
    {
        controller.center = new Vector3(0, 0.92f, 0);
        controller.height = 1.8f;
    }
}
