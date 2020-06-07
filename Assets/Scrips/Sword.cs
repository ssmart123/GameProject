using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public string SwordName;
    public float range;
    public float Damage;
    public float AttackSpeed;
    public float AttackDelayA; // 선딜
    public float AttackDelayB;
    public float AttackDelayC; // 후딜

    public Animator Anim;
    //  public BoxCollider BoxCollider; 박스 콜라이더가 접촉시 데미지 들어감
   
}
