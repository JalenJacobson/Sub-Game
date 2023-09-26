using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 aim;
    public float bulletSpeed = 10000f;

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
}
