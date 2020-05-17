using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Gun currentGun;

    private float currentFireRate;

    // private bool isReload = false;


    // Update is called once per frame
    void Update()
    {
        GunFireRateCalc();
       
        TryReload();
        TryFire();

    }

    private void GunFireRateCalc()
    {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime;
    }

   
        
    private void TryFire()
    {
        if (Input.GetButton("Fire1") && currentFireRate <= 0)
        {
            Fire();
        }
    }
    private void TryReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void Reload()
    {
        int RE = currentGun.reloadBulletCount - currentGun.currentBulletCount;


        if (currentGun.reloadBulletCount == currentGun.currentBulletCount)
        {
            Debug.Log("총알이 가득 차 있음");
            return;
        }
        else if (currentGun.carryBulletCount > 0)
        {
            if (currentGun.carryBulletCount + currentGun.currentBulletCount <= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount += currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
                return;
            }
            currentGun.carryBulletCount -= RE;
            currentGun.currentBulletCount = currentGun.currentBulletCount + RE;
            Debug.Log("재장전");
        }
        else 
        {
            Debug.Log("남은 총알 없음");
            return;
        }
    }
   

    


    private void Fire()
    {
        currentFireRate = currentGun.fireRate;
        Shoot();
    }

    private void Shoot()
    {
        if (currentGun.currentBulletCount > 0)
        {
            currentGun.muzzleFlesh.Play();
            currentGun.currentBulletCount--;
            Debug.Log("총알 발사함");
        }
        else
        {
            Debug.Log("장전된 총알이 없음");
        }
    }
}
