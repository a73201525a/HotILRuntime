using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ILUIEventBag
{

    GetItemAll = ILUIEvent.maxValue + 1,//获取服务端道具数据
    GetFightItemAll,//战斗获取的物品
    GetFightItemType,//战斗某种类型的物品
    LoadSpriteAll,//加载图集所有图片
    GetGoldDataItemBack,//得到金币数据,
    GetSettingDataItemBack,//得到当前玩家数据
    LoadUseWeapon,//加载使用界面武器显示
    LoadFightUseWeapon,//加载使用界面武器显示

    UpdateEquipment,//更新装备

    UpdateEquipmentByPlayerJson,//更新玩家装备  
    UpdateBagByPlayerJson,//更新玩家背包 
    UpdateBallByPlayerJson,//更新玩家球囊
    RemoveBagItem,//移除Bag物品数据
   // AddBagItem,//增加背包数据
    RemoveFightBagItem,//移除Bag物品数据

    SaveFightItem,//战斗物品保存
    AddFightWorldItem,//添加战斗拾取物品
    GetFightWorldALLItem,//获取战斗世界所有物品
    UpdateFightBulletData,//更新子弹
    GetFightMaterialDataByID,//通过id获取材料item
    UpdateFightItemData,//更新缓存数据
    GetFightPlayerID,//得到当前战斗玩家id
                     // GetMianPlayerID,//得到当前主场景玩家id
    GetPlayerBack,//获取玩家
 
    maxValue,
}

/// <summary>
/// 建筑
/// </summary>
public enum ILUIEventBuild
{

    LoadAllSprite = ILUIEventBag.maxValue + 1,//加载所有图片
    LoadItemSprte,//加载道具Item图集
 
    CloseItem,//关闭 item

    GetCurPlayerBtnAttack,//当前操作玩家攻击状态
    GetPlayer,//当前操作玩家
    ShowSkill,//显示UI技能
    ShowProp,//显示UI道具 
    Showbuild,//显示UI建筑物品
    UpdatewPropItem,//更新道具物品
    SkillFinishBack,//技能释放完成回调
    BuildFinishBack,//建造完成
    SkillUpdateCDTime,//技能更新CD计时
    UpdateBagByPlayerJson,//得到玩家道具
    maxValue,
}
/// <summary>
/// 当前战斗结束怪物奖励结算
/// </summary>
public enum ILUIEventCurrentFightEndRound
{
    LoadAllSprite = ILUIEventBuild.maxValue + 1,//加载所有图片
    LoadItemSprite,//加载item物品
    ShowRewardItem,//显示奖励物品
    FightEnd,
    maxValue,
}
/// <summary>
/// 设置界面消息事件
/// </summary>
public enum ILUIEventSetting
{
    LoadAllSprite = ILUIEventCurrentFightEndRound.maxValue + 1,//加载所有图片
    
    GetSettingJsonBack,//获取系统设置数据
    maxValue,
}
/// <summary>
///结算
/// </summary>
public enum ILUIEventSettlement
{
    LoadSettlementAllSprite = ILUIEventSetting.maxValue+1,//加载所有图片
   
    SettlementItem,//获取结算物品  
    SettlementSuccess,//战斗成功结束
    GetPlayerBack,//获取角色数据
    GetRewardItemBack,//获取战利品
    maxValue,
}
/// <summary>
/// 战斗时,属性消息
/// </summary>
public enum ILUIEventPlayerPanel
{
    LoadAllSprite = ILUIEventSettlement.maxValue + 1,//加载所有图片

    UpdateItem,//获取玩家携带球的物品  
    UpdataPlaerJson,//更新玩家数据
    HidePlayerData,//隐藏玩家数据显示
    HideMonsterCommon,//隐藏怪物通用属性
    ShowMonsterDetail,//显示怪物当前细节
    HideBuildCommon,//隐藏建筑通用属性
    ShowBuildDetail,//建筑细节属性显示
    HideBuildDetail,//隐藏建筑细节属性
    maxValue,
}
//冒险
public enum ILUIAdventureEvent
{
    LoadAdventure = ILUIEventPlayerPanel.maxValue+1,//加载关卡
    ReadFileAdventure,//读取关卡
    AdventureEnd,//结束关卡
    ShowWinCondition,//显示胜利条件 
    ShowRoundText,//显示回合数
    AdventureAngin,//关卡再来一次
    GetAdventureID,//获取当前关卡  
    GetPlayerBallItem,//获取当前关卡所有玩家携带的球
    AdventureWin,//关卡胜利
    GetPlayerJsonBack,//获取玩家数据
    maxValue,
}
/// <summary>
/// 选择出征部队人物界面
/// </summary>
public enum ILUIEventTroop
{
    LoadAllSprite = ILUIAdventureEvent.maxValue+1,//加载所有图片
    GetAllPlayerJsonBack,//得到所有玩家数据
    GetAdventureID,//得到关卡id
    GetMainPlayerBack,//加载场景所有玩家
    LoadModel,//加载模型
    maxValue,
}
/// <summary>
/// 存档
/// </summary>
public enum ILUIEventArchive
{
    LoadAllSprite = ILUIEventTroop.maxValue+1,//加载所有图片
    SaveArchiveShow,//存档消息
    maxValue,
}
//战利品
public enum ILUIEventSpoils
{
    LoadAllSprite= ILUIEventArchive.maxValue+1,//加载图集
    GetSpoilsItemJsonBack,//获取战利品数据
    maxValue,
}
/// <summary>
/// 技能升级
/// </summary>
public enum ILUIEventSKill
{
    LoadAllSprite= ILUIEventArchive.maxValue+1,//加载图集
    GetSkillItem,//获取玩家技能数据
    maxValue,
}