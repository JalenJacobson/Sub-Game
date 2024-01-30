using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InitiateDriveMotherShip : MonoBehaviour
{

    //public GameObject RiddlePannel;
   // public RiddlePannel RiddlePannel_Script;
   // public GameObject NewRiddleAvalable;
    public GameObject vessel;
    public VesselMovement VesselMovement_Script;
    public GameObject Mothership;
    public bool zoomOut = false;
    public Animator MSanim;
    public GameObject RedLight;
    public GameObject PurpleLight;
    public CinemachineVirtualCamera virtualCamera;

    public GameObject[] Slugs;
    public GameObject[] Speeders;

    public GameObject NextLevelPannel;
    // Start is called before the first frame update


    void Awake()
    {
       // RiddlePannel = GameObject.Find("RiddlePannel");
       // RiddlePannel_Script = RiddlePannel.GetComponent<RiddlePannel>();
       // NewRiddleAvalable = GameObject.Find("NewIndicator");
        Slugs = GameObject.FindGameObjectsWithTag("Slug");
        PurpleLight.SetActive(false);
    }

    void Start()
    {
        vessel = GameObject.FindGameObjectWithTag("Vessel");
        VesselMovement_Script = vessel.GetComponent<VesselMovement>();
        Mothership = GameObject.Find("MotherShip_Textured");
        MSanim = Mothership.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(zoomOut)
        {
            var endZoomOut = new Vector3(0, 4.20999f, -1000);
            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, endZoomOut, Time.deltaTime * .15f);
        }
    }

    
    

    void OnTriggerEnter(Collider other)
    {
        
        
        if(other.name.Contains("weenie"))
        {
           // RiddlePannel_Script.addRiddlePart();
           // NewRiddleAvalable.SetActive(true);
            PlayerPrefs.SetInt("climb", 1);
            VesselMovement_Script.nextTrigger(false);
            Speeders = GameObject.FindGameObjectsWithTag("Speeder");
            StartCoroutine("initiateDriveMotherShip");
        }
    }

    public IEnumerator initiateDriveMotherShip()
    {
        Vector3 newRotation = new Vector3(90, 0, 0);
        deactivateSlugs();
        deactivateSpeeders();
        VesselMovement_Script.vesselDead = true;
        vessel.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(90,0,0));
        virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 4.20999f, -300);
        virtualCamera.GetCinemachineComponent<CinemachineComposer>().m_ScreenY = 0.55f;
        MSanim.Play("ShieldClose");
        VesselMovement_Script.drivingMS = true;
        // virtualCamera.m_Lens.FieldOfView = 0;
        yield return new WaitForSeconds(2f);
        vessel.GetComponent<SphereCollider>().enabled = false;
        //Mothership.GetComponent<SphereCollider>().enabled = true;
        // VesselMovement_Script.lookingAtNextTrigger = true;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 5f;
        yield return new WaitForSeconds(2f);
        zoomOut = true;
        //Mothership.transform.parent = vessel.transform;
        VesselMovement_Script.speed = 2000;
        
        yield return new WaitForSeconds(8f);
        // VesselMovement_Script.lookingAtNextTrigger = false;
        zoomOut = false;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        PurpleLight.SetActive(true);
        RedLight.SetActive(false);
        yield return new WaitForSeconds(2f);
        NextLevelPannel.SetActive(true);
        
    }

    public void deactivateSlugs()
    {
        foreach(GameObject slug in Slugs)
        {
            slug.transform.Find("Trigger").gameObject.SetActive(false);
            slug.transform.Find("SludgeHead").gameObject.GetComponent<MeshCollider>().enabled = false;
            slug.transform.Find("Sludge").gameObject.GetComponent<MeshCollider>().enabled = false;
            slug.transform.Find("Sludge1").gameObject.GetComponent<MeshCollider>().enabled = false;
        }
    }
    public void deactivateSpeeders()
    {
        foreach(GameObject speeder in Speeders)
        {

            speeder.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
