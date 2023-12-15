using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public Rigidbody rb;
    public bool directionOne;
    public int pauseTime;
    private float pauseTimeActual;
    private bool pause;
    private float timer;
    public float moveTime;

    public bool xMove;
    public bool yMove;
    public bool zMove;

    public int speed;
    private float speedActual;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        pauseTimeActual = moveTime + pauseTime;
        speedActual = speed  * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(timer > moveTime && timer < pauseTimeActual)
        {
            
            // directionOne = !directionOne;
            pause = true;
            // timer = 0;
        }
        else if(timer > moveTime && timer > pauseTimeActual)
        {
            directionOne = !directionOne;
            pause = false;
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
        if(pause) return;
        if(xMove)
        {
            if(directionOne)
            {
                transform.Translate(speedActual,0,0);
            }
            if(!directionOne)
            {
                transform.Translate(-speedActual,0,0);
            }
        }
        if(yMove)
        {
            if(directionOne)
            {
                transform.Translate(0,speedActual,0);
            }
            if(!directionOne)
            {
                transform.Translate(0,-speedActual,0);
            }
        }
        if(zMove)
        {
            if(directionOne)
            {
                transform.Translate(0,0,speedActual);
            }
            if(!directionOne)
            {
                transform.Translate(0,0,-speedActual);
            }
        }
        
    }
}
