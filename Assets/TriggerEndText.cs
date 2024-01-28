using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEndText : MonoBehaviour
{
    public GameObject EndText;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("egg"))
        {
            EndText.SetActive(true);
        }
    }
}
