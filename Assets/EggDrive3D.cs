using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggDrive3D : MonoBehaviour
{
    public float x;
    public float y;
    public Rigidbody rb;
    public int speed;
    public int jumpForce;
    public int bounceForce;
    public bool grounded;
    public int remainingJumps = 2;
    public float coyoteTime = .5f;
    public bool braking = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        processInputs();
        brake();
        if(!grounded)
        {
            coyoteTime -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("bounce"))
        {
            rb.AddForce(Vector3.up * bounceForce);
            
        }
        if(collision.gameObject.tag == ("ground"))
        {
            remainingJumps = 2;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == ("ground"))
        {
            grounded = true;
            coyoteTime = 0.5f;
            
        }
        
    }
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == ("ground"))
        {
            grounded = false;
            
        }
    }

    public void processInputs()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        
    }

    public void brake()
    {
        if(!grounded) return;
        if(Input.GetKeyDown("space"))
        {
            rb.drag = 5;
            braking = true;
        }
        if(Input.GetKeyUp("space"))
        {
            rb.drag = 1;
            braking = false;
        }
    }

    public void Move()
    {
        if(braking) return;
        if(x > 0) x = 1;
        else if(x < 0 ) x = -1;
        if(y > 0) y = 1;
        else if(y < 0 ) y = -1;
        rb.AddForce(new Vector3(x * speed, 0, y * speed));
    }

    public void jump(Vector3 direction)
    {
        if(grounded)
        {
            
            rb.AddForce(direction * jumpForce);
            remainingJumps --;       
            
        }  
        else if(coyoteTime >=0)
        {
            rb.AddForce(direction * jumpForce);
            remainingJumps --;
        }
        else if(remainingJumps == 1)
        {
            rb.AddForce(direction * jumpForce);
            remainingJumps --; 
        }
    }

    
}
