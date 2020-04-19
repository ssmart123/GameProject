﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = FindObjectOfType<TileManager>();

            if (_Instance == null)
            {
                GameObject obj = new GameObject("Manager");
                _Instance = obj.AddComponent<TileManager>();
            }

            return _Instance;
        }
    }
    private static TileManager _Instance;

    public int sizeX, sizeY;
    public List<TileBuildData> datas = new List<TileBuildData>();
    
    [HideInInspector]
    public List<Tile> tiles = new List<Tile>();

    public List<Tile> wayPoints = new List<Tile>();
    public Tile spawnPoint;
    public Tile endPoint;
    
    public void Build()
    {
        MakeWay();
        MakeTileMap();
    }

    void MakeTileMap()
    {
        GameObject prefab = Resources.Load<GameObject>("Tile");
        //sizeX 의 크기만큼 반복
        for (int x = 0; x < sizeX; x++)
        {
            //sizeY의 크기만큼 반복
            for(int y = 0; y < sizeY; y++)
            {
                //최종적으로 x * y 만큼 반복
                GameObject obj = Instantiate(prefab);
                obj.transform.position = new Vector3(x * 6, 0, y * 6);
                Tile script = obj.GetComponent<Tile>();
                script.coordinate = new Vector2(x, y);
                tiles.Add(script);
            }
        }

        foreach (var data in datas)
        {
            foreach (var item in tiles)
            {
                if (data.coodinate == item.coordinate)
                {
                    item.SetState(data.tileState);
                    break;
                }
            }
        }
    }

    void MakeWay()
    {
        #region 데이터 설정
        TileBuildData spawnPointData = null;
        TileBuildData endPointData = null;
        List<TileBuildData> wayPointDatas = new List<TileBuildData>();

        foreach(var item in datas)
        {
            switch(item.tileState)
            {
                case Tile.eTileState.SpawnPoint:
                    spawnPointData = item;
                    break;
                case Tile.eTileState.EndPoint:
                    endPointData = item;
                    break;
                case Tile.eTileState.WayPoint:
                    wayPointDatas.Add(item);
                    break;
            }
        } // 데이터 정리
        #endregion

        #region 데이터 가공
        //SpawnPoint -> WayPoint 첫번째
        //WayPoint 첫번째 -> 두번째
        //반복... 남은게 없으면 -> EndPoint
        Vector2 currentVector = spawnPointData.coodinate;

        foreach(var item in wayPointDatas)
        {
            ConnectPoints(currentVector, item.coodinate);
            currentVector = item.coodinate;
        }

        ConnectPoints(currentVector, endPointData.coodinate);
        #endregion
    }

    private void ConnectPoints(Vector2 current,Vector2 destination)
    {
        while (current.x != destination.x)
        {
            if (current.x < destination.x)
                current.x += 1;
            else if (current.x > destination.x)
                current.x -= 1;

            if (current == destination)
                continue;

            datas.Add(new TileBuildData()
            {
                coodinate = current,
                tileState = Tile.eTileState.Way
            });
        }
        while (current.y != destination.y)
        {
            if (current.y < destination.y)
                current.y += 1;
            else if (current.y > destination.y)
                current.y -= 1;

            if (current == destination)
                continue;

            datas.Add(new TileBuildData()
            {
                coodinate = current,
                tileState = Tile.eTileState.Way
            });
        }
    }

    public void Rebuild()
    {
        foreach (var item in tiles)
        {
            Destroy(item.gameObject);
        }
        tiles.Clear();
        wayPoints.Clear();
        Build();
    }

    public Tile GetNextWayPoint(Tile currentTarget)
    {
        for(int i =0; i < wayPoints.Count; i++)
        {
            if(currentTarget == wayPoints[i])
            {
                if (i >= wayPoints.Count - 1)
                {
                    return endPoint;
                }
                return wayPoints[i + 1];
            }
        }


        return null;
    }


    [UnityEditor.MenuItem("테스트/리빌드")]
    public static void ReBuild()
    {
        TileManager.Instance.Rebuild();
    }
}


[System.Serializable]
public class TileBuildData
{
    public Vector2 coodinate;
    public Tile.eTileState tileState;
}
