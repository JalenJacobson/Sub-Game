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
    public Animator anim;
      public AudioSource audioSource;
    public AudioClip Projectile_woosh;

    void Start()
    {
        target =  GameObject.FindGameObjectWithTag("Vessel");
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 8);
        speed = 600;
        lockOn = true;
        anim = GetComponent<Animator>();
        audioSource.clip = Projectile_woosh;
        audioSource.Play();
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
    public void Pop()
    {
        StartCoroutine(PopandDestroy());
    }

    public IEnumerator PopandDestroy()
    {
        anim.Play("Pop");
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
