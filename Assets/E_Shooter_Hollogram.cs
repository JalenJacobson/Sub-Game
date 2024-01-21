using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Shooter_Hollogram : MonoBehaviour
{
    public float timer = 0;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;
    public GameObject target;
    public Animator anim;
    public bool timerStarted = false;
    public VesselMovement vesselMove_Script;
    public bool lockedOn = false;
    // public AudioSource audioSource;
  //  public AudioClip Shootersong;

    // Start is called before the first frame update
    void Start()
    {
        target =  GameObject.FindGameObjectWithTag("Vessel");
        anim = GetComponent<Animator>();
        vesselMove_Script = GameObject.FindGameObjectWithTag("Vessel").GetComponent<VesselMovement>();
     //    audioSource.clip = Shootersong;
     //   audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(timerStarted == true)
        {
        timer += Time.deltaTime;
        }

        if(timer >= 1.25f)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            timer = 0;
        }
    }

    void FixedUpdate()
    {
        // Vector3 direction = target.GetComponent<Transform>().position - bulletSpawnPoint.transform.position;
        // Quaternion toRotation = Quaternion.LookRotation(direction);
        // bulletSpawnPoint.transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 30f * Time.deltaTime);
        bulletSpawnPoint.transform.LookAt(target.GetComponent<Transform>());
    }

    void OnTriggerStay(Collider other)
    {
        if(other.name.Contains("weenie") && lockedOn == true)
        {
        timerStarted = true;
        anim.Play("Shoot");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.name.Contains("weenie"))
        {
        timerStarted = false;
        anim.Play("ShooterIdle");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name.Contains("Bullet"))
        {
            Destroy(gameObject);
        }
    }

    public void StopShooting()
    {
        timerStarted = false;
    }

    public void die()
    {
        vesselMove_Script.removeTracker();
        Destroy(gameObject);
    }
    public void lockOn()
    {
        lockedOn = true;
    }
}
