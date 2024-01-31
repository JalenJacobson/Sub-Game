using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Shotgun : MonoBehaviour
{
    public Vector3 aim;
    public float bulletSpeed = 100f;
    public Animator anim;
    public float lifeTime;
    public float Timer;
    public float type;

    void Awake()
    {
        getBulletProperties();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        // getBulletProperties();
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

    public void getBulletProperties()
    {
        getBulletLifetime();
        getBulletSpeed();
    }

    public void getBulletLifetime()
    {
        if(type == 0)
        {
            lifeTime =  3f;
        }
        else if(type == 1)
        {
            lifeTime =  .25f;
        }
        else if(type == 2)
        {
            lifeTime =  99f;
        }
        else lifeTime =  2f;
    }
    
    public void getBulletSpeed()
    {
        if(type == 2)
        {
            bulletSpeed =  0f;
        }
        else bulletSpeed =  1000f;
    }

    public void detonate()
    {
        // StartCoroutine(detonateSequence());
        // print("detonate");
        Destroy(gameObject);  
    }

    // public IEnumerator detonateSequence()
    // {
    //     print("detonate");
    //     yield return new WaitForSeconds(6);
    //     Destroy(gameObject); 
    // }

     void OnTriggerEnter(Collider other)
    {
        if(type == 2)return;
        if(other.name.Contains("Wall"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            if(anim)
            {
                anim.Play("BulletExplode");
            }
            Destroy(gameObject);
        }
        
    }
}
