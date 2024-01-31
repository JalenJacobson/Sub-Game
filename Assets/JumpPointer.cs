using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPointer : MonoBehaviour
{

    public Transform target;
    public Transform rockets;
    public GameObject Ship;
    public EggDrive3D Ship_Script;
    private LineRenderer lr;
    public Vector3 grapplePoint;
    public Transform grappleShootPoint, MotherShip;
    public EggDrive3D eggDrive_Script;
    public LayerMask canGrapple;
    public SpringJoint joint;
    private Vector3 currentGrapplePosition;
    public bool inRangeGrapple = false;
    public bool grappling = false;
    public Camera cam;
   

    void Awake()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        eggDrive_Script = MotherShip.gameObject.GetComponent<EggDrive3D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Ship_Script = Ship.GetComponent<EggDrive3D>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        if(Input.GetMouseButtonDown(0))
        {
            Ship_Script.jump(rockets.transform.forward);
        }
        if(Input.GetMouseButtonUp(0))
        {
            Ship_Script.jumpKill();
        }

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 1000f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(cam.transform.position, mousePos - cam.transform.position, Color.blue);

        if(Input.GetMouseButtonDown(1))
        {
            startGrapple();
        }
        else if(Input.GetMouseButtonUp(1))
        {
            endGrapple();
        }
        if(!grappling && joint != null)
        {
            endGrapple();
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void DrawRope() {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, grappleShootPoint.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    void startGrapple()
    {
        if(!inRangeGrapple) return;
        grappling = true;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, canGrapple))
        {
            if(!hit.transform.gameObject.GetComponent<GrapplePoint>().grappleable)
            {
                // print("didn't work");
                return;
            } 
            // print("should grapple" + hit.transform.name);
            eggDrive_Script.startGrapple();
            grapplePoint = hit.transform.position;
            joint = MotherShip.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(MotherShip.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = 50f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = grappleShootPoint.position;
        }
    }
    void endGrapple()
    {
        
        grappling = false;
        eggDrive_Script.endGrapple();
        lr.positionCount = 0;
        Destroy(joint);
    }
}
