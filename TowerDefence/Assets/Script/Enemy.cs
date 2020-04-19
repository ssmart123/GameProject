using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 현재 스폰된 적들 리스트
    /// </summary>
    public static List<Enemy> enemies = new List<Enemy>();

    public float moveSpeed;
    public float hp;
    Vector3 targetPos = Vector3.zero;
    public Transform modelTrans;
    Tile currentTile = null;

    private void Awake()
    {
        enemies.Add(this);
    }
    private void OnDestroy()
    {
        enemies.Remove(this);
    }

    private void Update()
    {
        if (targetPos != Vector3.zero)
        {
            transform.Translate((targetPos - transform.position)
             .normalized * moveSpeed * Time.deltaTime);

            if ((targetPos - transform.position).magnitude <= 0.3f)
            {
                transform.position = targetPos;
                targetPos = Vector3.zero;
                NextMove();
            }
        }
    }

    public void NextMove()
    {
        var targetTile = TileManager.Instance.GetNextWayPoint(currentTile);
        if(targetTile == null)
        {
            Destroy(gameObject);
            return;
        }
        Move(targetTile);
    }


    /// <summary>
    /// 해당 타일로 이동하는 메소드
    /// </summary>
    /// <param name="tile">해당 타일</param>
    public void Move(Tile tile)
    {
        currentTile = tile;
        targetPos = tile.transform.position;

        modelTrans.LookAt(
            new Vector3(targetPos.x, modelTrans.position.y, targetPos.z)
            );
    }
}
