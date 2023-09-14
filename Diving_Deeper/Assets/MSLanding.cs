using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MSLanding : MonoBehaviour
{

    public GameObject Mothership;
    public Animator MSanim;
    public CinemachineVirtualCamera virtualCamera;
    public bool zoomOut = false;
    public GameObject RiddleGuessPannel;
    public bool startLanding = false;
    public bool moveCamera = false;

    public GameObject panelOpenButton;

    public GameObject vessel;
    public VesselMovement VesselMovement_Script;

    public Renderer rend;
    public bool alreadyTriggered = false;
    public bool landingSequenceStarted = false;
    // Start is called before the first frame update
    
    void Awake()
    {
        Mothership = GameObject.Find("MotherShip_Textured");
        MSanim = Mothership.GetComponent<Animator>();
        vessel = GameObject.FindGameObjectWithTag("Vessel");
        VesselMovement_Script = vessel.GetComponent<VesselMovement>();
        rend = GetComponent<Renderer>();
        
    }
    
    void Start()
    {
        
        // panelOpenButton = GameObject.Find("RiddleButton");
        // RiddleGuessPannel = GameObject.Find("SolveRiddlePannel");
    }

    void FixedUpdate()
    {
        // old mother ship location 1891, -1314, 18328
        //new location
        // if(Input.GetKeyDown("q"))
        // {
        //     StartCoroutine("landingSequence");
        //     // startLanding = true;
        // }

        if(startLanding)
        {
            var fromPosition = Mothership.GetComponent<Transform>().position;
            var toPosition = new Vector3(-39, -92, 7303);
            Mothership.GetComponent<Transform>().position = Vector3.Lerp(fromPosition, toPosition, .05f);

            var fromRotation = Mothership.GetComponent<Transform>().rotation;
            var toRotation = Quaternion.Euler(new Vector3(0,0,0));
            Mothership.GetComponent<Transform>().rotation = Quaternion.Lerp(fromRotation, toRotation, .05f);
            
        }
        if(landingSequenceStarted)
        {
            var vesselToPosition = new Vector3(-39, 150, 7303);
            if(vessel.GetComponent<Transform>().position != vesselToPosition)
            {
                vessel.GetComponent<Transform>().position = vesselToPosition;
            }

            var vesselToRotation = Quaternion.Euler(new Vector3(0,0,0));
            if(vessel.GetComponent<Transform>().rotation != vesselToRotation)
            {
                vessel.GetComponent<Transform>().rotation = vesselToRotation;
            }
        }

       
    }


    void OnTriggerEnter(Collider other)
    {
        if(alreadyTriggered) return;
        if(other.name.Contains("wennie") || other.name.Contains("Mother"))
        {
            rend.enabled = false;
            StartCoroutine("landingSequence");
        }
        
        
        
    }

    public IEnumerator landingSequence()
    {
        Mothership.transform.parent = null;
        Mothership.GetComponent<SphereCollider>().enabled = false;
        alreadyTriggered = true;
        landingSequenceStarted = true;
        // vessel.transform.parent = Mothership.transform;
        
        yield return new WaitForSeconds(.25f);
        // moveVessel();
        // vessel.GetComponent<Rigidbody>().isKinematic = true;
        // moveVessel();
        yield return new WaitForSeconds(.25f);
        virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 0, -700);
        yield return new WaitForSeconds(6);
        startLanding = true;
        yield return new WaitForSeconds(5);
        MSanim.Play("Landing");
        yield return new WaitForSeconds(5);
        panelOpenButton.SetActive(false);
        RiddleGuessPannel.SetActive(true);


    }

    public void moveVessel()
    {
        
            var vesselToPosition = new Vector3(-39, 150, 7303);
            vessel.GetComponent<Transform>().position = vesselToPosition;

            
            var vesselToRotation = Quaternion.Euler(new Vector3(0,0,0));
            vessel.GetComponent<Transform>().rotation = vesselToRotation;
    }

    
}
