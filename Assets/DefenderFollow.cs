using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DefenderFollow : MonoBehaviour
{
    public Animator anim;
    public GameObject target;
    public Transform targetPosition;
    public float speed = 1;

    public bool attackMode = false;

    //public ParentConstraint pc;
    //public ConstraintSource constraintSource;
    public bool firstAttack = true;
    // Start is called before the first frame update
    void Start()
    {

        target =  GameObject.FindGameObjectWithTag("Vessel");
        targetPosition = target.transform;
        anim = GetComponent<Animator>();
        //pc = GetComponent<ParentConstraint>();
        //constraintSource.sourceTransform = target.transform;
        //constraintSource.weight = 1;
        //pc.AddSource(constraintSource);
        //pc.translationAtRest = transform.position;
        //pc.SetSource(0,1);
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
        if(other.name.Contains("weenie") && firstAttack)
        {
            StartCoroutine("initiateAttack");
            firstAttack = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.name.Contains("weenie"))
        {
            attackMode = false;
            anim.Play("DefenderIdle");
            firstAttack = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Collider>().enabled=false;
        attackMode = false;
        anim.Play("DefenderExplode");
        Destroy(gameObject, 2);
    }

    public IEnumerator initiateAttack()
    {
        speed = 10;
        attackMode = true;
        anim.Play("DefenderAttack");
        yield return new WaitForSeconds(1f);
        attackMode = false;
        //pc.constraintActive = true;
        anim.Play("DefenderCircle");
        yield return new WaitForSeconds(5f);
        speed = 50;
        attackMode = true;
        //pc.constraintActive = false;

    }
}
