using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DefenderFollow : MonoBehaviour
{
    public Animator anim;
    public GameObject target;
    public GameObject vessel;
    public Transform targetPosition;
    public float speed = 1;

    public bool attackMode = false;
    public bool holdBeforeAttack = false;
    public Vector3 currentRelativePosition;
    public GameObject cube;
    public Transform cubePosition;
    public GameObject followPoint;
    public bool firstAttack = true;

    public bool startCircling = false;
    public float angle = 0;
    public bool clockwise;
    public float direction = -1;
    public float radiusOffset = 350;
    public float circleSpeed = 3;
    public float attackWaitTime = 3;
    public int attackPointNumber;
    public AudioSource audioSource;
    public AudioClip defender_ScreechAudio;
    public AudioSource audiosource_flap;
    public AudioClip flapper;

    void Start()
    {
        pickRandoms();
        target =  GameObject.FindGameObjectWithTag("AttackPointFront");
        vessel =  GameObject.FindGameObjectWithTag("Vessel");
        cube =  GameObject.FindGameObjectWithTag("cube");
        targetPosition = target.transform;
        cubePosition = cube.transform;
        anim = GetComponent<Animator>();
        audioSource.clip = defender_ScreechAudio;
        audioSource.Play();
        audiosource_flap.clip = flapper;
        audiosource_flap.Play();
    }

    public void pickRandoms()
    {
        radiusOffset = Random.Range(4, 30);
        circleSpeed = Random.Range(1, 4); 
        clockwise = Random.value > 0.5;
        attackWaitTime = Random.Range(3, 12);
        if(clockwise)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }      
    }

    
    void FixedUpdate()
    {
        if(holdBeforeAttack)
        {
            followPoint.transform.position = target.transform.TransformPoint(currentRelativePosition);
        }
        if(startCircling)
        {
            transform.position = Vector3.Lerp(transform.position, followPoint.transform.position, .06f);
            angle += Time.deltaTime * direction * circleSpeed;
            float x = Mathf.Cos(angle) * radiusOffset;
            float y = Mathf.Sin(angle) * radiusOffset;
            followPoint.transform.position = new Vector3(target.transform.position.x + x, target.transform.position.y + y, target.transform.position.z);
        }
        if(attackMode)
        {
            attack();
        }   
    }

    void attack()
    {
        transform.position = Vector3.MoveTowards(transform.position, vessel.transform.position, 1);
        transform.LookAt(vessel.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        speed = 50;
        if(other.name.Contains("weenie") && firstAttack)
        {
            // anim.Play("DefenderAttack");
            StartCoroutine("initiateAttack");
            firstAttack = false;
            // transform.position = Vector3.MoveTowards(transform.position, cubePosition.position, speed * Time.deltaTime);
            transform.LookAt(cubePosition); 
        }
    }
    // void OnTriggerExit(Collider other)
    // {
    //     if(other.name.Contains("weenie"))
    //     {
    //         // attackMode = false;
    //         // anim.Play("DefenderIdle");
    //         // firstAttack = true;
    //     }
    // }

    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.collider.name.Contains("Bullet"))
    //     {
    //         explodeSequence();
    //     }
    //     else if (collision.collider.name.Contains("weenie"))
    //     {
    //         print("weenie collisition" + gameObject.name);
    //         StopAllCoroutines();
    //         gameObject.GetComponent<Collider>().enabled=false;
    //         attackMode = false;
    //         anim.Play("DefenderExplode");
    //         collision.collider.SendMessage("takeDamage", 2);
    //         Destroy(gameObject, 2);
            
    //     }
    // }

    public void explodeSequence()
    {
        StopAllCoroutines();
        gameObject.GetComponent<Collider>().enabled=false;
        attackMode = false;
        anim.Play("DefenderExplode");
        Destroy(gameObject, 2);
    }

    public IEnumerator initiateAttack()
    {
        currentRelativePosition = target.transform.InverseTransformPoint(transform.position);
        holdBeforeAttack = true;
        yield return new WaitForSeconds(.5f);
        holdBeforeAttack = false;
        startCircling = true;
        yield return new WaitForSeconds(attackWaitTime);
        startCircling = false;
        attackMode = true;
    }
}
