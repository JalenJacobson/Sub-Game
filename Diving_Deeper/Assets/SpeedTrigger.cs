using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTrigger : MonoBehaviour
{

    public GameObject Vessel;
    public VesselMovement VesselMovement_Script;
    // Start is called before the first frame update
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
        
        VesselMovement_Script.speedMove();
        Destroy(gameObject);
    }
}
