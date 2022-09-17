using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPointPathManager
{
    //玩家路线集合
    public Vector3[] Points;//路径集合


    public Transform[] MonsterPoint;//怪物进入战斗后站位
    public Transform[] PlayerPoint;//玩家进入战斗后站位
    public MapHex[] MapArray;//地图格子集合

    private static FightPointPathManager instance;//单例
    public static FightPointPathManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FightPointPathManager();
            }
            return instance;

        }
    }
    private FightPointPathManager()
    {
        GetPoints();
        GetMapHexGrid();
        SetMonsterAndPlayerFightPoint();//默认场景
    }

    //设置寻路点
    public void GetPoints()
    {
        GameObject tmp = GameObject.Find("Path");
        Points = new Vector3[tmp.transform.childCount];
        for (int i = Points.Length - 1; 0 <= i; i--)
        {
            Points[i] = tmp.transform.GetChild(i).transform.position;
        }

    }
    /// <summary>
    /// 获取地型格子
    /// </summary>
    public void GetMapHexGrid()
    {
        GameObject tmp = GameObject.Find("MapData");
        MapArray = new MapHex[tmp.transform.childCount];
        MapArray = tmp.GetComponentsInChildren<MapHex>();
        // Debug.Log("MapArray:" + MapArray.Length);
    }

    /// <summary>
    /// 设置怪物和玩家战斗点位
    /// </summary>
    public void SetMonsterAndPlayerFightPoint(string index = "")
    {
        string fightPanel = "FightPoint" + index;
        PlayerPoint = new Transform[Utility.MaxFightPlayer];
        MonsterPoint = new Transform[Utility.MaxFightMonster];
        for (int i = 0; i < Utility.MaxFightMonster; i++)
        {
            MonsterPoint[i] = GameObject.Find(fightPanel + "/Monster" + i).transform;
        }
        for (int i = 0; i < Utility.MaxFightPlayer; i++)
        {
            PlayerPoint[i] = GameObject.Find(fightPanel + "/PlayerPoint").transform;
        }

    }
    public void Destory()
    {
        Points = null;
        MonsterPoint = null;//怪物进入战斗后站位
        PlayerPoint = null;//玩家进入战斗后站位
        MapArray = null;//地图格子集合
        instance = null;
    }
}
