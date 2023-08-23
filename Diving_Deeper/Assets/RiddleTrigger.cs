using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleTrigger : MonoBehaviour
{

    public GameObject RiddlePannel;
    public RiddlePannel RiddlePannel_Script;
    // Start is called before the first frame update
    void Start()
    {
        RiddlePannel_Script = RiddlePannel.GetComponent<RiddlePannel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        RiddlePannel_Script.addRiddlePart();
        
    }
}
