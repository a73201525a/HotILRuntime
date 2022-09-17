using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// bag消息
/// </summary>
public class BagMsg:ILMsgBase 
{
    public List<ItemJson> ItemList;//物品数据
    /// <summary>
    /// 物品
    /// </summary>
    public ItemJson item;
    /// <summary>
    /// 移除bag数据
    /// </summary>
    /// <param name="item">物品数据</param>
    /// <param name="msgID"></param>
    public BagMsg(ItemJson item,ushort msgID)
    {
        this.msgID = msgID;
        this.item = item;
    }

    /// <summary>
    ///新增物品数据
    /// </summary>
    /// <param name="ItemList">新增的物品数据</param>
    /// <param name="msgID"></param>
    public BagMsg(List<ItemJson> ItemList, ushort msgID)
    {
        this.msgID = msgID;
        this.ItemList = ItemList;
    }
}
/// Bag使用级界面
/// </summary>
public class UsePanelMsg : ILMsgBase
{
    public Grid useGrid;//使用的格子
    /// <summary>
    /// 进入的格子
    /// </summary>
    public int enterIndex;
    /// <summary>
    /// 当前格子
    /// </summary>
    public int curindex;
    /// <summary>
    /// 背包使用界面
    /// </summary>
    /// <param name="useGrid"></param>
    /// <param name="msgID"></param>
    public UsePanelMsg(Grid useGrid, ushort msgID)
    {
        this.useGrid = useGrid;
        this.msgID = msgID;
    }
    /// <summary>
    ///  使用装备物品
    /// </summary>
    /// <param name="enterIndex">进入装备格子</param>
    /// <param name="curindex">点击使用物品格子索引</param>
    /// <param name="msgID"></param>
    public UsePanelMsg(int enterIndex, int curindex, ushort msgID)
    {
        this.enterIndex = enterIndex;
        this.curindex = curindex;
        this.msgID = msgID;
    }
}
public class FightBagMsg : ILMsgBase
{
    //拾取物品的ID
    public int ItemID;
    //物品的数量
    public int ItemCount;
    public ushort CallBack;//回调
    public int bulletType;//子弹类型
    public int itemType;//物品类型
    public ItemJson item;//单个物品
    /// <summary>
    /// 拾取物品
    /// </summary>
    /// <param name="ItemID">物品id</param>
    /// <param name="ItemCount">物品数量</param>
    /// <param name="msgID"></param>
    public FightBagMsg(int ItemID, int ItemCount, ushort msgID)
    {
        this.ItemID = ItemID;
        this.ItemCount = ItemCount;
        this.msgID = msgID;
    } 
    public FightBagMsg(int ItemID,ushort msgID,ushort CallBack)
    {
        this.ItemID = ItemID;
        this.CallBack = CallBack;
        this.msgID = msgID;
    }
    
    /// <summary>
    /// 背包数据回调
    /// </summary>
    /// <param name="CallBack"></param>
    /// <param name="msgID"></param>
    public FightBagMsg(ushort CallBack, ushort msgID)
    {
        this.CallBack = CallBack;
        this.msgID = msgID;
    } 
    /// <summary>
    /// 单个物品数据处理
    /// </summary>
    /// <param name="item">单个物品数据</param>
    /// <param name="msgID"></param>
    public FightBagMsg(ItemJson item, ushort msgID)
    {
        this.item = item;
        this.msgID = msgID;
    }
    /// <summary>
    /// 得到同种类型物品
    /// </summary>
    /// <param name="CallBack"></param>
    /// <param name="itemType"></param>
    /// <param name="msgID"></param>
    public FightBagMsg(ushort CallBack,int itemType, ushort msgID)
    {
        this.itemType = itemType;
        this.CallBack = CallBack;
        this.msgID = msgID;
    }
}
public class FightBagMsgBack : ILMsgBase
{
    public List<ItemJson> items;//获取物品缓存数据
    public ItemJson itemJson;//获取单个物品缓存数据
    /// <summary>
    /// 获取Item缓存数据
    /// </summary>
    /// <param name="items">item数据</param>
    /// <param name="msgID">消息id</param>
    public FightBagMsgBack(List<ItemJson> items, ushort msgID)
    {
        this.items = items;
        this.msgID = msgID;
    }
    /// <summary>
    /// 获取单个背包数据
    /// </summary>
    /// <param name="itemJson"></param>
    /// <param name="msgID"></param>
    public FightBagMsgBack(ItemJson itemJson, ushort msgID)
    {
        this.itemJson = itemJson;
        this.msgID = msgID;
    }
}
