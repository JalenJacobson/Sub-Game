using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public Transform bulletSpawnPointBack;
    public GameObject[] bulletPrefabs;
    public List<GameObject> activeMines;
    public float[] reloadTimes;
    public float bulletSpeed = 100f;
    public Transform target;
    public int currentAmmo = 0;
    public float reloadTime = 0;
    public bool canShoot = true;
    public GameObject Crosshairs;
    public float changeReset = 0;
    public GameObject AmmoType;
    public AudioSource audioSource;
    public AudioClip RapidFire;
    public AudioClip BurstFire;
    public Bullet grenade;
    public float grenadeReleasePower;
    public bool holdingGrenade = false;

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
            if(currentAmmo == 2)
            {
                var bullet = Instantiate(bulletPrefabs[currentAmmo], bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                grenade = bullet.GetComponent<Bullet>();
                bullet.GetComponent<Bullet>().followPoint = bulletSpawnPoint;
                bullet.GetComponent<Bullet>().aim = bulletSpawnPoint.forward;
                bullet.GetComponent<Bullet>().type = currentAmmo;
                reloadTime += reloadTimes[currentAmmo];
                holdingGrenade = true;
                // activeMines.Add(bullet);
            }
            else
            {
                var bullet = Instantiate(bulletPrefabs[currentAmmo], bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Bullet>().aim = bulletSpawnPoint.forward;
                bullet.GetComponent<Bullet>().type = currentAmmo;
                reloadTime += reloadTimes[currentAmmo];
            }
        }
        if(holdingGrenade)
        {
            if(grenadeReleasePower <= 1)
            {
                grenadeReleasePower += Time.deltaTime;
            }
            
        } 
        if(Input.GetMouseButtonUp(0))
        {
            if(currentAmmo != 2) return;
            grenade.release(grenadeReleasePower, bulletSpawnPoint.forward);
            holdingGrenade = false;
            grenadeReleasePower = 0;
        }


        if(Input.GetMouseButtonDown(1) && currentAmmo == 2)
        {
            // foreach(GameObject mine in activeMines)
            // {
            //     mine.GetComponent<Bullet>().detonate();
            //     print("started");
            // }
            // activeMines.Clear();
            // print("should remove");
            grenade.detonate();
        }

        

        if(Input.mouseScrollDelta.y != 0)
        {
            if(Input.mouseScrollDelta.y >= 1)
            {
                if(currentAmmo >0 && changeReset <= 0)
                {
                    currentAmmo --;
                    changeReset += .5f;
                }
                
            }
            if(Input.mouseScrollDelta.y <= -1)
            {
                if(currentAmmo < 2 && changeReset <= 0)
                {
                    currentAmmo ++;
                    changeReset += .5f;
                }
            }
        }

        if(changeReset >= 0)
        {
            changeReset -= Time.deltaTime;
        }

        if(currentAmmo == 0)
        {
            Crosshairs.gameObject.SendMessage("standardAmmo");
            AmmoType.gameObject.SendMessage("RapidFire");
            if(Input.GetMouseButtonDown(0))
            {
                audioSource.clip = RapidFire;
                audioSource.Play();
            }
            
        }
        else if(currentAmmo == 1)
        {
            Crosshairs.gameObject.SendMessage("shotGunAmmo");
            AmmoType.gameObject.SendMessage("BurstFire");
            if(Input.GetMouseButtonDown(0))
            {
                audioSource.clip = BurstFire;
                audioSource.Play();
            }
        }
        else if(currentAmmo == 2)
        {
            Crosshairs.gameObject.SendMessage("sniperAmmo");
            AmmoType.gameObject.SendMessage("LayMines");
        }
    }

    // public IEnumerator detonateSequence()
    // {
    //     foreach(GameObject mine in activeMines)
    //     {
    //         mine.GetComponent<Bullet>().detonate();
    //         print("started");
    //     }
    //     yield return new WaitForSeconds(3f);
        

    // }

}
