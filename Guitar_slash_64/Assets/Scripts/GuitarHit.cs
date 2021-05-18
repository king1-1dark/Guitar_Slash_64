using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarHit : MonoBehaviour
{
    public int attackDamage = 30;
    public void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<EnemyNavAgent>().OnHit(attackDamage);
    }
}
