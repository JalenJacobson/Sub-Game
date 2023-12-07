using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggDrive3D : MonoBehaviour
{
    public float x;
    public float y;
    public Rigidbody rb;
    public int speed;
    public bool grounded = true;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        processInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("ground"))
        {
            print("touching");
            grounded = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == ("ground"))
        {
            print("not touching");
            grounded = false;
        }
    }

    public void processInputs()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        if(Input.GetMouseButtonDown(0) && grounded)
        {
            jump();
        }
    }

    public void Move()
    {
        rb.AddForce(new Vector3(x * speed, -100f, y * speed));
    }

    public void jump()
    {
        rb.AddForce(Vector3.up * 2000);
    }

    
}
