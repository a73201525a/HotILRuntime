using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 怪物消息处理
/// </summary>
public class MonsterMsg : ILMsgBase
{
    public int buildIndex;//怪物归属
    public int level;//怪物等级
    public Vector3 creatPos;//怪物数量
    public Vector3 moveTo;//怪物数量

    public Bullet bullet;//击中的子弹
    public int monsterID;//怪物ID
    public int monsterIndex;//怪物索引
    public ushort CallBack;
    public Transform parent;
    public int SkillID;//技能id
    public Player player;//怪物攻击目标
  
    public GameObject monseterObj;//怪物
    public bool isPointerMonster;//点击怪物
    public SaveMapData[] SaveMapData;//存档数据
    /// <summary>
    /// 怪物存档数据
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="SaveMapData"></param>
    public MonsterMsg(ushort msgID, SaveMapData[] SaveMapData)
    {
        this.SaveMapData = SaveMapData;
        this.msgID = msgID;

    }
    /// <summary>
    /// 禁用怪物反键
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="isPointerMonster"></param>
    public MonsterMsg(ushort msgID, bool isPointerMonster)
    {
        this.isPointerMonster = isPointerMonster;
        this. msgID = msgID;

    }
    /// <summary>
    /// 怪物获取
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="monseterObj">怪物对象</param> 
    /// <param name="player">玩家</param>
    public MonsterMsg(ushort msgid, GameObject monseterObj, Player player)
    {
        this.msgID = msgid;
        this.monseterObj = monseterObj;
        this.player = player;
    }

    /// <summary>
    /// 怪物消息处理回调
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
    /// 怪物创建
    /// </summary>
    /// <param name="monsterID">怪物id</param>
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
    /// 传入归属索引
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
///怪物消息回调
public class MonsterMsgBack : ILMsgBase
{
    public PlayerJson playerJson;//玩家数据
    public GameObject monster;//怪物
    public List<MonsterBase> monsters;//怪物集合
    /// <summary>
    /// 获取玩家数据及怪物id
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
    /// 怪物集合
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="monsters"></param>
    public MonsterMsgBack(ushort msgID, List<MonsterBase> monsters)
    {
        this.msgID = msgID;
        this.monsters = monsters;

    }
}
