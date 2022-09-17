using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMsg : ILMsgBase
{
    /// <summary>
    /// 建筑id
    /// </summary>
    public int buildID; 
    /// <summary>
    /// 物品index
    /// </summary>
    public int itemIndex;

    /// <summary>
    /// 回调消息
    /// </summary>
    public ushort Callback;

    /// <summary>
    /// 位置索引
    /// </summary>
    public int IndexX;
    /// <summary>
    /// 位置索引
    /// </summary>
    public int IndexY;
   
    public bool prohibitionAttack;//禁令攻击 
    public bool prohibitionBuildAttack;//能否攻击建筑 
    public bool prohibitionSkill;//技能禁用
    public RoundType roundType;//是否是玩家攻击
    public bool PoniterEnter;//反键点击
    public int attackerID;//攻击者ID
   
    /// <summary>
    /// 当前玩家操作id
    /// </summary>
    public int curPlayerID;
    /// <summary>
    /// 需要拆除友方建筑物品
    /// </summary>
    public GameObject tmpGameObject;

    public SaveMapData[] saveMapDatas;//存档的数据
    /// <summary>
    /// 获取存档数据
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="saveMapDatas"></param>
    public BuildMsg(ushort msgID, SaveMapData[] saveMapDatas)
    {
        this.msgID = msgID;
        this.saveMapDatas = saveMapDatas;
    }
    /// <summary>
    /// 获取当前操作玩家id
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="curPlayerID"></param> 
    /// <param name="prohibitionAttack"></param>
    /// <param name="prohibitionBuildAttack"></param>   
    /// /// <param name="prohibitionSkill"></param>
    public BuildMsg(ushort msgID, int curPlayerID,bool prohibitionAttack,bool prohibitionBuildAttack,bool prohibitionSkill)
    {
        this.msgID = msgID;
        this.curPlayerID = curPlayerID;
        this.prohibitionAttack = prohibitionAttack; 
        this.prohibitionBuildAttack = prohibitionBuildAttack;
        this.prohibitionSkill = prohibitionSkill;
    }
    /// <summary>
    ///玩家创建友方建筑
    /// </summary> 
    /// <param name="buildID">建筑id</param>
    /// <param name="playerID">玩家id</param>
    /// <param name="IndexX">位置索引</param>
    /// <param name="IndexY ">位置索引/param>
    /// <param name="msgID"></param>
    public BuildMsg(int buildID,int playerID, int IndexX, int IndexY, ushort msgID)
    {
        this.msgID = msgID;
        this.curPlayerID = playerID;
        this.buildID = buildID;
        this.IndexY = IndexY;
        this.IndexX = IndexX;
    }
    /// <summary>
    /// 建筑被攻击
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="tmpGameObject">建筑物品</param>
    /// <param name="attackerID">攻击者id</param> 
    /// <param name="IsPlayerAttack">攻击者是不是玩家</param>
    public BuildMsg(ushort msgID,GameObject tmpGameObject,int attackerID, RoundType roundType)
    {
        this.msgID = msgID;
        this.tmpGameObject = tmpGameObject;
        this.roundType = roundType; 
        this.attackerID = attackerID;
    }
    /// <summary>
    /// 删除建筑
    /// </summary>
    /// <param name="tmpGameObject">建筑ID</param>
    /// <param name="msgID"></param>
    public BuildMsg(ushort msgID,GameObject tmpGameObject)
    {
        this.msgID = msgID;
        this.tmpGameObject = tmpGameObject;
       
    }
    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="itemIndex">物品索引</param>
    public BuildMsg(ushort msgID,int itemIndex)
    {
        this.msgID = msgID;
        this.itemIndex = itemIndex;
    } 
    /// <summary>
    /// 点击进入
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="PoniterEnter"></param>
    public BuildMsg(ushort msgID,bool PoniterEnter)
    {
        this.msgID = msgID;
        this.PoniterEnter = PoniterEnter;
    }
    /// <summary>
    /// 无参消息
    /// </summary>
    /// <param name="msgID"></param>
    public BuildMsg(ushort msgID)
    {
        this.msgID = msgID;
    } 
    /// <summary>
    /// 消息回调
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="CallBack"></param>
    public BuildMsg(ushort msgID,ushort CallBack)
    {
        this.msgID = msgID;
        this.Callback = CallBack;
    }


}
public class BuildMsgBack : ILMsgBase
{
    /// <summary>
    /// 建筑id
    /// </summary>
    public int buildID;
    /// <summary>
    /// 建筑集合
    /// </summary>
    public List<BuildItemBase> buildList;
    /// <summary>
    /// 无参回调消息
    /// </summary>
    /// <param name="msgID"></param>
    public BuildMsgBack(ushort msgID)
    {
        this.msgID = msgID;
    }
    /// <summary>
    /// 关闭当前UI
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="buildID"></param>
    public BuildMsgBack(ushort msgID, int buildID)
    {
        this.msgID = msgID;
        this.buildID = buildID;
    } 
    /// <summary>
    /// 获取所有建筑
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="buildID"></param>
    public BuildMsgBack(ushort msgID, List<BuildItemBase> buildList)
    {
        this.msgID = msgID;
        this.buildList = buildList;
    }
}