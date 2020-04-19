using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float damage, range, attackSpeed;
    public Transform headerTrans;
    private Coroutine _attackRoutine;
    private Enemy _currentTarget;

    public GameObject fxPrefab;
    public List<Transform> attackFxPosList;

    private void Start()
    {
        StartCoroutine(UpdateCoroutine());
    }

    IEnumerator UpdateCoroutine()
    {
        //사정거리 안에 들어가는 Enemy들을 찾아와야 된다.
        // 경우의 수는 2가지
        // 1. 유니티 자체 기능 RayCast 사용한다.
        //      ㄴ Collider를 이용하는 방법
        // 2. Enemy들을 List에 담아서 각자 거리계산을 한다.
        //      ㄴ Collider 없어도 작동함 / 정확함
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            if (_currentTarget != null) /// 공격 중일 경우
            {
                headerTrans.LookAt(
                    new Vector3(_currentTarget.transform.position.x, headerTrans.position.y, _currentTarget.transform.position.z)
                    );
                continue;
            }

            foreach (var enemy in Enemy.enemies)
            {
                var dist = Vector3.Distance(transform.position, enemy.transform.position);
                if (dist <= range * 6)
                {
                    Attack(enemy);
                }
            }
        }
    }

    private void Attack(Enemy enemy)
    {
        if(_attackRoutine != null)
        {
            return;
        }

        _currentTarget = enemy;
        _attackRoutine = StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        while (_currentTarget != null &&
            Vector3.Distance(transform.position, _currentTarget.transform.position) <= range * 6)
        {
            yield return new WaitForSeconds(1 / attackSpeed);
            Debug.Log($"공격함! 타겟 체력 : {_currentTarget.hp} Dmg : {damage}");

            StartCoroutine(FxCoroutine());
            _currentTarget.hp -= damage;
            if(_currentTarget.hp <= 0)
            {
                if(_currentTarget != null)
                    Destroy(_currentTarget.gameObject);
            }
        }

        _currentTarget = null;
        _attackRoutine = null;
    }

    IEnumerator FxCoroutine()
    {
        List<GameObject> objList = new List<GameObject>();

        foreach (var item in attackFxPosList)
        {
            GameObject obj = Instantiate(fxPrefab);
            obj.transform.position = item.position;
            objList.Add(obj);
        }

        yield return new WaitForSeconds(1);

        foreach (var obj in objList)
        {
            Destroy(obj);
        }
    }
}
