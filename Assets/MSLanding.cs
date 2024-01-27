using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MSLanding : MonoBehaviour
{

    public GameObject Mothership;
    // public Animator MSanim;
    public CinemachineVirtualCamera virtualCamera;
    public bool zoomOut = false;
    public GameObject RiddleGuessPannel;
    public bool startLanding = false;
    public bool zoomIn = false;
    public GameObject LZ;

    //public GameObject panelOpenButton;

    public Renderer rend;
    public bool alreadyTriggered = false;
    
    void Awake()
    {
        Mothership = GameObject.FindGameObjectWithTag("egg");
        // MSanim = Mothership.GetComponent<Animator>();
        
        rend = GetComponent<Renderer>();
        
    }
    
    void Start()
    {
        
        // panelOpenButton = GameObject.Find("RiddleButton");
        // RiddleGuessPannel = GameObject.Find("SolveRiddlePannel");
    }

    void FixedUpdate()
    {
        if(startLanding)
        {
            var fromPosition = Mothership.GetComponent<Transform>().position;
            var toPosition = LZ.GetComponent<Transform>().position + new Vector3(0, 3, 0);
            Mothership.GetComponent<Transform>().position = Vector3.Lerp(fromPosition, toPosition, .05f);

            var fromRotation = Mothership.GetComponent<Transform>().rotation;
            var toRotation = Quaternion.Euler(new Vector3(0,0,0));
            Mothership.GetComponent<Transform>().rotation = Quaternion.Lerp(fromRotation, toRotation, .05f);
            
        }
        if(zoomIn)
        {
            var endZoomIn = new Vector3(4.26f, .19f, -3.2f);
            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, endZoomIn, Time.deltaTime * 1f);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if(alreadyTriggered) return;
        if(other.tag == "egg")
        {
            rend.enabled = false;
            StartCoroutine("landingSequence");
        }
        
        
        
    }

    public IEnumerator landingSequence()
    {
        Mothership.GetComponent<SphereCollider>().enabled = false;
        Mothership.GetComponent<Rigidbody>().isKinematic = true;
        //GameObject.Find("Player2").SetActive(false);
        //GameObject.Find("LivesRemainingImages").SetActive(false);
        alreadyTriggered = true;
        //virtualCamera.m_LookAt = Mothership.GetComponent<Transform>();
        // virtualCamera.m_Follow = Mothership.GetComponent<Transform>();
        zoomIn = true;
        yield return new WaitForSeconds(2);
        startLanding = true;
        yield return new WaitForSeconds(2);
        Mothership.GetComponent<Animator>().Play("Landing");
        yield return new WaitForSeconds(2);
        // panelOpenButton.SetActive(false);
        RiddleGuessPannel.SetActive(true);


    }
}
