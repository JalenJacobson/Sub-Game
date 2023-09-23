using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimCrosshairs : MonoBehaviour
{

    public CinemachineVirtualCamera virtualCamera;
    public Camera cam;
    public float zoffset;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 mousePos = Input.mousePosition;
        // mousePos.z = 10000;
        // mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        // transform.position = mousePos;
        
        // print(Input.mousePosition);
        transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zoffset));
    }
    
}
