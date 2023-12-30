using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleTrigger : MonoBehaviour
{

    public GameObject RiddlePannel;
    public RiddlePannel RiddlePannel_Script;
    public GameObject NewRiddleAvalable;

    public GameObject Vessel;
    public VesselMovement VesselMovement_Script;
    public GameObject ScreenRiddle;

    public bool isTrainingTrigger;
    // Start is called before the first frame update

    void Awake()
    {
        RiddlePannel = GameObject.Find("RiddlePannel");
        RiddlePannel_Script = RiddlePannel.GetComponent<RiddlePannel>();
        NewRiddleAvalable = GameObject.Find("NewIndicator");
        Vessel = GameObject.FindGameObjectWithTag("Vessel");
        VesselMovement_Script = Vessel.GetComponent<VesselMovement>(); 
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("weenie"))
        {
            VesselMovement_Script.nextTrigger();
            if(!isTrainingTrigger)
            {
                RiddlePannel_Script.addRiddlePart();
                NewRiddleAvalable.SetActive(true);
                ScreenRiddle.SetActive(true);
            }
            Destroy(gameObject);
        }
        
        
    }
}
