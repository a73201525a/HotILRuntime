using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPointPathManager
{
    //���·�߼���
    public Vector3[] Points;//·������


    public Transform[] MonsterPoint;//�������ս����վλ
    public Transform[] PlayerPoint;//��ҽ���ս����վλ
    public MapHex[] MapArray;//��ͼ���Ӽ���

    private static FightPointPathManager instance;//����
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
        SetMonsterAndPlayerFightPoint();//Ĭ�ϳ���
    }

    //����Ѱ·��
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
    /// ��ȡ���͸���
    /// </summary>
    public void GetMapHexGrid()
    {
        GameObject tmp = GameObject.Find("MapData");
        MapArray = new MapHex[tmp.transform.childCount];
        MapArray = tmp.GetComponentsInChildren<MapHex>();
        // Debug.Log("MapArray:" + MapArray.Length);
    }

    /// <summary>
    /// ���ù�������ս����λ
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
        MonsterPoint = null;//�������ս����վλ
        PlayerPoint = null;//��ҽ���ս����վλ
        MapArray = null;//��ͼ���Ӽ���
        instance = null;
    }
}
