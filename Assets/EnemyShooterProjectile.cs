using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterProjectile : MonoBehaviour
{
    public GameObject target;
    public float speed = 2000;
    public Rigidbody rb;
    public float timeCount;
    public bool lockOn = true;
    public bool dead = false;

    void Start()
    {
        target =  GameObject.FindGameObjectWithTag("Vessel");
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 3);
        speed = 600;
        lockOn = true;
    }

    void FixedUpdate()
    {
        if(dead) return;
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;
        if(lockOn)
        {
            transform.LookAt(target.GetComponent<Transform>());
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        if(other.name.Contains("weenie"))
        {
            lockOn = false;
        }
    }
}
