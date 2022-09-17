using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 冒险消息
/// </summary>
public class ILUIAdventureMsg : ILMsgBase
{
    public int[] playerIDArray;//进入冒险玩家id数组
    public int[] monsterIDArr;//进入冒险玩家id数组
    public int[] buildIDArr;//进入冒险玩家id数组 
    public int[] buildLevel;//进入冒险建筑等级
    public Vector2[] monsterPos;//进入关卡冒险怪物位置
    public Vector2[] buildPos;//进入关卡冒险建筑位置
    public Vector2[] playerPos;//进入关卡冒险玩家位置
    public int AdventureID;//关卡id
    public PlayerJson[] playerJsons;//玩家数据

    public string AdventureCondition;//关卡胜利条件
    public bool isRoundStart;//是否是新的回合
    public ushort CallBack;//回调
 
    /// <summary>
    /// 获取关卡id
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="AdventureID"></param>
    public ILUIAdventureMsg(ushort msgID, int AdventureID)
    {
        this.msgID = msgID;
        this.AdventureID = AdventureID;
    }
    /// <summary>
    /// 显示文本
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="AdventureCondition"></param>
    public ILUIAdventureMsg(ushort msgID, string AdventureCondition,bool isRoundStart)
    {
        this.msgID = msgID;
        this.AdventureCondition = AdventureCondition;
        this.isRoundStart = isRoundStart;
    }
    /// <summary>
    /// 无参消息
    /// </summary>
    /// <param name="msgID"></param>
    public ILUIAdventureMsg(ushort msgID)
    {
        this.msgID = msgID;
    }
    /// <summary>
    /// 进入冒险
    /// </summary>
    /// <param name="msgID"></param>
    public ILUIAdventureMsg(ushort msgID, PlayerJson[] playerJsons, int AdventureID)
    {
        this.msgID = msgID;
        this.playerJsons = playerJsons;
        this.AdventureID = AdventureID; 
    }

    /// <summary>
    ///  加载存档
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="playerIDArray">玩家id</param>
    /// <param name="monsterIDArr">怪物id</param>
    /// <param name="buildIDArr">建筑id</param>
    /// <param name="monsterPos">怪物位置</param>
    /// <param name="buildPos">建筑位置</param>
    /// <param name="playerPos">玩家位置</param>
    /// <param name="AdventureID">关卡id</param>
    public ILUIAdventureMsg(ushort msgID, int[] playerIDArray, int[] monsterIDArr, int[] buildIDArr,int[]buildLevel,Vector2[] monsterPos,Vector2 [] buildPos, Vector2[] playerPos, int AdventureID)
    {
        this.msgID = msgID;
        this.playerIDArray = playerIDArray;
        this.monsterIDArr = monsterIDArr;
        this.buildIDArr = buildIDArr;
        this.AdventureID = AdventureID; 
        this.monsterPos = monsterPos; 
        this.buildPos = buildPos;
        this.playerPos = playerPos;
        this.buildLevel = buildLevel;
    }


    public ILUIAdventureMsg(ushort msgID, ushort CallBack)
    {
        this.msgID = msgID;
        this.CallBack = CallBack;
    }
}
/// <summary>
///冒险场景消息
/// </summary>
public class AdventureMsg : ILMsgBase
{
    /// <summary>
    /// 闯关胜利条件
    /// </summary>
    public AdventureWinType winType; 
  
    /// <summary>
    /// 关卡条件胜利
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="winType"></param>
    public AdventureMsg(ushort msgID, AdventureWinType winType)
    {
        this.msgID = msgID;
        this.winType = winType;
    }
}
/// <summary>
/// 关卡回调
/// </summary>
public class ILUIAdventureMsgBack : ILMsgBase
{
    public int AdventureID;//关卡ID

    /// <summary>
    /// 获取关卡id
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="AdventureID"></param>
    public ILUIAdventureMsgBack(ushort msgID, int AdventureID)
    {
        this.msgID = msgID;
        this.AdventureID = AdventureID;
    }
 
}