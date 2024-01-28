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
    public float speedActual;

    public bool touchToStart = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        pauseTimeActual = moveTime + pauseTime;
        speedActual = speed *Time.deltaTime;
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
        if(!touchToStart || touchToStart && hasBeenTouched)
        {
            timer += Time.deltaTime;
        }
        if(!touchToStart || touchToStart && hasBeenTouched)
        {
            handleMove();
        }
        
    }

    public bool hasBeenTouched = false;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("egg"))
        {
            hasBeenTouched = true;
        }
    }

    void FixedUpdate()
    {
        
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
