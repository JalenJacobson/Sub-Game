using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFloor : MonoBehaviour
{
    public msFollowObject msFollowObject_Script;
    // Start is called before the first frame update
    void Start()
    {
        msFollowObject_Script = GameObject.Find("MotherShipFollow Object").GetComponent<msFollowObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "egg")
        {
            other.GetComponent<EggDrive3D>().deathByFall();
            msFollowObject_Script.deathByFall();
        }
    }
}
