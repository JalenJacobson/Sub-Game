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
    public bool holdBeforeAttack = false;
    public Vector3 currentRelativePosition;
    public GameObject cube;
    public Transform cubePosition;
    public GameObject followPoint;
    //public ParentConstraint pc;
    //public ConstraintSource constraintSource;
    public bool firstAttack = true;

    public bool startCircling = false;
    public float angle = 0;
    public bool clockwise;
    public float direction = -1;
    public float radiusOffset = 350;
    public float circleSpeed = 3;
    // Start is called before the first frame update
    void Start()
    {

        target =  GameObject.FindGameObjectWithTag("AttackPointFront");
        cube =  GameObject.FindGameObjectWithTag("cube");
        targetPosition = target.transform;
        cubePosition = cube.transform;
        anim = GetComponent<Animator>();
        //pc = GetComponent<ParentConstraint>();
        //constraintSource.sourceTransform = target.transform;
        //constraintSource.weight = 1;
        //pc.AddSource(constraintSource);
        //pc.translationAtRest = transform.position;
        //pc.SetSource(0,1);
        clockwise = Random.value > 0.5;
        if(clockwise)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        radiusOffset = Random.Range(4, 30);
        circleSpeed = Random.Range(1, 4);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, followPoint.transform.position, .06f);
        if(holdBeforeAttack)
        {
            
            followPoint.transform.position = target.transform.TransformPoint(currentRelativePosition);
        }
        if(startCircling)
        {
            angle += Time.deltaTime * direction * circleSpeed;
            float x = Mathf.Cos(angle) * radiusOffset;
            float y = Mathf.Sin(angle) * radiusOffset;
            followPoint.transform.position = new Vector3(target.transform.position.x + x, target.transform.position.y + y, target.transform.position.z);
            // print(target.transform.position);
        }
    }

    void attack()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);
        transform.LookAt(targetPosition);
    }
    public void getAttack()
    {
        StartCoroutine("initiateAttack");
        firstAttack = false;   
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
        if (collision.collider.name.Contains("Bullet"))
        {
        StopAllCoroutines();
        gameObject.GetComponent<Collider>().enabled=false;
        attackMode = false;
        anim.Play("DefenderExplode");
        Destroy(gameObject, 2);
        }
    }

    public IEnumerator initiateAttack()
    {
        //Jalen left off here
        // anim.Play("DefenderCircle");
        // yield return new WaitForSeconds(1f);
        // anim.Play("DefenderAttack");
        // speed = 50;
        // attackMode = true;
        // //pc.constraintActive = false;

        //justin picked up here
        currentRelativePosition = target.transform.InverseTransformPoint(transform.position);
        holdBeforeAttack = true;

        yield return new WaitForSeconds(3f);
        holdBeforeAttack = false;
        startCircling = true;
    }
}
