using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("총 이름")]
    public string gunName;
    [Header("사정거리")] 
    public float Range;
    [Header("정확도")]
    public float Accuracy;
    [Header("연사속도")]
    public float fireRate;
    [Header("재장전 속도")]
    public float reloadCount;
    [Header("재장전 시간")]
    public float reloadTime;


    [Header("총의 데미지")]
    public int damage;

    [Header("총알 재장전 갯수")]
    public int reloadBulletCount;
    [Header("현재 탄알집에 남았는 총알의 갯수")]
    public int currentBulletCount;
    [Header("최대 소유 가능 총알 갯수")]
    public int maxBulletCount;
    [Header("현재 소유하고 있는 총알 갯수")]
    public int carryBulletCount;

    [Header("반동 세기")]
    public float retroActionForce;
    [Header("정조준시 반동 세기")]
    public float retroActionFineSightForce;

    public Vector3 fineSightOriginPos;
    public Animator anim;
    public ParticleSystem muzzleFlesh;

    public AudioClip fire_Sound; 

}
