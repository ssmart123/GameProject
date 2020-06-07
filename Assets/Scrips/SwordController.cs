using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField]
    private Sword currentSword;

    private bool isAttack=false;
    private bool isSwing=false;

    private RaycastHit hitInfo;

    private void Update()
    {
        TryAttack();
    }
    private void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!isAttack)
            { 
                // 코루틴 실행
            }
        }
    }
    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        //currentSword.Anim.SetTrigger("Attack"); 좌클릭시 공격 애니메이션 실행

        yield return new WaitForSeconds(currentSword.AttackDelayA);
        isSwing = true;
        //공격 활성화 시점


        yield return new WaitForSeconds(currentSword.AttackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentSword.AttackDelayC - currentSword.AttackDelayA - currentSword.AttackDelayB);




        isAttack = false;
    }
    IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject()) 
            {
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;

        }
    }
    private bool CheckObject()
    {
        if(Physics.Raycast(transform.position,transform.forward,out hitInfo,currentSword.range))
        {
            return true;
        }
        return false;
    }



}
