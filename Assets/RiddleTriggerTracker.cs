using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiddleTriggerTracker : MonoBehaviour
{

    public Vector3 point;
    public Camera cam;
    public VesselMovement VesselMovement_Script;
    public GameObject currentRiddleTrigger;
    public bool isOffScreen;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>(); 
        VesselMovement_Script = GameObject.FindGameObjectWithTag("Vessel").GetComponent<VesselMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentRiddleTrigger = VesselMovement_Script.riddleTriggers[VesselMovement_Script.currentRiddleTrigger];
        Vector3 riddleTriggerPosition = currentRiddleTrigger.GetComponent<Transform>().position;
        point = cam.WorldToScreenPoint(riddleTriggerPosition);
        isOffScreen = point.x <= 0 || point.x >= Screen.width || point.y <= 0 || point.y >= Screen.height;
        if(isOffScreen)
        {
            GetComponent<Image>().enabled = false;
        }
        else if(!isOffScreen) gameObject.GetComponent<Image>().enabled = true;
        if(point.z >= 1000)
        {
            point.z = 1000;
        }
        
        
        transform.position = point;
    }
}
