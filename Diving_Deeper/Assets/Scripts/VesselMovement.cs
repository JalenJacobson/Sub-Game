using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VesselMovement : MonoBehaviour
{

    public Rigidbody rb;
    public int currentRiddleTrigger = 0;
    public List<Transform> riddleTriggers;
    public bool playerControlling;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        playerControlling = false;
        if (Keyboard.current[Key.Space].isPressed)
        {
            print("space");
            rb.AddForce(transform.forward * 600);
        }

        if (Keyboard.current[Key.UpArrow].isPressed)
        {
            print("holding up");
            
            transform.Rotate(1, 0, 0, Space.World);
            playerControlling = true;
        }
        if (Keyboard.current[Key.DownArrow].isPressed)
        {
            print("holding down");
            transform.Rotate(-1, 0, 0, Space.World);
            playerControlling = true;
        }
        if (Keyboard.current[Key.LeftArrow].isPressed)
        {
            print("holding left");
            transform.Rotate(0, -1, 0, Space.World);
            playerControlling = true;
        }
        if (Keyboard.current[Key.RightArrow].isPressed)
        {
            print("holding right");
            transform.Rotate(0, 1, 0, Space.World);
            playerControlling = true;
        }
        if(!playerControlling)
        {
            lookAtNextTrigger();
        }

        // print(riddleTriggers[currentRiddleTrigger]);
        // else Quaternion.Slerp(transform.rotation, transform.LookAt(riddleTriggers[currentRiddleTrigger]), .01f);  
        // else transform.LookAt(riddleTriggers[currentRiddleTrigger]); 
    }

    public void lookAtNextTrigger()
    {
        Vector3 direction = riddleTriggers[currentRiddleTrigger].position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 1f * Time.deltaTime);
    }

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
