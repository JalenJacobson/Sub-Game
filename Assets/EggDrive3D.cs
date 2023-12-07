using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggDrive3D : MonoBehaviour
{
    public float x;
    public float y;
    public Rigidbody rb;
    public int speed;

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

    public void processInputs()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        if(Input.GetMouseButtonDown(0))
        {
            jump();
        }
    }

    public void Move()
    {
        rb.AddForce(new Vector3(x * speed, 0, y * speed));
    }

    public void jump()
    {
        rb.AddForce(Vector3.up * 5000);
    }
}
