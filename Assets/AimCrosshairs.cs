using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimCrosshairs : MonoBehaviour
{

    public CinemachineVirtualCamera virtualCamera;
    public Camera cam;
    public float zoffset;
    public float yoffset;
    public SpriteRenderer spriteRendererWeapon;
    public GameObject Weapon;
    public SpriteRenderer spriteRendererAmmo;
    public GameObject Ammo;
    public Sprite[] spriteArray1;
    public Sprite[] spriteArray2;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        spriteRendererWeapon = Weapon.GetComponent<SpriteRenderer>();
        spriteRendererAmmo = Ammo.GetComponent<SpriteRenderer>();
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
            spriteRendererWeapon.sprite = spriteArray1[0];
            spriteRendererAmmo.sprite = spriteArray2[0];  
        }
    public void shotGunAmmo()
        {
            spriteRendererWeapon.sprite = spriteArray1[1];
            spriteRendererAmmo.sprite = spriteArray2[1];
        }

    
    
}
