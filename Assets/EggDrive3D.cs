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
    public bool airBoostUsed;
    public float jumpBuffer = .1f;
    public int remainingJumps = 2;
    public int jumpClicks = 2;
    public float coyoteTime = .5f;
    public int airTravelSpeed;
    public bool braking = false;
    private Vector3 jumpBufferDirection;
    public bool grappling = false;
    public GameObject jumpFX;
    public GameObject landFX;
    public Vector3 respawnPoint;
    public bool downwardSlide = false;
    public Animator anim;
    public AudioSource audioSource;
    public AudioClip JumpAudio;
    public AudioClip DoubleJumpAudio;
    public AudioClip LandAudio;
    public AudioClip GrappleAudio;
    public AudioClip BoostAudio;
    public AudioClip BounceAudio;
    public AudioClip ExplodeAudio;
    public AudioSource movingAudioSource;
    public AudioClip MoveAudio;
    
    

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        respawnPoint = transform.position;
        anim = GetComponent<Animator>();
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
        Move();
        governSpeed();
        if(downwardSlide)
        {
            rb.AddForce(80, -80, 0);
        }
    }

    private bool recentBounce = false;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("spikes"))
        {
            StartCoroutine("fall");
        }
        if(collision.gameObject.tag == ("bounce"))
        {
            rb.AddForce(Vector3.up * bounceForce);
            recentBounce = true;
            airBoostUsed = false;
            recentGrapple = false;
            remainingJumps = 2;
            jumpClicks = 2;
            audioSource.clip = BounceAudio;
            audioSource.Play();
            
        }
        if(collision.gameObject.tag == ("ground"))
        {  
            airBoostUsed = false;
            recentGrapple = false;
            recentBounce = false;
            if(jumpBuffer > 0)
            {
                remainingJumps = 2;
                jumpClicks = 2;
                groundJump = true;
                // rb.AddForce(new Vector3(0, 1500, 0));
                rb.AddForce(jumpBufferDirection * jumpForce);
                StartCoroutine(jumpingFx());
                jumpClicks --; 
                 
            }
            if(braking)
            {
                rb.drag = 10;
            }
            grounded = true;
            if(remainingJumps <= 1)
            {
                audioSource.clip = LandAudio;
                audioSource.Play(); 
            }
            
        }
    }

    
    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == ("ground"))
        {
            if(groundJump) return;
            remainingJumps = 2;
            jumpClicks = 2;
            grounded = true;
            coyoteTime = 0.5f;
        }
        
    }
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == ("ground"))
        {
            groundJump = false;
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
        
        if (Input.GetKey("s"))
        {
            y = -1;  
            movingAudioSource.clip = MoveAudio;
            movingAudioSource.Play();  
        }
        if (Input.GetKeyUp("s"))
        {
            y=0;
            movingAudioSource.Stop();
            if(!grappling)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z/2);
            }
        }
        if (Input.GetKey("w"))
        {
            y = 1;
            movingAudioSource.clip = MoveAudio;
            movingAudioSource.Play();  
        }
        if (Input.GetKeyUp("w"))
        {;
            y=0;
            movingAudioSource.Stop();
            if(!grappling)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z/2); 
            }
             
        }
        if (Input.GetKey("a"))
        {
            x = -1;  
            movingAudioSource.clip = MoveAudio;
            movingAudioSource.Play();  
        }
        if (Input.GetKeyUp("a"))
        {
            x=0;
            movingAudioSource.Stop();
            if(!grappling)
            {
                rb.velocity = new Vector3(rb.velocity.x/2, rb.velocity.y, rb.velocity.z); 
            }
              
        }
        if (Input.GetKey("d"))
        {
            x = 1;  
            movingAudioSource.clip = MoveAudio;
            movingAudioSource.Play();  
        }
        if (Input.GetKeyUp("d"))
        {
            x=0;
           movingAudioSource.Stop();
            if(!grappling)
            {
                rb.velocity = new Vector3(rb.velocity.x/2, rb.velocity.y, rb.velocity.z); 
            }
              
        }
        if(Input.GetKeyDown("j") || Input.GetKeyDown("r") || Input.GetKeyDown("q"))
        {
            boost();
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

    public void startGrapple()
    {
        grappling = true;
        airTravelSpeed = 150;
        remainingJumps = 2;
        recentBounce = false;
        
        speed = 60;
        audioSource.clip = GrappleAudio;
        audioSource.Play();
        // rb.drag = 0;
    }
    public void endGrapple()
    {
        grappling = false;
        airTravelSpeed = 35;
        speed = 80;
        recentGrapple = true;
        jumpClicks = 2;
        remainingJumps = 2;
    }

    public void Move()
    {
        if(braking) return;
        
        rb.AddForce(new Vector3(x * speed, 0, y * speed));
    }

    public void boost()
    {
        if(grappling || airBoostUsed) return;
        if(!grounded && jumpClicks <=0)
        {
            airBoostUsed = true;
            StartCoroutine(airBoost(.5f));
        }
        rb.AddForce(x * 2500, 0 , y * 2500);
        audioSource.clip = BoostAudio;
        audioSource.Play();
    }

    public IEnumerator airBoost(float boostTime)
    {
        airTravelSpeed = 150;
        yield return new WaitForSeconds(boostTime);
        airTravelSpeed = 35;
    }

    public bool groundJump = false;
    public bool recentGrapple = false;

    public void jump(Vector3 direction)
    {
        if(!grounded && remainingJumps <= 0)
        {
            jumpBuffer = .25f;
            jumpBufferDirection = direction;
        }
        else if(grounded)
        {
            jumpBuffer = 0;
        }
        if(jumpClicks <= 0 || grappling) return;
        if(remainingJumps == 2)
        {
            if(grounded)
            {
                groundJump = true;
                rb.AddForce(direction * jumpForce);
                // StartCoroutine(airBoost(.25f));
                StartCoroutine(jumpingFx());
                jumpClicks --;
                audioSource.clip = JumpAudio;
                audioSource.Play(); 
            }
            else if(recentGrapple || recentBounce)
            {
                rb.AddForce(direction * jumpForce);
                // StartCoroutine(airBoost(.25f));
                StartCoroutine(jumpingFx());
                jumpClicks --; 
                audioSource.clip = JumpAudio;
                audioSource.Play(); 
            }
                
        }  
        else if(remainingJumps == 1)
        {
            if(jumpClicks <= 0) return;
            rb.AddForce(direction * jumpForce);
            // StartCoroutine(airBoost(.25f));
            StartCoroutine(jumpingFx());
            remainingJumps --; 
            jumpClicks --;
            audioSource.clip = DoubleJumpAudio;
            audioSource.Play(); 
        }
    }

    public void jumpKill()
    {
        if(rb.velocity.y > 0)
        {
            // rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y/2, rb.velocity.z);
        }
        
    }

    IEnumerator jumpingFx()
    {
        jumpFX.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        jumpFX.gameObject.SetActive(false);
    }

    public void deathByFall()
    {
        StartCoroutine("fall");
    }

    public IEnumerator fall()
    {
        anim.Play("MS_Dead");
        audioSource.clip = ExplodeAudio;
        audioSource.Play();
        yield return new WaitForSeconds(1.5f);
        rb.velocity = new Vector3(0,0,0);
        transform.position = respawnPoint;
        anim.Play("Ms_Alive");
    }
    
}
