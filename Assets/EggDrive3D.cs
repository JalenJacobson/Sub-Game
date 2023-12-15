using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggDrive3D : MonoBehaviour
{
    public float x;
    public float y;
    public float xUse;
    public float yUse;
    public Rigidbody rb;
    public int speed;
    public int jumpForce;
    public int bounceForce;
    public bool grounded;
    public float jumpBuffer = .1f;
    public int remainingJumps = 2;
    public int jumpClicks = 2;
    public float coyoteTime = .5f;
    public int airTravelSpeed;
    public bool braking = false;
    private Vector3 jumpBufferDirection;

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
        if(jumpBuffer > 0)
        {
            jumpBuffer -= Time.deltaTime;
        }
        
        
    }

    void FixedUpdate()
    {
        if(rb.velocity != Vector3.zero) print("Velocity " + rb.velocity);
        Move();
        governSpeed();
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
            jumpClicks = 2;
            if(jumpBuffer > 0)
            {
                jump(jumpBufferDirection);
            }
            if(braking)
            {
                rb.drag = 10;
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == ("ground"))
        {
            remainingJumps = 2;
            grounded = true;
            coyoteTime = 0.5f;
        }
        
    }
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == ("ground"))
        {
            rb.drag = 1;
            grounded = false;
            StartCoroutine(leaveGround(.2f));
        }
    }

    public IEnumerator leaveGround(float coyoteWaitTime)
    {
        yield return new WaitForSeconds(coyoteWaitTime);
        remainingJumps --;
    }

    public void governSpeed()
    {
        if(grounded) return;
        if(rb.velocity.x > airTravelSpeed)
        {
            rb.velocity = new Vector3(airTravelSpeed, rb.velocity.y, rb.velocity.z);
        }
        if(rb.velocity.x < -airTravelSpeed)
        {
            rb.velocity = new Vector3(-airTravelSpeed, rb.velocity.y, rb.velocity.z);
        }
        if(rb.velocity.z > airTravelSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, airTravelSpeed);
        }
        if(rb.velocity.z < -airTravelSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -airTravelSpeed);
        }
    }

    public void processInputs()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            y = -1;  
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            y=0;
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z/5);  
            // rb.AddForce(new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z/5));  
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            y = 1;  
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {;
            y=0;
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z/5);  
            // rb.AddForce(new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z/5));  
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            x = -1;  
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            x=0;
            rb.velocity = new Vector3(rb.velocity.x/5, rb.velocity.y, rb.velocity.z);  
            // rb.AddForce(new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z/5));  
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            x = 1;  
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            x=0;
            rb.velocity = new Vector3(rb.velocity.x/5, rb.velocity.y, rb.velocity.z);  
            // rb.AddForce(new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z/2));  
        }
        
    }

    public void brake()
    {
        
        if(Input.GetKeyDown("space"))
        {
            braking = true;
            if(grounded) rb.drag = 20;
             
            // if(grounded) rb.velocity = new Vector3(0, rb.velocity.y, 0); 
        }
        if(Input.GetKeyUp("space"))
        {
            braking = false;
            if(grounded) rb.drag = 1;
        }
    }

    public void Move()
    {
        if(braking) return;
        
        rb.AddForce(new Vector3(x * speed, 0, y * speed));
    }

    public void jump(Vector3 direction)
    {
        if(!grounded && remainingJumps <= 0)
        {
            jumpBuffer = .1f;
            jumpBufferDirection = direction;
        }
        else if(grounded)
        {
            jumpBuffer = 0;
        }
        if(remainingJumps == 2)
        {
            rb.AddForce(new Vector3(0, 800, 0));
            rb.AddForce(direction * jumpForce);
            jumpClicks --;
            // StartCoroutine(leaveGround(.001f));      
        }  
        else if(remainingJumps == 1)
        {
            if(jumpClicks == 0) return;
            rb.AddForce(direction * jumpForce);
            rb.AddForce(new Vector3(0, 800, 0));
            remainingJumps --; 
            jumpClicks --;
        }
    }

    public void jumpKill()
    {
        if(rb.velocity.y > 0)
        {
            // rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y/2, rb.velocity.z);
        }
        
    }
    
}
