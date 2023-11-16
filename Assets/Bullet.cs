using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 aim;
    public float bulletSpeed = 100f;
    public Animator anim;
    public float lifeTime;
    public float Timer;

    void Awake()
    {
        // Destroy(gameObject, .25f);
    }

    void Start()
    {
        bulletSpeed = 1000f;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(aim != null)
        {
            gameObject.GetComponent<Rigidbody>().velocity = aim * bulletSpeed;
        }

        if(Timer >= lifeTime)
        {
            Destroy(gameObject);
        }
        
        
    }

     void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        anim.Play("BulletExplode");
        Destroy(gameObject);
    }
}
