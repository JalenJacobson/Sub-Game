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

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == ("ground"))
        {
            print("touching");
            grounded = true;
        }
        if(collision.gameObject.tag == ("bounce"))
        {
            rb.AddForce(Vector3.up * bounceForce);
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
        
    }

    public void Move()
    {
        rb.AddForce(new Vector3(x * speed, 0, y * speed));
    }

    public void jump(Vector3 direction)
    {
        if(!grounded) return;
        rb.AddForce(direction * jumpForce);
    }

    
}
