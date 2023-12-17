using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : Bullet
{
    public bool released = false;
    public float releasePowerActual;
    public bool doneMoving = false;
    public Vector3 backward;
    public Rigidbody rb;
    public float reverseForce;
    public GameObject RedLight;
    public GameObject GreenLight;
    public GameObject armoredEnemy;
    

    // Start is called before the first frame update
    public override void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Nest.")
        {
            GreenLight.SetActive(false);
            //RedLight.SetActive(true);
            armoredEnemy = other.transform.parent.gameObject;
            anim.Play("BlowUp");
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        if(!released)
        {
            transform.position = followPoint.transform.TransformPoint(0,0,0);
            // transform.rotation = followPoint.transform.rotation;
        }
        if(released)
        {
            if(rb.velocity.magnitude <= 5f)
            {
                rb.velocity = Vector3.zero;
                // doneMoving = true;
                
            }
        }
        
    }

    void FixedUpdate()
    {
        if(rb.velocity == Vector3.zero) return;
        rb.AddForce(backward * 20);
    }

    public override void release(float releasePower, Vector3 aimNew, Vector3 backwardNew)
    {
        releasePowerActual = (releasePower + 1) * 5000;
        aim = aimNew;
        backward = backwardNew;
        released = true;
        rb.AddForce(aim * releasePowerActual) ;
        print("release" + releasePower);
    }

    public override void detonate()
    {
        Destroy(armoredEnemy);
        Destroy(gameObject);
        //play grenade explostion animation here
        //send message to armoredEnemy game object to play animation 
        //eg. Both sludge and nest should have a function with the same name that tells them to destroy and play death animation. We call that here. 
    }

}
