using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatCamera : MonoBehaviour
{
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera"); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.GetComponent<Transform>());
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z);

    }
}