using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderFollow : MonoBehaviour
{
    public Animator anim;
    public GameObject target;
    public Transform targetPosition;
    public float speed = 1;

    public bool attackMode = false;
    // Start is called before the first frame update
    void Start()
    {
        target =  GameObject.FindGameObjectWithTag("Vessel");
        targetPosition = target.transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(attackMode)
        {
            attack();
        }
    }

    void attack()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);
        transform.LookAt(targetPosition);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("weenie"))
        {
            attackMode = true;
            anim.Play("DefenderAttack");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.name.Contains("weenie"))
        {
            attackMode = false;
            anim.Play("DefenderIdle");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Collider>().enabled=false;
        attackMode = false;
        anim.Play("DefenderExplode");
        Destroy(gameObject, 2);
    }
}
