using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject[] bulletPrefabs;
    public float[] reloadTimes;
    public float bulletSpeed = 100f;
    public Transform target;
    public int currentAmmo = 0;
    public float reloadTime = 0;
    public bool canShoot = true;
    public GameObject Crosshairs;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        if(reloadTime > 0)
        {
            canShoot = false;
            reloadTime -= 1f * Time.deltaTime;
        }
        else if(reloadTime <= 0)
        {
            canShoot = true;
        }
        if(Input.GetMouseButtonDown(0))
        {
            if(!canShoot)return;
            var bullet = Instantiate(bulletPrefabs[currentAmmo], bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            float lifetime = getBulletLifetime();
            // var direction = new Vector3(0,0,90);
            // bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            bullet.GetComponent<Bullet>().aim = bulletSpawnPoint.forward;
            bullet.GetComponent<Bullet>().lifeTime = lifetime;
            reloadTime += reloadTimes[currentAmmo];
        }

        

        if(Input.mouseScrollDelta.y != 0)
        {
            print(Input.mouseScrollDelta.y);
            if(Input.mouseScrollDelta.y >= 1)
            {
                print("Down");
                if(currentAmmo >0)
                {
                    currentAmmo --;
                }
                
            }
            if(Input.mouseScrollDelta.y <= -1)
            {
                print("Up");
                if(currentAmmo < 2)
                {
                    currentAmmo ++;
                }
            }
        }

        if(currentAmmo == 0)
        {
            Crosshairs.gameObject.SendMessage("standardAmmo");
        }
        else if(currentAmmo == 1)
        {
            Crosshairs.gameObject.SendMessage("shotGunAmmo");
        }
        else if(currentAmmo == 2)
        {
            Crosshairs.gameObject.SendMessage("sniperAmmo");
        }
    }

    public float getBulletLifetime()
    {
        if(currentAmmo == 0)
        {
            return 2f;
        }
        else if(currentAmmo == 1)
        {
            return .25f;
        }
        else if(currentAmmo == 3)
        {
            return 99f;
        }
        else return 2f;
    }

    public void changeAmmoType()
    {

    }
}
