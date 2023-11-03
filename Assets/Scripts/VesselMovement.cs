using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class VesselMovement : MonoBehaviour
{

    public Rigidbody rb;
    //public int currentRiddleTrigger = 0;
    //public List<GameObject> riddleTriggers;
    public bool playerControlling;
    public float timer = 0;
    public bool vesselDead = false;
   // public GameObject RiddlePannel;
    public GameObject MenuButton;
    public GameObject ResumeButton;
    public GameObject startGamePannel;
    public Vector3 currentCheckPoint;
    public int speed = 600;

    public CinemachineVirtualCamera virtualCamera;

    public bool drivingMS = false;
    public bool hasJoint = false;
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        vesselDead = true;
        rb = GetComponent<Rigidbody>();
        //RiddlePannel = GameObject.Find("RiddlePannel");
        MenuButton = GameObject.Find("RiddleButton");
        ResumeButton = GameObject.Find("Close");
        startGamePannel = GameObject.Find("StartPannel");
        // for(var i = 1; i<= riddleTriggers.Count - 1; i++)
        // {
        //     riddleTriggers[i].SetActive(false);
        // }
        currentCheckPoint = new Vector3(0, 0, 324);
    }

    void Update()
    {
        timer = timer = timer + Time.deltaTime;

        if(Input.GetKeyDown("z"))
        {
            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, -15, 4.20999f);
        }
        else if(Input.GetKeyDown("x"))
        {
            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 4.20999f, 15);
        }
        else if(Input.GetKeyDown("c"))
        {
            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 4.20999f, -15);
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(drivingMS) return;
        if (collision.gameObject.name != "RiddleTrigger" || collision.gameObject.name != "TriggerCube" || !collision.gameObject.name.Contains("Defender") )
        {
            //this if statement makes a joint between the sludge and the vessel. Could figure out a way to have the sludge drag you in.
            // if(collision.gameObject.GetComponent<Rigidbody>() != null && collision.gameObject.name.Contains("Sludge"))
            // {
            //     gameObject.AddComponent<FixedJoint> ();  
		    //     gameObject.GetComponent<FixedJoint>().connectedBody = collision.gameObject.GetComponent<Rigidbody>();
		    //     hasJoint = true;
            // }
            
            StartCoroutine("loseCondition");
        }
    }

    public IEnumerator loseCondition()
    {
        yield return new WaitForSeconds(.1f);
        vesselDead = true;
        //RiddlePannel.SetActive(true);
        // rb.useGravity = true;
        MenuButton.SetActive(false);
        ResumeButton.SetActive(false);

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // if(vesselDead) return;
        // playerControlling = false;
        if (Keyboard.current[Key.Space].isPressed)
        {
            
            rb.AddForce(transform.forward * speed);
        }

        if (Keyboard.current[Key.UpArrow].isPressed || Input.GetKey("s"))
        {
           
            
            transform.Rotate(1, 0, 0, Space.Self);
            playerControlling = true;
            timer = 0f;
        }
        if (Keyboard.current[Key.DownArrow].isPressed || Input.GetKey("w"))
        {
            
            transform.Rotate(-1, 0, 0, Space.Self);
            playerControlling = true;
            timer = 0f;
        }
        if (Keyboard.current[Key.LeftArrow].isPressed  || Input.GetKey("a"))
        {
            
            transform.Rotate(0, -1, 0, Space.Self);
            playerControlling = true;
            timer = 0f;
        }
        if (Keyboard.current[Key.RightArrow].isPressed || Input.GetKey("d"))
        {
            
            transform.Rotate(0, 1, 0, Space.Self);
            playerControlling = true;
            timer = 0f;
        }
        //if(!playerControlling && Keyboard.current[Key.F].isPressed)
        //{
            // StartCoroutine(lookAtNextTrigger());
            // timer = timer = timer + Time.deltaTime;
            // if (timer > 8f)
            // {   
               // lookAtNextTrigger();
            // }
            
       // }

        // print(riddleTriggers[currentRiddleTrigger]);
        // else Quaternion.Slerp(transform.rotation, transform.LookAt(riddleTriggers[currentRiddleTrigger]), .01f);  
        // else transform.LookAt(riddleTriggers[currentRiddleTrigger]); 
    }

    // public void lookAtNextTrigger()
    // {
    //     Vector3 direction = riddleTriggers[currentRiddleTrigger].GetComponent<Transform>().position - transform.position;
    //     Quaternion toRotation = Quaternion.LookRotation(direction);
    //     transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 3f * Time.deltaTime);
    // }

    // public void nextTrigger()
    // {
    //     currentCheckPoint = riddleTriggers[currentRiddleTrigger].GetComponent<Transform>().position;
    //     currentRiddleTrigger++;
    //     riddleTriggers[currentRiddleTrigger].SetActive(true);
    // }

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
        health -= damageTaken;
        print(health);
    }

    public void startGame()
    {
        startGamePannel.SetActive(false);
        vesselDead = false;
        MenuButton.SetActive(true);
        // rb.AddForce(transform.forward * 20000);
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

    // IEnumerator WaitAndP()
    // {
    //     // suspend execution for 5 seconds
    //     yield return new WaitForSeconds(5);
    //     print("WaitAndPrint " + Time.time);
    // }

    // public void OnRotateUp()
    // {
    //     print("hello");
    //     // print(context);
    // }
    // public void OnRotateDown()
    // {
    //     print("Down");
    // }
    // public void OnRotateLeft()
    // {
    //     print("left");
    // }
    // public void OnRotateRight()
    // {
    //     print("right");
    // }
    
}
