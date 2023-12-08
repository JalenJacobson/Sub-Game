using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public Rigidbody rb;
    public bool directionOne;
    public float timer;
    public float moveTime;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > moveTime)
        {
            
            directionOne = !directionOne;
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        handleMove();
    }

    public void handleMove()
    {
        if(directionOne)
        {
            rb.velocity = new Vector3(0, 5, 0);
        }
        if(!directionOne)
        {
            rb.velocity = new Vector3(0, -5, 0);
        }
    }
}
