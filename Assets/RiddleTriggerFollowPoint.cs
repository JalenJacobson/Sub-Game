using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleTriggerFollowPoint : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
       transform.LookAt(target); 
    }

    public void nextTrigger(GameObject newTrigger)
    {
        target = newTrigger.GetComponent<Transform>();
    }
}
