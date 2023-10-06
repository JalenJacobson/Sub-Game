using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderFollow : MonoBehaviour
{
    public GameObject target;
    public Transform targetPosition;
    public float speed = 25;

    public bool attackMode = false;
    // Start is called before the first frame update
    void Start()
    {
        target =  GameObject.FindGameObjectWithTag("Vessel");
        targetPosition = target.transform;
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
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.name.Contains("weenie"))
        {
            attackMode = false;
        }
    }
}
