using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : Bullet
{
    public bool released = false;
    public float releasePowerActual;
    public bool doneMoving = false;
    public Vector3 backward;

    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        if(!released)
        {
        transform.position = followPoint.transform.TransformPoint(0,0,0);
        }
        if(released)
        {
            if(gameObject.GetComponent<Rigidbody>().velocity.magnitude <= 5f)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                // doneMoving = true;
                
            }
        }
        
    }

    void FixedUpdate()
    {
        if(gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero) return;
        gameObject.GetComponent<Rigidbody>().AddForce(backward * 9.8f);
    }

    public override void release(float releasePower, Vector3 aimNew, Vector3 backwardNew)
    {
        releasePowerActual = (releasePower) * 8000;
        aim = aimNew;
        backward = backwardNew;
        released = true;
        gameObject.GetComponent<Rigidbody>().AddForce(aim * releasePowerActual) ;
        print("release" + releasePower);
    }
}
