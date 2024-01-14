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
            armoredEnemy = other.transform.parent.gameObject;
            anim.Play("BlowUp");
            other.transform.parent.gameObject.SendMessage("Explode");
        }
        if(other.name.Contains("Sludge"))
        {
            
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        if(!released)
        {
            transform.position = followPoint.transform.TransformPoint(0,0,0);
            transform.rotation = followPoint.transform.rotation;
        }
        if(released)
        {
            grenadeTimer += Time.deltaTime;
            if(grenadeTimer >= 5f)
            {
                StartCoroutine(endEarly());
            }
        }
    }

    public float grenadeTimer;

    IEnumerator endEarly()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(2);
        anim.Play("BlowUp");
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

   

    public override void release(float releaseTime, Vector3 aimNew)
    {
        if(releaseTime < 1)
        {
            Destroy(gameObject);
        }
        else
        {
            releasePowerActual = 8000;
            aim = aimNew;
            released = true;
            rb.AddForce(aim * releasePowerActual) ;
        }
    }

    public override void detonate()
    {
        Destroy(armoredEnemy);
        Destroy(gameObject);
    }

}
