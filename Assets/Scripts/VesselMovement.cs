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
    // public bool DamageOverloadCoroutineStarted = false;
    public GameObject RiddlePannel;
    public GameObject NextLevelPannel;
    public GameObject MenuButton;
    public GameObject ResumeButton;
    public GameObject startGamePannel;
    public Vector3 currentCheckPoint;
    public int speed = 600;
    public float shieldRegenCountUp;
    public bool shieldRegenCountUpRunning;
    public float damageInThisCountdown;
    public GameObject Shield;
    public ShieldFollow Shield_Script;
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
        Shield_Script = Shield.GetComponent<ShieldFollow>();
        // Shield.SetActive(false);
        for(var i = 1; i<= riddleTriggers.Count - 1; i++)
        {
            riddleTriggers[i].SetActive(false);
        }
        currentCheckPoint = gameObject.GetComponent<Transform>().position;
        anim = GetComponent<Animator>();
    }

    private bool firstTimeDeath = true;
    public bool thrusting = false;
    public bool thrusterDisabled = false;

    void Update()
    {
        if(health <= 0)
        {
            if(!firstTimeDeath) return;
            StartCoroutine(loseCondition());
        }
        
        if(Shield_Script.shieldHealth <= 0)
        {
            Shield.SetActive(false);
            
            if(shieldRegenCountUp >= 3)
            {
                Shield.SetActive(true);
                Shield_Script.shieldVisible.enabled = false;
                Shield_Script.shieldHealth = 20;
                shieldRegenCountUp = 0;
            }
        }
        if(shieldRegenCountUpRunning)
        {
            shieldRegenCountUp += Time.deltaTime;
        }
        if (Input.GetKeyUp("space"))
        {
            audioSourceIdle.Stop();
            thrusting = false;
        }

        if(vesselDead) return;
        if (Input.GetKeyDown("space"))
        {
            if(thrusterDisabled) return;
            audioSourceIdle.clip = ThrusterIdle;
            audioSourceIdle.Play();
            thrusting = true;
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
                // print("start double tap");
                // transform.rotation = Quaternion.LookRotation (-transform.rotation);;
                // print("double tap space");
            }
            lastTapTimeSpace = Time.time;
            
        }

        if(canDodge)
        {
            handleDodges();
        }
        
    }

    void handleDodges()
    {
         if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(forceJumpDownCoroutine());
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(forceJumpUpCoroutine());
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(forceJumpRightCoroutine());
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(forceJumpLeftCoroutine());
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
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<Transform>().position = currentCheckPoint;
        Shield_Script.shieldHealth = 20;
        LifeBar.setShield(Shield_Script.shieldHealth);
        shieldRegenCountUpRunning = false;
        Shield.SetActive(true);
        Shield_Script.shieldVisible.enabled = true;
        health = 100;
        LifeBar.setHealth(health);
        yield return new WaitForSeconds(2);
        rb.isKinematic = false;
        vesselDead = false;
        firstTimeDeath = true;
        yield return new WaitForSeconds(2);
        Shield_Script.shieldVisible.enabled = false;
        LifeBar.stopFlashing();
        LifeBar.died();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(lookingAtNextTrigger == true || drivingMS == true)
        {
            lookAtNextTrigger();
        }
        if(vesselDead) return;
        // playerControlling = false;
        if (Keyboard.current[Key.Space].isPressed)
        {
            if(thrusterDisabled) return;
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
            // print("should be bouncing" + normalDirection);
        }

        if (thrusting == true)
        {
            steerThrusting();
        }
        else if (thrusting == false)
        {
            steerIdle();
        }
        if(Input.GetKey("f"))
        {
            if(lockOnEnemy == null)
            {
                lookAtNextTrigger();
            }
            else
            {
                lookAtlockOnEnemy();
            }
            
        }
    }

    public void steerThrusting()
    {
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
    }

    public void steerIdle()
    {
        if ( Input.GetKey("s"))
        {
            transform.Rotate(3, 0, 0, Space.Self);
            playerControlling = true;
            timer = 0f;
        }
        if (Input.GetKey("w"))
        {
            transform.Rotate(-3, 0, 0, Space.Self);
            playerControlling = true;
            timer = 0f;
        }
        if (Input.GetKey("a"))
        {
            transform.Rotate(0, -3, 0, Space.Self);
            playerControlling = true;
            timer = 0f;
        }
        if (Input.GetKey("d"))
        {
            transform.Rotate(0, 3, 0, Space.Self);
            playerControlling = true;
            timer = 0f;
        } 
    }
  
    private bool canDodge = true;
    public IEnumerator forceJumpUpCoroutine()
    {
        canDodge = false;
        anim.Play("BarrelRoll_Up");
        audioSource.clip = ThrusterDown;
        audioSource.Play();
        yield return new WaitForSeconds(.15f);
        forceJumpUp = true;
        yield return new WaitForSeconds(.3f);
        forceJumpUp = false;
        yield return new WaitForSeconds(.55f);
        canDodge = true;
    }
    public IEnumerator forceJumpDownCoroutine()
    {
        canDodge = false;
        anim.Play("BarrelRoll_Down");
        audioSource.clip = ThrusterDown;
        audioSource.Play();
        yield return new WaitForSeconds(.15f);
        forceJumpDown = true;
        yield return new WaitForSeconds(.3f);
        forceJumpDown = false;
        yield return new WaitForSeconds(.55f);
        canDodge = true;
    }
    public IEnumerator forceJumpRightCoroutine()
    {
        canDodge = false;
        anim.Play("BarrelRoll_Right");
        audioSource.clip = ThrusterDown;
        audioSource.Play();
        yield return new WaitForSeconds(.15f);
        forceJumpRight = true;
        yield return new WaitForSeconds(.3f);
        forceJumpRight = false;
        yield return new WaitForSeconds(.55f);
        canDodge = true;
    }
    public IEnumerator forceJumpLeftCoroutine()
    {
        canDodge = false;
        anim.Play("BarrelRoll_Left");
        audioSource.clip = ThrusterDown;
        audioSource.Play();
        yield return new WaitForSeconds(.15f);
        forceJumpLeft = true;
        yield return new WaitForSeconds(.3f);
        forceJumpLeft = false;
        yield return new WaitForSeconds(.55f);
        canDodge = true;
    }

    public void lookAtNextTrigger()
    {
        Vector3 direction = riddleTriggers[currentRiddleTrigger].GetComponent<Transform>().position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 3f * Time.deltaTime);
        
    }

    public Transform lockOnEnemy;
    public void lookAtlockOnEnemy()
    {
        Vector3 direction = lockOnEnemy.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 3f * Time.deltaTime);
        
    }
    public void placeTracker(GameObject Enemy)
    {
        if(Enemy.name.Contains("test"))
        {
            Enemy.SendMessage("lockOn");
        }
        lockOnEnemy = Enemy.GetComponent<Transform>();
        Enemy.SendMessage("lockOn");
    }
    public void removeTracker()
    {
        lockOnEnemy = null;
    }

    public void nextTrigger(bool normalTrigger)
    {
        addHealth();
        currentCheckPoint = riddleTriggers[currentRiddleTrigger].GetComponent<Transform>().position;
        currentRiddleTrigger++;
        riddleTriggers[currentRiddleTrigger].SetActive(true);
        RiddleTriggerFollowPoint_Script.nextTrigger(riddleTriggers[currentRiddleTrigger]);
        if(normalTrigger)
        {
            StartCoroutine("getTriggerLookAtNext");
        }
        
    }

    public IEnumerator getTriggerLookAtNext()
    {
       lookingAtNextTrigger = true;
       vesselDead = true;
       yield return new WaitForSeconds(1.5f);
       lookingAtNextTrigger = false;
       vesselDead = false; 
    }

    public void addHealth()
    {
        var newHealth = health + 15;
        if(newHealth >= 100)
        {
            health = 100;
        }
        else health = newHealth;
        LifeBar.setHealth(health);
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
            health -= damageTaken;
            damageInThisCountdown += damageTaken;
            LifeBar.setHealth(health);
            shieldRegenReset();
    }

    public void shieldRegenReset()
    {
        shieldRegenCountUp = 0;
        shieldRegenCountUpRunning = true;
    
    }

    public IEnumerator CameraShake(int damageTaken)
    {
        float shakeAmplitude = damageTaken * 2.5f;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shakeAmplitude;
        yield return new WaitForSeconds(.5f);
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
    }

    // public IEnumerator DamageOverload()
    // {
    //     DamageOverloadCoroutineStarted = true;
    //     Shield.SetActive(true);
    //     yield return new WaitForSeconds(3f);
    //     Shield.SetActive(false);
    //     DamageOverloadCoroutineStarted = false;
    // }
    
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
        
        vesselDead = false;
        //MenuButton.SetActive(true);  
    }
    public void pauseGame()
    {
        
        vesselDead = true;
        //MenuButton.SetActive(true);  
    }
    public void closeMenustart()
    {
        startGamePannel.SetActive(false);
        RiddlePannel.SetActive(false);
    }

    private Coroutine speedCoroutine;
    public void speedMove()
    {
        if(speedCoroutine != null) StopCoroutine(speedCoroutine);
        speedCoroutine = StartCoroutine("SpeedMoveRoutine");
    }

    public IEnumerator SpeedMoveRoutine()
    {
        speed = 2000;
        yield return new WaitForSeconds(3f);
        speed = 600;
    }
}
