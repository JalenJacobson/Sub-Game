using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VesselMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRotateUp(InputValue value)
    {
        
        print("Up" + value);
    }
    public void OnRotateDown()
    {
        print("Down");
    }
    public void OnRotateLeft()
    {
        print("left");
    }
    public void OnRotateRight()
    {
        print("right");
    }
    
}
