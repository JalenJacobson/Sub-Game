using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msFollowObject : MonoBehaviour
{

    public GameObject MS;
    public float yOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = MS.GetComponent<Transform>().position + new Vector3(0, yOffset, 0);  
    }
}
