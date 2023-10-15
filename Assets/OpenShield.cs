using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShield : MonoBehaviour
{

    public GameObject Mothership;
    public Animator MSanim;

    public GameObject vessel;
    public VesselMovement VesselMovement_Script;
    public bool openSequenceStarted = false;

    public GameObject RiddlePannel;
    public RiddlePannel RiddlePannel_Script;
    public GameObject NewRiddleAvalable;

    
    // Start is called before the first frame update
    
    void Awake()
    {
        RiddlePannel = GameObject.Find("RiddlePannel");
        RiddlePannel_Script = RiddlePannel.GetComponent<RiddlePannel>();
        NewRiddleAvalable = GameObject.Find("NewIndicator");
        
    }
    
    void Start()
    {
        Mothership = GameObject.Find("MotherShip_Textured");
        MSanim = Mothership.GetComponent<Animator>();
        vessel = GameObject.FindGameObjectWithTag("Vessel");
        VesselMovement_Script = vessel.GetComponent<VesselMovement>();
        
    }

    void FixedUpdate()
    {
        if(openSequenceStarted)
        {
              
            //VesselMovement_Script.lookAtNextTrigger();
            
            
        }
    }

    void OnTriggerEnter()
    {
       // RiddlePannel_Script.addRiddlePart();
       // NewRiddleAvalable.SetActive(true);
        //VesselMovement_Script.nextTrigger();
        gameObject.GetComponent<Transform>().position = new Vector3(10000, 10000, 0);
        StartCoroutine("openShieldRoutine");
    }

    public IEnumerator openShieldRoutine()
    {
        openSequenceStarted = true;
        VesselMovement_Script.vesselDead = true;
        yield return new WaitForSeconds(3f);
        MSanim.Play("ShieldOpen");
        yield return new WaitForSeconds(8f);
        openSequenceStarted = false;
        VesselMovement_Script.vesselDead = false;
    }
}
