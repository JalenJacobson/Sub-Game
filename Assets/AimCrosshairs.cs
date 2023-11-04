using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class AimCrosshairs : MonoBehaviour
{

    public CinemachineVirtualCamera virtualCamera;
    public Camera cam;
    public float zoffset;
    public float yoffset;
    public Image i_Weapon;
    public GameObject Weapon;
    public Image i_Ammo;
    public GameObject Ammo;
    public Sprite[] spriteArray1;
    public Sprite[] spriteArray2;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        i_Weapon = Weapon.GetComponent<Image>();
        i_Ammo = Ammo.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 mousePos = Input.mousePosition;
        // mousePos.z = 10000;
        // mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        // transform.position = mousePos;
        
        // print(Input.mousePosition);
        transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y + yoffset, zoffset));
    }

    public void standardAmmo()
        {
            i_Weapon.sprite = spriteArray1[0];
            i_Ammo.sprite = spriteArray2[0];  
        }
    public void shotGunAmmo()
        {
            i_Weapon.sprite = spriteArray1[1];
            i_Ammo.sprite = spriteArray2[1];
        }
    public void sniperAmmo()
        {
            i_Weapon.sprite = spriteArray1[2];
            i_Ammo.sprite = spriteArray2[2];
        }

    
    
}
