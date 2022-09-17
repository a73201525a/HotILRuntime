using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMsg : ILMsgBase
{
    public PlayerBase playerBase;//玩家
    public SkillOrBuildType SkillOrBuildType;//建筑升级or 技能升级
    public int SkillID;//技能ID
    public int[] SkillArray;//技能数组

    /// <summary>
    /// 根据ID创建技能
    /// </summary>
    /// <param name="SkillID"></param>
    public SkillMsg(int SkillID, Vector3 pos, ushort msgID)
    {
        this.SkillID = SkillID;
        this.msgID = msgID;
    }


    /// <summary>
    /// 初始化技能数组
    /// </summary>
    /// <param name="SkillArray"></param>
    /// <param name="msgID"></param>
    public SkillMsg(int[] SkillArray, ushort msgID)
    {
        this.SkillArray = SkillArray;
        this.msgID = msgID;
    }
    /// <summary>
    /// 技能Or建筑
    /// </summary>
    /// <param name="playerBase">玩家数据</param>
    /// <param name="SkillOrBuildType">技能or建筑升级</param>
    public SkillMsg(ushort msgId, PlayerBase playerBase, SkillOrBuildType SkillOrBuildType)
    {
        this.msgID = msgId;
        this.playerBase = playerBase;
        this.SkillOrBuildType = SkillOrBuildType;

    }

}


public class CreatSkillMsg : ILMsgBase
{
    public int SkillID;//技能id
    public int SkillLevel;//技能等级
    public NPCBase StartNpc;//施法者
    public NPCBase EndNpc;//受法术者
    public BuildItemBase Build;//建筑施法

    

    public SkillAttackObject StartType;//施法者
    public SkillAttackObject EndType;//被受伤者
    /// <summary>
    /// 创建技能
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="id">技能id</param>
    /// <param name="npcStart">施法者</param>
    /// <param name="npcEnd">受法术者</param>
    /// <param name="build">建筑施法</param>
    /// <param name="StartType">施法者</param>
    /// <param name="EndType">被受伤者</param>
    public CreatSkillMsg(ushort msgId, int id,int SkillLevel, NPCBase npcStart, NPCBase npcEnd, BuildItemBase build, SkillAttackObject StartType, SkillAttackObject EndType)
    {
        this.msgID = msgId;
        this.SkillID = id;
        this.SkillLevel = SkillLevel;
        this.StartNpc = npcStart;
        this.EndNpc = npcEnd;
        this.Build = build;
        this.StartType = StartType;
        this.EndType = EndType;


    }
}



/// <summary>
/// 使用技能(对使用对象的消息)
/// </summary>

public class SkillDataMsg : ILMsgBase
{
    public int NPCID;//攻击者ID
    public NPCBase endNpc;//被攻击者
    public SkillItem Skill;//使用技能的ID
    public RoundType roundType;//谁的回合施法
    /// <summary>
    /// 
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="playerID">玩家id</param>
    /// <param name="SkillID">使用技能的ID</param>
    /// <param name="roundType">谁的回合施法</param>
    public SkillDataMsg(ushort msgID, int NPCID, SkillItem Skill, NPCBase endNpc, RoundType roundType)
    {
        this.NPCID = NPCID;
        this.msgID = msgID;
        this.Skill = Skill;
        this.roundType = roundType;
        this.endNpc = endNpc;
    }
}