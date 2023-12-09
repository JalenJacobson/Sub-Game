using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPointer : MonoBehaviour
{

    public Transform target;
    public GameObject Ship;
    public EggDrive3D Ship_Script;
    // Start is called before the first frame update
    void Start()
    {
        Ship_Script = Ship.GetComponent<EggDrive3D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        if(Input.GetMouseButtonDown(0))
        {
            Ship_Script.jump(transform.forward);
        }
    }
}
