using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ϣ����
/// </summary>
public class MonsterMsg : ILMsgBase
{
    public int buildIndex;//�������
    public int level;//����ȼ�
    public Vector3 creatPos;//��������
    public Vector3 moveTo;//��������

    public Bullet bullet;//���е��ӵ�
    public int monsterID;//����ID
    public int monsterIndex;//��������
    public ushort CallBack;
    public Transform parent;
    public int SkillID;//����id
    public Player player;//���﹥��Ŀ��
  
    public GameObject monseterObj;//����
    public bool isPointerMonster;//�������
    public SaveMapData[] SaveMapData;//�浵����
    /// <summary>
    /// ����浵����
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="SaveMapData"></param>
    public MonsterMsg(ushort msgID, SaveMapData[] SaveMapData)
    {
        this.SaveMapData = SaveMapData;
        this.msgID = msgID;

    }
    /// <summary>
    /// ���ù��ﷴ��
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="isPointerMonster"></param>
    public MonsterMsg(ushort msgID, bool isPointerMonster)
    {
        this.isPointerMonster = isPointerMonster;
        this. msgID = msgID;

    }
    /// <summary>
    /// �����ȡ
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="monseterObj">�������</param> 
    /// <param name="player">���</param>
    public MonsterMsg(ushort msgid, GameObject monseterObj, Player player)
    {
        this.msgID = msgid;
        this.monseterObj = monseterObj;
        this.player = player;
    }

    /// <summary>
    /// ������Ϣ����ص�
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="CallBack"></param>
    public MonsterMsg(ushort msgid, ushort CallBack)
    {
        this.msgID = msgid;
        this.CallBack = CallBack;
    }
    public MonsterMsg(ushort msgid)
    {
        this.msgID = msgid;
       
    }
    /// <summary>
    /// ���ﴴ��
    /// </summary>
    /// <param name="monsterID">����id</param>
    public MonsterMsg(ushort msgid, int monsterID, int buildIndex, Transform parent, Vector3 creatPos, Vector3 moveTo)
    {
        this.msgID = msgid;
        this.buildIndex = buildIndex;
        this.creatPos = creatPos;
        this.moveTo = moveTo;
        this.monsterID = monsterID;
        this.parent = parent;
    }
    /// <summary>
    /// �����������
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="monsterID"></param>
    public MonsterMsg(ushort msgid, int buildIndex, Player player)
    {
        this.msgID = msgid;
        this.buildIndex = buildIndex;
        this.player = player;
    }
}
///������Ϣ�ص�
public class MonsterMsgBack : ILMsgBase
{
    public PlayerJson playerJson;//�������
    public GameObject monster;//����
    public List<MonsterBase> monsters;//���Ｏ��
    /// <summary>
    /// ��ȡ������ݼ�����id
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="monsterID"></param>
    /// <param name="playerJson"></param>
    public MonsterMsgBack(ushort msgID, GameObject monster, PlayerJson playerJson)
    {
        this.msgID = msgID;
        this.playerJson = playerJson;
        this.monster = monster;
    }
    /// <summary>
    /// ���Ｏ��
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="monsters"></param>
    public MonsterMsgBack(ushort msgID, List<MonsterBase> monsters)
    {
        this.msgID = msgID;
        this.monsters = monsters;

    }
}
