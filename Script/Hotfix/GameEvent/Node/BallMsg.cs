using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMsg : ILMsgBase
{
    public PlayerData playerData;//玩家数据
    public ushort CallBack;//回调事件
    /// <summary>
    /// 获取玩家数据
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="playerData"></param>
    public BallMsg(ushort msgID, PlayerData playerData)
    {
        this.msgID = msgID;
        this.playerData = playerData;
    }
    /// <summary>
    /// 消息回调
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="CallBack"></param>
    public BallMsg(ushort msgID, ushort CallBack)
    {
        this.msgID = msgID;
        this.CallBack = CallBack;
    }
}
