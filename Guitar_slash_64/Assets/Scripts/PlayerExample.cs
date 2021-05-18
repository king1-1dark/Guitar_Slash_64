using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExample : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyNavAgent>().OnAware();  
        }
    }
}
