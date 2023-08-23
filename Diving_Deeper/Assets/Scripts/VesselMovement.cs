using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VesselMovement : MonoBehaviour
{

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Keyboard.current[Key.Space].isPressed)
        {
            print("space");
            rb.AddForce(transform.forward * 600);
        }

        if (Keyboard.current[Key.UpArrow].isPressed)
        {
            print("holding up");
            
            transform.Rotate(1, 0, 0, Space.World);
        }
        if (Keyboard.current[Key.DownArrow].isPressed)
        {
            print("holding down");
            transform.Rotate(-1, 0, 0, Space.World);
        }
        if (Keyboard.current[Key.LeftArrow].isPressed)
        {
            print("holding left");
            transform.Rotate(0, -1, 0, Space.World);
        }
        if (Keyboard.current[Key.RightArrow].isPressed)
        {
            print("holding right");
            transform.Rotate(0, 1, 0, Space.World);
        }
        else transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, .01f);  
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
