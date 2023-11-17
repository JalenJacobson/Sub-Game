using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float timer = 0;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;
    public GameObject target;
    public Animator anim;
    public bool timerStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        target =  GameObject.FindGameObjectWithTag("Vessel");
        anim = GetComponent<Animator>();
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

    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("weenie"))
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
}
