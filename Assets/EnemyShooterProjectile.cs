using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterProjectile : MonoBehaviour
{
    public GameObject target;
    public GameObject offsetTarget;
    public float speed = 300;
    public float speed2 = 0;
    public Rigidbody rb;
    public float timeCount;
    public bool lockOn = true;
    public bool dead = false;
    public Animator anim;
      public AudioSource audioSource;
    public AudioClip Projectile_woosh;
    public bool offset = true;

    void Start()
    {
        target =  GameObject.FindGameObjectWithTag("Vessel");
        offsetTarget = GameObject.FindGameObjectWithTag("offsetTarget");
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 8);
        speed = 800;
        lockOn = true;
        anim = GetComponent<Animator>();
        audioSource.clip = Projectile_woosh;
        audioSource.Play();
        StartCoroutine(lockOnWait());
    }

    void FixedUpdate()
    {
        if(dead) return;
        if(lockOn && offset == true)
        {
            gameObject.GetComponent<Rigidbody>().velocity = transform.up * speed;
        }
        else if(lockOn && offset == false)
        {
            gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;
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
    public IEnumerator lockOnWait()
    {
        yield return new WaitForSeconds(.5f);
        offset = false;
        speed = 1200;
    }
    public void Pop()
    {
        StartCoroutine(PopandDestroy());
    }

    public IEnumerator PopandDestroy()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed2;
        anim.Play("Pop");
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
