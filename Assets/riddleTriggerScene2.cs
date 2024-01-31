using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class riddleTriggerScene2 : MonoBehaviour
{

    public GameObject RiddlePannel;
    public RiddlePannel RiddlePannel_Script;
    
    public GameObject egg;
    public EggDrive3D egg_Script;
    public bool shouldAddRiddlePart = false;
    public GameObject ScreenRiddle;

    // Start is called before the first frame update
    void Awake()
    {
        RiddlePannel = GameObject.Find("RiddlePannel");
        RiddlePannel_Script = RiddlePannel.GetComponent<RiddlePannel>();
        egg =  GameObject.FindGameObjectWithTag("egg");
        egg_Script = egg.GetComponent<EggDrive3D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "egg")
        {
            if(shouldAddRiddlePart)
            {
                RiddlePannel_Script.addRiddlePart();
            
                // NewRiddleAvalable.SetActive(true);
                ScreenRiddle.SetActive(true);
                 
            }
            else egg_Script.respawnPoint = transform.position;
            Destroy(gameObject);
        }
    }
}
