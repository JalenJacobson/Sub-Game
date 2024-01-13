using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePanel : MonoBehaviour
{
    public GameObject ObjectivePannel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void instructionsOpen()
    {
        ObjectivePannel.SetActive(true);
    }
    public void instructionsClose()
    {
        ObjectivePannel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("weenie"))
        {
            ObjectivePannel.SetActive(true);
        }
    }
}
