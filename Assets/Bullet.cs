using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 aim;
    public float bulletSpeed = 100f;

    void Awake()
    {
        Destroy(gameObject, 2);
    }

    void Start()
    {
        bulletSpeed = 1000f;
    }

    // Update is called once per frame
    void Update()
    {
        if(aim != null)
        {
            gameObject.GetComponent<Rigidbody>().velocity = aim * bulletSpeed;
        }
        
    }

     void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
