using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Tooltip("총 이름")]
    public string gunName;
    [Tooltip("사정거리")] 
    public float Range;
    [Tooltip("정확도")]
    public float Accuracy;
    [Tooltip("연사속도")]
    public float fireRate;
    [Tooltip("재장전 속도")]
    public float relodeBulletCount;


    [Tooltip("총의 데미지")]
    public int damage;

    [Tooltip("총알 재장전 갯수")]
    public int reloadBulletCount;
    [Tooltip("현제 탄알집에 남았는 총알의 갯수")]
    public int currentBulletCount;
    [Tooltip("최대 소유 가능 총알 갯수")]
    public int maxBulletCount;
    [Tooltip("현재 소유하고 있는 총알 갯수")]
    public int carryBulletCount;

    [Tooltip("반동 세기")]
    public float retroActionForce;
    [Tooltip("정조준시 반동 세기")]
    public float retroActionFineSightForce;

    public Vector3 fineSightOriginPos;
    public Animator anim;
    public ParticleSystem muzzleFlesh;

    public AudioClip fire_Sound; 

}
