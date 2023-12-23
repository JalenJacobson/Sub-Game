using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class VesselMovement : MonoBehaviour
{

    public Rigidbody rb;
    public Animator anim;
    public int currentRiddleTrigger = 0;
    public List<GameObject> riddleTriggers;
    public bool playerControlling;
    public float timer = 0;
    public bool vesselDead = false;
    public bool DamageOverloadCoroutineStarted = false;
    public GameObject RiddlePannel;
    public GameObject NextLevelPannel;
    public GameObject MenuButton;
    public GameObject ResumeButton;
    public GameObject startGamePannel;
    public Vector3 currentCheckPoint;
    public int speed = 600;
    public float damageOverloadCountdown;
    public bool damageOverloadCountdownRunning;
    public float damageInThisCountdown;
    public GameObject Shield;
    public bool lookForwardTime = false;
    public bool lookingAtNextTrigger = false;
    public float stopSpinCount;
    

    public CinemachineVirtualCamera virtualCamera;
    public bool canTakeDamage;
    public bool drivingMS = false;
    public bool hasJoint = false;
    public int health = 100;
    public GameObject lookForwardObject;

    public GameObject testCube;

    // double tap variables
    public float tapSpeed = 0.05f;
    public float lastTapTimeDown = 0;
    public float lastTapTimeUp = 0;
    public float lastTapTimeRight = 0;
    public float lastTapTimeLeft = 0;
    public float lastTapTimeSpace = 0;
    public bool pressedFirstTime = false;
    public bool forceJumpUp = false;
    public bool forceJumpDown = false;
    public bool forceJumpRight = false;
    public bool forceJumpLeft = false;
    public LifeBar LifeBar;
    public AudioSource audioSource;
    public AudioSource audioSourceIdle;
    public AudioClip ThrusterStart;
    public AudioClip ThrusterIdle;
    public AudioClip ThrusterDown;
    public RiddleTriggerFollowPoint RiddleTriggerFollowPoint_Script;


    // Start is called before the first frame update
    void Start()
    {
        lastTapTimeDown = 0;
        lastTapTimeUp = 0;
        lastTapTimeRight = 0;
        lastTapTimeLeft = 0;
        vesselDead = true;
        rb = GetComponent<Rigidbody>();
        NextLevelPannel = GameObject.Find("NextLevelPanel");
        NextLevelPannel.SetActive(false);
        RiddlePannel = GameObject.Find("RiddlePannel");
        MenuButton = GameObject.Find("RiddleButton");
        ResumeButton = GameObject.Find("Close");
        startGamePannel = GameObject.Find("StartPannel");
        Shield = GameObject.Find("Shield");
        Shield.SetActive(false);
        for(var i = 1; i<= riddleTriggers.Count - 1; i++)
        {
            riddleTriggers[i].SetActive(false);
        }
        currentCheckPoint = gameObject.GetComponent<Transform>().position;
        anim = GetComponent<Animator>();
    }

    private bool firstTimeDeath = true;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            audioSourceIdle.clip = ThrusterIdle;
            audioSourceIdle.Play();
        }

        if (Input.GetKeyUp("space"))
        {
            audioSourceIdle.Stop();
        }
        timer = timer + Time.deltaTime;
        
        if(Input.GetKeyUp("c"))
        {
            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 4.20999f, -15);
        }
        else if(Input.GetKeyDown("c"))
        {
            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 4.20999f, 15);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if((Time.time - lastTapTimeSpace) <= tapSpeed)
            {
                // StartCoroutine(forceJumpSpaceCoroutine());
                print("start double tap");
                // transform.rotation = Quaternion.LookRotation (-transform.rotation);;
                print("double tap space");
            }
            lastTapTimeSpace = Time.time;
            
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // if((Time.time - lastTapTimeDown) <= tapSpeed)
            // {
                StartCoroutine(forceJumpDownCoroutine());
            // }
            // lastTapTimeDown = Time.time;
            
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // if((Time.time - lastTapTimeUp) <= tapSpeed)
            // {
            //     // print("double tap Up");
            //     // rb.AddForce(Vector3.up * 1000);
                StartCoroutine(forceJumpUpCoroutine());
            // }
            // lastTapTimeUp = Time.time;
            
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // if((Time.time - lastTapTimeRight) <= tapSpeed)
            // {
                StartCoroutine(forceJumpRightCoroutine());
            // }
            // lastTapTimeRight = Time.time;
            
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // if((Time.time - lastTapTimeLeft) <= tapSpeed)
            // {
                StartCoroutine(forceJumpLeftCoroutine());
            // }
            // lastTapTimeLeft = Time.time;
        }

        

        if(health <= 0)
        {
            if(!firstTimeDeath) return;
            StartCoroutine(loseCondition());
        }

        if(damageInThisCountdown >= 20 && !DamageOverloadCoroutineStarted)
        {
            StartCoroutine(DamageOverload());
        }
        
        if(damageOverloadCountdownRunning)
        {
            if(damageOverloadCountdown > 0)
            {
                damageOverloadCountdown -= Time.deltaTime;
            }
            else if(damageOverloadCountdown <= 0)
            {
                damageOverloadCountdownRunning = false;
                damageInThisCountdown = 0;
            }
        }

        if(Input.GetKeyDown("b"))
        {
            stopSpinCount ++;
        }
        if(stopSpinCount >= 10)
        {
            StartCoroutine(stopSpin());
            stopSpinCount = 0;
        }
        
        
    }

    private Vector3 normalDirection;
    private bool collisionWallBounce = false;

    void OnCollisionEnter(Collision collision)
    {
        if(drivingMS) return;
        if (collision.gameObject.name != "RiddleTrigger" || collision.gameObject.name != "TriggerCube" || !collision.gameObject.name.Contains("Defender") )
        {
            health -= 5;
            var contact = collision.contacts[0];
            normalDirection = contact.normal;
            StartCoroutine(stopSpin());
        }
    }

    public IEnumerator loseCondition()
    {
        firstTimeDeath = false;
        yield return new WaitForSeconds(.1f);
        vesselDead = true;
        Shield.SetActive(false);
        anim.Play("DeadWeenie");
        rb.isKinematic = true;
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Transform>().position = currentCheckPoint;
        Shield.SetActive(true);
        health = 100;
        LifeBar.setHealth(health);
        yield return new WaitForSeconds(2);
        rb.isKinematic = false;
        vesselDead = false;
        firstTimeDeath = true;
        yield return new WaitForSeconds(4);
        Shield.SetActive(false);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(vesselDead) return;
        // playerControlling = false;
        if (Keyboard.current[Key.Space].isPressed)
        {
            
            rb.AddForce(transform.forward * speed);
        }

        
        

        if(forceJumpUp)
        {
            rb.AddRelativeForce (0, 2000, 0);
        }
        if(forceJumpDown)
        {
            rb.AddRelativeForce (0, -2000, 0);
        }
        if(forceJumpLeft)
        {
            rb.AddRelativeForce (-3000, 0, 0);
        }
        if(forceJumpRight)
        {
            rb.AddRelativeForce (3000, 0, 0);
        }

        if(collisionWallBounce)
        {
            rb.AddForce(normalDirection * 5000);
            print("should be bouncing" + normalDirection);
        }

        if ( Input.GetKey("s"))
        {
            transform.Rotate(1, 0, 0, Space.Self);
            playerControlling = true;
            timer = 0f;
        }
        if (Input.GetKey("w"))
        {
            
            transform.Rotate(-1, 0, 0, Space.Self);
            playerControlling = true;
            timer = 0f;
        }
        if (Input.GetKey("a"))
        {
            
            transform.Rotate(0, -1, 0, Space.Self);
            playerControlling = true;
            timer = 0f;
        }
        if (Input.GetKey("d"))
        {
            
            transform.Rotate(0, 1, 0, Space.Self);
            playerControlling = true;
            timer = 0f;
        }

        // if(lookForwardTime)
        // {
        //     lookForward();
        // }

        if(Input.GetKey("f"))
        {
            lookAtNextTrigger();
        }
        if(lookingAtNextTrigger == true)
        {
            lookAtNextTrigger();
        }

        
    }
  

    public IEnumerator forceJumpUpCoroutine()
    {
        anim.Play("BarrelRoll_Up");
                    audioSource.clip = ThrusterDown;
            audioSource.Play();
        yield return new WaitForSeconds(.15f);
        forceJumpUp = true;
        yield return new WaitForSeconds(.3f);
        forceJumpUp = false;
    }
    public IEnumerator forceJumpDownCoroutine()
    {
        anim.Play("BarrelRoll_Down");
        audioSource.clip = ThrusterDown;
        audioSource.Play();
        yield return new WaitForSeconds(.15f);
        forceJumpDown = true;
        yield return new WaitForSeconds(.3f);
        forceJumpDown = false;
    }
    public IEnumerator forceJumpRightCoroutine()
    {
        anim.Play("BarrelRoll_Right");
        audioSource.clip = ThrusterDown;
        audioSource.Play();
        yield return new WaitForSeconds(.15f);
        forceJumpRight = true;
        yield return new WaitForSeconds(.3f);
        forceJumpRight = false;
    }
    public IEnumerator forceJumpLeftCoroutine()
    {
        anim.Play("BarrelRoll_Left");
        audioSource.clip = ThrusterDown;
        audioSource.Play();
        yield return new WaitForSeconds(.15f);
        forceJumpLeft = true;
        yield return new WaitForSeconds(.3f);
        forceJumpLeft = false;
    }

    public void lookAtNextTrigger()
    {
        Vector3 direction = riddleTriggers[currentRiddleTrigger].GetComponent<Transform>().position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 3f * Time.deltaTime);
        
    }

    public void nextTrigger()
    {
        currentCheckPoint = riddleTriggers[currentRiddleTrigger].GetComponent<Transform>().position;
        currentRiddleTrigger++;
        riddleTriggers[currentRiddleTrigger].SetActive(true);
        RiddleTriggerFollowPoint_Script.nextTrigger(riddleTriggers[currentRiddleTrigger]);
        addHealth();
    }

    public void addHealth()
    {
        var newHealth = health + 15;
        if(newHealth >= 100)
        {
            health = 100;
        }
        else health = newHealth;
    }

    // public void revertToCheckPoint()
    // {
    //     StartCoroutine("revertCoroutine");
    // }

    public IEnumerator revertCoroutine()
    {
        transform.position = currentCheckPoint;
        //Vector3 direction = riddleTriggers[currentRiddleTrigger].GetComponent<Transform>().position - transform.position;
        //Quaternion toRotation = Quaternion.LookRotation(direction);
        //transform.rotation = toRotation;
        rb.isKinematic = true;
        //RiddlePannel.SetActive(false);
        MenuButton.SetActive(true);
        ResumeButton.SetActive(true);
        yield return new WaitForSeconds(2);
        rb.isKinematic = false;
        vesselDead = false;
        // yield return new WaitForSeconds(4);
        
        
        
        // transform.rotation
    }

    public void takeDamage(int damageTaken)
    {
            StartCoroutine(CameraShake(damageTaken)); 
            if(!damageOverloadCountdownRunning)
            {
                damageOverloadCountdownRunning = true;
                damageOverloadCountdown = 2f;
            }
            
            health -= damageTaken;
            damageInThisCountdown += damageTaken;
            LifeBar.setHealth(health);
    }

    public IEnumerator CameraShake(int damageTaken)
    {
        float shakeAmplitude = damageTaken * 2.5f;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shakeAmplitude;
        yield return new WaitForSeconds(.5f);
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
    }

    public IEnumerator DamageOverload()
    {
        DamageOverloadCoroutineStarted = true;
        Shield.SetActive(true);
        yield return new WaitForSeconds(3f);
        Shield.SetActive(false);
        DamageOverloadCoroutineStarted = false;
    }
    
    public void lookForward()
    {
        Vector3 direction = lookForwardObject.GetComponent<Transform>().position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 3f * Time.deltaTime);
    }

    public IEnumerator stopSpin()
    {
        // yield return new WaitForSeconds(.25f);
        
        collisionWallBounce = true;
        yield return new WaitForSeconds(.25f);
        collisionWallBounce = false;
        rb.isKinematic = true;
        yield return new WaitForSeconds(.05f);
        rb.isKinematic = false;
    }

    public void startGame()
    {
        startGamePannel.SetActive(false);
        
        vesselDead = false;
        //MenuButton.SetActive(true);
        RiddlePannel.SetActive(false);
    }

    public void speedMove()
    {
        StartCoroutine("SpeedMoveRoutine");
    }

    public IEnumerator SpeedMoveRoutine()
    {
        speed = 2000;
        yield return new WaitForSeconds(1f);
        speed = 600;
    }
}
