using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePanel : MonoBehaviour
{
    public GameObject ObjectivePannel;
    public GameObject ObjectiveMission;
    public GameObject ObjectiveEnemy;
    public GameObject ObjectiveCamp;
    public GameObject ObjectiveDefender;
    public GameObject ObjectiveShooter;
    public GameObject ObjectiveNest;
    public GameObject CampHollagram;
    public Animator anim;
    public VesselMovement vessel;
    public GameObject Music;
    // Start is called before the first frame update
    void Start()
    {
        anim = CampHollagram.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void instructionsOpen()
    {
        ObjectivePannel.SetActive(true);
    }
    public void instructionsNextCamp()
    {
        ObjectiveMission.SetActive(false);
        ObjectiveEnemy.SetActive(true);
        ObjectiveCamp.SetActive(true);
        CampHollagram.SetActive(true);
    }
    public void instructionsNextDefender()
    {
        ObjectiveCamp.SetActive(false);
        ObjectiveDefender.SetActive(true);
        anim.Play("Defenders"); 
    }
    public void instructionsNextShooter()
    {
        ObjectiveDefender.SetActive(false);
        ObjectiveShooter.SetActive(true);
        anim.Play("Shooters"); 
    }
    public void instructionsNextNest()
    {
        ObjectiveShooter.SetActive(false);
        ObjectiveNest.SetActive(true);
        anim.Play("Nest"); 
    }
    public void instructionsClose()
    {
        ObjectivePannel.SetActive(false);
        Music.SetActive(true);
        vessel.SendMessage("startGame");
    }
    public void platformInstructionsClose()
    {
        ObjectivePannel.SetActive(false);
        Music.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("weenie"))
        {
            ObjectivePannel.SetActive(true);
        }
    }
}
