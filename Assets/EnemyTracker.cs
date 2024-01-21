using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTracker : MonoBehaviour
{
    public Vector3 point;
    public Camera cam;
    public VesselMovement VesselMovement_Script;
    public Transform lockOnEnemy;
    public bool isOffScreen;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>(); 
        VesselMovement_Script = GameObject.FindGameObjectWithTag("Vessel").GetComponent<VesselMovement>();
        gameObject.GetComponent<Image>().enabled = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(VesselMovement_Script.lockOnEnemy != null)
        {
            lockOnEnemy = VesselMovement_Script.lockOnEnemy;
            Vector3 lockOnEnemyposition = lockOnEnemy.position;
            point = cam.WorldToScreenPoint(lockOnEnemyposition);
        }    
        isOffScreen = point.x <= 0 || point.x >= Screen.width || point.y <= 0 || point.y >= Screen.height;
        if(isOffScreen || VesselMovement_Script.lockOnEnemy == null)
        {
            GetComponent<Image>().enabled = false;
            print("isoffscreen");
        }
        else if(!isOffScreen || VesselMovement_Script != null) gameObject.GetComponent<Image>().enabled = true;
        if(point.z >= 1000)
        {
            point.z = 1000;
        }
        
        
        transform.position = point;
    }
}
