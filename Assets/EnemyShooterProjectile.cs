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

    // Start is called before the first frame update
    void Start()
    {
        target =  GameObject.FindGameObjectWithTag("Vessel");
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 3);
        speed = 600;
        lockOn = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(dead) return;
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;

        // Vector3 direction = target.GetComponent<Transform>().position - transform.position;
        // Quaternion toRotation = Quaternion.LookRotation(direction);
        // transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, .25f);
        // timeCount = timeCount + Time.deltaTime;
        // transform.rotation = toRotation;
        if(lockOn)
        {
            transform.LookAt(target.GetComponent<Transform>());
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("weenie"))
        {
            print("entered");
            lockOn = false;
        }
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.collider.name.Contains("Bullet"))
    //     {
    //         dead = true;
    //     // StopAllCoroutines();
    //         gameObject.GetComponent<Collider>().enabled=false;
    //     // attackMode = false;
    //     // anim.Play("DefenderExplode");
    //         Destroy(gameObject);
    //     }

    //     else if (collision.collider.name.Contains("weenie"))
    //     {
    //         // StopAllCoroutines();
    //         // gameObject.GetComponent<Collider>().enabled=false;
    //         // attackMode = false;
    //         // anim.Play("DefenderExplode");
    //         collision.collider.SendMessage("takeDamage", 2);
    //         Destroy(gameObject);
            
    //     }
    // }
}
