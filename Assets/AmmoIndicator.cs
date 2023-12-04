using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoIndicator : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RapidFire()
    {
        anim.Play("RapidFire");
    }
    public void BurstFire()
    {
        anim.Play("BurstFire"); 
    }
    public void LayMines()
    {
        anim.Play("LayMines");    
    }
}
