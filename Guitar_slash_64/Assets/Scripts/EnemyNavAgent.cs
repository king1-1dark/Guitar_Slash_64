using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyNavAgent : MonoBehaviour
{
    public GameObject EnemyTarget;
    public GameObject target;
    NavMeshAgent agent;
    public float fov = 120f;
    public float ViewDistance = 20f;
    public float Speed = 1f;
    public int health = 100;
    private Animator anim;
    private bool isAware = false;
    private Collider[] ragdollColliders;
    private Rigidbody[] ragdollRigidbodies;
    private bool isDetecting = false;
    public float loseThreshold = 5f;
    private float LoseTimer = 0;
    public GameObject fist;
    private AudioSource feet;

    void Start()
    {
        feet = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Collider col in ragdollColliders)
        {
            if (!col.CompareTag("Enemy"))
            {
                col.enabled = false;
            }

        }
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
    }


    void Update()
    {
        if (health <= 0)
        {
            Die();
            return;
        }

        if (isAware)
        {


            agent.SetDestination(target.transform.position);
            agent.speed = 4;
            anim.SetBool("Aware", true);
            anim.SetFloat("Speed", Speed, 0.0f, Time.deltaTime);
            if (!isDetecting)
            {
                LoseTimer += Time.deltaTime;
                if (LoseTimer >= loseThreshold)
                {
                    isAware = false;
                    LoseTimer = 0;
                }
            }
            if (agent.remainingDistance < 1.8f)
            {
                anim.SetBool("Aware", false);
                Attack();
            }
        }
        else
        {
            anim.SetBool("Aware", false);

        }
        SearchForPlayer();
    }
    public void SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(target.transform.position)) < fov / 2f)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < ViewDistance)
            {
                OnAware();
                if (!feet.isPlaying)
                    feet.Play();
            }
            else
                isDetecting = false;
        }
        else
            isDetecting = false;
    }

    public void Attack()
    {
        anim.SetTrigger("Attacking");
        feet.Stop();
    }
    public void Die()
    {
        feet.Stop();
        EnemyTarget.SetActive(false);
        agent.speed = 0;
        anim.enabled = false;

        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true;
            fist.GetComponent<Collider>().enabled = false;
        }
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }
    }
    public void StartFist()
    {
        fist.GetComponent<Collider>().enabled = true;
    }
    public void EndFist()
    {
        fist.GetComponent<Collider>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            feet.Stop();
            anim.SetTrigger("Hit");
            health -= 25;   
        }
    }
    //public void OnHit(int damage)
    //{
    //    feet.Stop();
    //    anim.SetTrigger("Hit");
    //    health -= damage;
    //}
    public void OnAware()
    {
        isAware = true;
        isDetecting = true;
        LoseTimer = 0;
    }

}
