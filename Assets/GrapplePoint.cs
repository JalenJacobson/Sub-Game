using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    // public Light lt;
    // Color color0 = Color.red;
    // Color color1 = Color.blue;
    public bool grappleable = false;

    // Start is called before the first frame update
    void Start()
    {
        // lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void canGrapple()
    {
        // lt.color = color1;
        grappleable = true;

    }
    public void cantGrapple()
    {
        // lt.color = color0;
        grappleable = false;
    }
}
