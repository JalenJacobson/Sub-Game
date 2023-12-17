
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
    public float type;
    public float scaleTimer;
    public Transform followPoint;

    void Awake()
    {
        
    }

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        getBulletProperties();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Timer += Time.deltaTime;
        scaleTimer = Timer * 10;
        if(aim != null)
        {
            gameObject.GetComponent<Rigidbody>().velocity = aim * bulletSpeed;
        }

        if(Timer >= lifeTime)
        {
            Destroy(gameObject);
            
        }

        // if(type == 1)
        // {
        //     gameObject.transform.localScale += new Vector3(scaleTimer, scaleTimer, scaleTimer);
        // }
        
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

    public virtual void detonate()
    {
        // StartCoroutine(detonateSequence());
        print("detonate");
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

    public virtual void release(float release, Vector3 aim, Vector3 backward)
    {
        return;
    }
    
}
