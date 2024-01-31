using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{

    public float angle = 0;
    public bool clockwise;
    public float direction = -1;
    public float radiusOffset = 350;
    public float circleSpeed = 3;
    public float attackWaitTime = 3;
    public float startAnimTime = 3;
    public int followHeight;
    public GameObject followPoint;
    public GameObject target;
    public GameObject egg;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        pickRandoms();
        StartCoroutine(startAnim());
        egg = GameObject.FindGameObjectWithTag("egg");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, followPoint.transform.position, .01f);
        transform.LookAt(followPoint.transform.position);
        angle += Time.deltaTime * direction * circleSpeed;
        float x = Mathf.Cos(angle) * radiusOffset;
        float y = Mathf.Sin(angle) * radiusOffset;
        if(!followingEgg)
        {
            followPoint.transform.position = new Vector3(target.transform.position.x + x, target.transform.position.y + followHeight, target.transform.position.z + y);
        }
        else if(followingEgg)
        {
            followPoint.transform.position = new Vector3(egg.transform.position.x + x, egg.transform.position.y + followHeight, egg.transform.position.z + y);
        }
    }

    public void pickRandoms()
    {
        radiusOffset = Random.Range(4, 9);
        circleSpeed = Random.Range(1, 4); 
        clockwise = Random.value > 0.5;
        attackWaitTime = Random.Range(3, 12);
        followHeight = Random.Range(3, 8);
        startAnimTime = Random.Range(1, 3);
        if(clockwise)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }      
    }

    public void swarmEgg()
    {
        StartCoroutine(followEgg());
        // print("followegg");
    }

    private bool followingEgg = false;

    public IEnumerator followEgg()
    {
        followingEgg = true;
        yield return new WaitForSeconds(3);
        followingEgg = false;
    }
    public IEnumerator startAnim()
    {
        yield return new WaitForSeconds(startAnimTime);
        anim.Play("Butterflies");
    }
}
