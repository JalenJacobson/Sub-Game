using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : Bullet
{
    public bool released = false;
    public float releasePowerActual;
    public bool doneMoving = false;
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
            if(gameObject.GetComponent<Rigidbody>().velocity != aim)
            {
                doneMoving = true;
            }
        }
        
    }

    void FixedUpdate()
    {
        if(doneMoving) return;
        gameObject.GetComponent<Rigidbody>().AddForce(-aim * 9.8f);
    }

    public override void release(float releasePower, Vector3 aimNew)
    {
        releasePowerActual = releasePower * 5000;
        aim = aimNew;
        released = true;
        gameObject.GetComponent<Rigidbody>().AddForce(aim * releasePowerActual) ;
        print("release" + releasePower);
    }
}
