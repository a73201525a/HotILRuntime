using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 世界物品数据类型
/// </summary>
public enum WorldItemType
{
    Gold, //金币
    Wood, //木材数量
    Stone, //石材数量
}

public class WorldItemController : AdventureBase
{

    public List<ItemJson> RewardItems;//关卡里面获取的道具 关卡胜利后 再保存到背包

    public void Awake()
    {
        msgIds = new ushort[]
       {
              (ushort)AdventureItemEvent.Init,//初始化物品数据
              (ushort)AdventureItemEvent.UpdateGoldItem,//添加物品数据 
              (ushort)AdventureItemEvent.SaveItem,//关卡胜利保存数据
              (ushort)AdventureItemEvent.AddItemList,//保存领取的奖励
              (ushort)AdventureItemEvent.GetItemArray,//获取物品
               (ushort)AdventureItemEvent.GetGoldArray,//得到金币 木材 石材数量
           

       };
        RegisteSelf(this, msgIds);
        Init();
    }

    public override void ProcessEvent(ILMsgBase tmpMsg)
    {

        switch (tmpMsg.msgID)
        {
            case (ushort)AdventureItemEvent.Init://初始化物品数据
                {
                    ItemMsgBack itemMsg = (ItemMsgBack)tmpMsg;
                    ItemJson Gold = new ItemJson(itemMsg.tmpItem.ConfigID, 0, itemMsg.tmpItem.Index, itemMsg.tmpItem.bodyConfigID);
                    ItemJson Wood = new ItemJson((int)RewardType.wood, 0, itemMsg.tmpItem.Index, itemMsg.tmpItem.bodyConfigID);
                    ItemJson Stone = new ItemJson((int)RewardType.stone, 0, itemMsg.tmpItem.Index, itemMsg.tmpItem.bodyConfigID);
                    RewardItems.Add(Gold);
                    RewardItems.Add(Wood);
                    RewardItems.Add(Stone);
                }
                break;
            case (ushort)AdventureItemEvent.GetGoldArray://获取金币,石材,木材,物品数量
                {
                    WorldItemMsg msg = (WorldItemMsg)tmpMsg;
                    WorldItemBackMsg worldBack = new WorldItemBackMsg(new int[] { RewardItems[0].Number, RewardItems[1].Number, RewardItems[2].Number }, msg.CallBack);
                    SendMsg(worldBack);
                }
                break;

            case (ushort)AdventureItemEvent.GetItemArray://获取所有
                {
                    WorldItemMsg msg = (WorldItemMsg)tmpMsg;
                    WorldItemBackMsg worldBack = new WorldItemBackMsg(RewardItems, msg.CallBack);
                    SendMsg(worldBack);
                }
                break;
            case (ushort)AdventureItemEvent.AddItemList://添加奖励物品
                {
                    WorldItemMsg msg = (WorldItemMsg)tmpMsg;
                    for (int i = 0; i < msg.ItemList.Count; i++)
                    {
                        if (msg.ItemList[i].ConfigID == (int)RewardType.gold
                            && msg.ItemList[i].ConfigID == (int)RewardType.wood
                            && msg.ItemList[i].ConfigID == (int)RewardType.stone)
                        {
                            continue;
                        }
                        for (int k = 0; k < RewardItems.Count; k++)
                        {
                            if (msg.ItemList[i].GetConfigBool(ExcelItmeType.is_pile))
                            {
                                if (RewardItems[k].ConfigID == msg.ItemList[i].ConfigID)
                                {
                                    int maxNumber = RewardItems[k].GetConfigInt(ExcelItmeType.MaxLimit);
                                    int residue = maxNumber - RewardItems[k].Number;

                                    if (RewardItems[k].Number== maxNumber)
                                    {
                                        continue;
                                    }
                                    if (msg.ItemList[i].Number <= residue)
                                    {
                                        RewardItems[k].Number += msg.ItemList[i].Number;
                                        msg.ItemList[i].Number = 0;
                                    }
                                    else
                                    {
                                        RewardItems[k].Number = maxNumber;
                                        msg.ItemList[i].Number -= residue;
                                        i--;
                                    }
                                }
                            }
                        }
                    }

                    for (int i = 0; i < msg.ItemList.Count; i++)
                    {
                        if (msg.ItemList[i].Number > 0)
                        {
                            RewardItems.Add(msg.ItemList[i]);
                        }

                    }
                }
                break;
            case (ushort)AdventureItemEvent.UpdateGoldItem://物品数据更新
                {
                    WorldItemMsg msg = (WorldItemMsg)tmpMsg;
                    for (int i = 0; i < msg.ItemList.Count; i++)
                    {
                        UpdatItem(msg.ItemList[i]);
                    }

                    WorldItemBackMsg worldBack = new WorldItemBackMsg(new int[] { RewardItems[0].Number, RewardItems[1].Number, RewardItems[2].Number }, (ushort)ILUIEvent.UpdateFightDataBack);
                    SendMsg(worldBack);
                }
                break;
            case (ushort)AdventureItemEvent.SaveItem://保存物品
                {

                    ItemMsg bagMsg = new ItemMsg(RewardItems, (ushort)ILUIEvent.AddItem);
                    SendMsg(bagMsg);
                }
                break;

        }
    }

    private void Init()
    {
        RewardItems = new List<ItemJson>();
        //初始化金币
        ItemMsg msg = new ItemMsg((ushort)ILUIEvent.GetGoldItem, (ushort)AdventureItemEvent.Init);
        SendMsg(msg);

    }

    /// <summary>
    /// 更新物品数据
    /// </summary>
    /// <param name="worldItem"></param>
    /// <param name="add">物品是添加,还是减少</param>
    private void UpdatItem(ItemJson itemJson)
    {

        if (itemJson.ConfigID == (int)RewardType.gold)//石材
        {
            RewardItems[0].Number += itemJson.Number;

        }
        else if (itemJson.ConfigID == (int)RewardType.wood)//木材
        {
            RewardItems[1].Number += itemJson.Number;
        }
        else if (itemJson.ConfigID == (int)RewardType.stone)
        {
            RewardItems[2].Number += itemJson.Number;
        }





    }

    public override void OnDestory()
    {
        base.OnDestory();

    }


}
