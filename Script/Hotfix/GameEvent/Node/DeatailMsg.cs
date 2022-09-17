using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeatailMsg : ILMsgBase
{
    public string connet;//详情内容
    public MonsterBase monsterData;//怪物数据
    public Player player;//玩家数据
    public BuildItemBase buildItem;//建筑物品
    public List<Buff> buffList;//buffList
    /// <summary>
    /// 创建详情内容
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="connet"></param>
    public DeatailMsg(ushort msgID, string connet)
    {
        this.msgID = msgID;
        this.connet = connet;
    } 
    /// <summary>
    /// 显示怪物数据
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="monsterData"></param>
    public DeatailMsg(ushort msgID, MonsterBase monsterData)
    {
        this.msgID = msgID;
        this.monsterData = monsterData;
    } 
    /// <summary>
    /// 玩家数据显示
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="player"></param>
    public DeatailMsg(ushort msgID, Player player)
    {
        this.msgID = msgID;
        this.player = player;
    } 
    /// <summary>
    /// 建筑物品数据
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="buildItem">建筑物品</param>
    public DeatailMsg(ushort msgID, BuildItemBase buildItem)
    {
        this.msgID = msgID;
        this.buildItem = buildItem;
    } 
  
    /// <summary>
    /// 移除所有详情
    /// </summary>
    /// <param name="msgID"></param>
    public DeatailMsg(ushort msgID)
    {
        this.msgID = msgID;
    }
    /// <summary>
    /// 显示触发buff数据
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="buffList"></param>

    public DeatailMsg(ushort msgID,List<Buff> buffList)
    {
        this.msgID = msgID;
        this.buffList = buffList;
      
    }
}

