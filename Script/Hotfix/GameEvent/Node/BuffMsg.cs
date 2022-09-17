using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMsg : ILMsgBase
{
    public int NpcID;
    public ushort backMsg;
    public Buff buff;//buff使用
    public int casterID;//施法者的ID
    public int time;//读档使用  buff剩余时间
    public int level;//buff等级
    /// <summary>
    /// buff效果类型
    /// </summary>
    public GameBuffType buffType;

    /// <summary>
    /// 效果对象
    /// </summary>
    public NPCBase NPCBase;
    /// <summary>
    /// 激活Buff效果
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="buffType">buff效果</param>
    /// <param name="NPCBase">需要激活的对象</param>
    public BuffMsg(ushort msgID, GameBuffType buffType, NPCBase NPCBase, int casterID, int level, int time = -1)
    {
        this.msgID = msgID;
        this.buffType = buffType;
        this.NPCBase = NPCBase;
        this.casterID = casterID;
        this.time = time;
        this.level = level;
    }
    /// <summary>
    /// Buff移除
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="buff"></param>
    /// <param name="NPCBase"></param>
    public BuffMsg(ushort msgID, Buff buff, NPCBase NPCBase)
    {
        this.msgID = msgID;
        this.buff = buff;
        this.NPCBase = NPCBase;
    }
    /// <summary>
    ///玩家回合更新Buff效果
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="isPlayerRefreshBuff">是玩家回合?</param>
    public BuffMsg(ushort msgID)
    {
        this.msgID = msgID;
    }
    /// <summary>
    /// 消息回调
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="backID"></param>
    public BuffMsg(ushort msgID, ushort backID)
    {
        this.msgID = msgID;
        this.backMsg = backID;
    }
    /// <summary>
    /// 移除buff
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="NpcID">角色id</param>
    public BuffMsg(ushort msgID, int NpcID)
    {
        this.msgID = msgID;
        this.NpcID = NpcID;
    }
}

public class BuffMsgBack : ILMsgBase
{
    public List<Buff> buffList;
    public BuffMsgBack(ushort msgID, List<Buff> buffList)
    {
        this.msgID = msgID;
        this.buffList = buffList;
    }
}
