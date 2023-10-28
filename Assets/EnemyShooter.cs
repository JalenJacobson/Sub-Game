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

    // Start is called before the first frame update
    void Start()
    {
        target =  GameObject.FindGameObjectWithTag("Vessel");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 2)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            timer = 0;
        }
    }

    void FixedUpdate()
    {
        Vector3 direction = target.GetComponent<Transform>().position - bulletSpawnPoint.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        bulletSpawnPoint.transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 30f * Time.deltaTime);
    }
}
