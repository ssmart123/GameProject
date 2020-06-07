using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Gun currentGun;

    private float currentFireRate;

    private bool isReload = false;
    private bool isFineSightMode = false;

    // 본래 포지션 값.
    [SerializeField]
    private Vector3 originPos;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }
    // Update is called once per frame
    void Update()
    {
        GunFireRateCalc();
        TryFire();
        TryReload();
        TryFineSight();


    }

    void GunFireRateCalc() 
    {
        if (currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    void TryFire()
    {
        if (Input.GetButton("Fire1") && currentFireRate <= 0 && !isReload)
        {
            Fire();
        }
    }
    void Fire()
    {
        if (!isReload)
        {
            if (currentGun.currentBulletCount > 0)
                Shoot();
            else
                StartCoroutine(ReloadCoroutine());
           
        }
        
    }
    void Shoot()
    {
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate;// 연사속도 재계산
        PlaySE(currentGun.fire_Sound);
        currentGun.anim.SetTrigger("Fire");
        currentGun.muzzleFlesh.Play();

        Debug.Log("총알 발사함");
    }

    void TryReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.currentBulletCount < currentGun.reloadBulletCount)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }
    IEnumerator ReloadCoroutine()
    {
        if (currentGun.carryBulletCount > 0)
        {
            isReload = true;
            currentGun.anim.SetTrigger("Reload");

            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;

            yield return new WaitForSeconds(currentGun.reloadTime);


            if (currentGun.carryBulletCount >= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;
            }
            else
            {
                currentGun.currentBulletCount = currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
            }
            isReload = false;

        }
        else 
        {
            Debug.Log("소유한 총알이 없습니다.");
        }
    }

    void TryFineSight()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            FineSight();
        }
    }

    private void FineSight()
    {
        isFineSightMode = !isFineSightMode;
        currentGun.anim.SetBool("FineSightMode", isFineSightMode);
        if (isFineSightMode)
        {
            StopAllCoroutines();
            StartCoroutine(FineSightActivateCoroutine());
        }
        else 
        {
            StopAllCoroutines();
            StartCoroutine(FineSightDeactivateCoroutine());            
        }
    }

    IEnumerator FineSightActivateCoroutine()
    {
        while (currentGun.transform.localPosition != currentGun.fineSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.2f);
            yield return null;
        }
    }
    IEnumerator FineSightDeactivateCoroutine()
    {

        while (currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.2f);
            yield return null;
        }
    }


    private void PlaySE(AudioClip _clip)
    {
       // audioSource.clip = _clip;
       // audioSource.Play();
    }
}
