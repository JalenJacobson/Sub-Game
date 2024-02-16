using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPointer_AimFollow : MonoBehaviour
{
    public Camera cam;
    public float zoffset;
    public LayerMask jumpPointerIntersectPlane;

    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        // transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zoffset));
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, jumpPointerIntersectPlane))
        {
            transform.position = hit.point;
        }
    }
}
