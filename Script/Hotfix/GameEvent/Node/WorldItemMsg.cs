using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 世界物品消息
/// </summary>
public class WorldItemMsg : ILMsgBase
{
    public ushort CallBack;//回调消息
    public List<ItemJson> ItemList;//获取多个物品数据

    /// <summary>
    /// 消息回调
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="CallBack"></param>
    public WorldItemMsg(ushort msgID, ushort CallBack)
    {
        this.msgID = msgID;
        this.CallBack = CallBack;
    }
    /// <summary>
    /// 获取所有物品
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="ItemList">所有奖励物品</param>
    public WorldItemMsg(ushort msgID, List<ItemJson> ItemList)
    {
        this.msgID = msgID;
        this.ItemList = ItemList;
    }
 
    /// <summary>
    /// 无参消息
    /// </summary>
    /// <param name="msgID"></param>
    public WorldItemMsg(ushort msgID)
    {
        this.msgID = msgID;
        
    }

}
/// <summary>
/// 世界物品回调获取数据
/// </summary>
public class WorldItemBackMsg : ILMsgBase
{


    public int[] ItemArray;//金币,木材,石材数量

    public List<ItemJson> allItem;
    /// <summary>
    /// 世界缓存的数据数量获取
    /// </summary>
    /// <param name="ItemArray">金币,木材,石材数量</param>
    /// <param name="msgID"></param>
    public WorldItemBackMsg(int[] ItemArray, ushort msgID)
    {
        this.ItemArray = ItemArray;
        this.msgID = msgID;
    }

    public WorldItemBackMsg(List<ItemJson> allItem, ushort msgID)
    {
        this.allItem = allItem;
        this.msgID = msgID;
    }
}
