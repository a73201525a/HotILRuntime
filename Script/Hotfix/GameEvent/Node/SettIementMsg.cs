using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 怪物死亡后结算
/// </summary>
public class SettlementMsg : ILMsgBase
{
    public int ConfigID;//怪物id
    public RoundType isMonsterRound;//怪物回合
    public int PlayerID;//玩家id
    public ushort callBack;//回调
    public int[] rewardList;//奖励物品

    /// <summary>
    /// 物品数据返回
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="rewardList">奖励物品</param>
    /// <param name="isMonsterRound"></param>
    /// <param name="playerID"></param>
    public SettlementMsg(ushort msgID, int[] rewardList, RoundType isMonsterRound,int playerID)
    {
        this.msgID = msgID;
        this.rewardList = rewardList;   
        this.isMonsterRound = isMonsterRound;
        this.PlayerID = playerID;
    }
}
/// <summary>
/// 关卡后结算
/// </summary>
public class AdventureSettlementMsg : ILMsgBase
{
    internal AdventureCfgTable adventureCfg;//当前关卡结算数据
    public bool AdventureWin;//是否胜利
    public ushort callBack;//回调
    /// 结算界面消息
    /// </summary>
    /// <param name="msgID"></param>
    public AdventureSettlementMsg(ushort msgID, ushort callBack)
    {
        this.msgID = msgID;
        this.callBack = callBack;
    }

    /// <summary>
    /// 奖励获取物品
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="AdventureWin">是否通关</param>
    /// <param name="adventureCfg">奖励的物品</param>
    internal AdventureSettlementMsg(ushort msgID, bool AdventureWin , AdventureCfgTable adventureCfg)
    {
        this.msgID = msgID;
        this.adventureCfg = adventureCfg;
        this.AdventureWin = AdventureWin;
      
    }
}
  
