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
    // Start is called before the first frame update

    void Awake()
    {
        // RiddlePannel = GameObject.Find("RiddlePannel");
        // RiddlePannel_Script = RiddlePannel.GetComponent<RiddlePannel>();
        // NewRiddleAvalable = GameObject.Find("NewIndicator");
    }

    void Start()
    {
        
        
        Vessel = GameObject.FindGameObjectWithTag("Vessel");
        VesselMovement_Script = Vessel.GetComponent<VesselMovement>();
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
            Destroy(gameObject);
        }
        //RiddlePannel_Script.addRiddlePart();
        //NewRiddleAvalable.SetActive(true);
        
    }
}
