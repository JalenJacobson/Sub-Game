using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePanelTrigger : MonoBehaviour
{
    public GameObject ObjectivePannel;
    public VesselMovement vessel;
    // Start is called before the first frame update
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
            ObjectivePannel.SetActive(true);
            vessel.SendMessage("pauseGame");
        }
    }
}
