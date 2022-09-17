using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public enum AdventureEvent
{
    LoadAdventure = ILManagerID.AdventureManager + 1,//加载冒险
    LoadAdventurePath,//加载关卡路径
    LoadAdventureMonster,//加载关卡怪兽
    LoadAdventureBoss,//加载Boss
                      // LoadAdventureSpecial,//加载特殊关卡
    loadSpecialFurniture,//加载特殊家具
    WorldTimeChang,//时间改变
    GetPlayerObj,//得到玩家对象
    SelectAdventure,//选择关卡




    UpdateFightDataBack,//更新战斗数据回调

    SetFightMainInit,//初始化战斗主场景数据




    maxValue,
}

public enum AdventureBuildEvent
{
    LoadBuild = AdventureEvent.maxValue + 1,//加载建筑物品
    LoadBuildAll,//加载所有中立和敌对建筑
    CreateBuild,//创建建筑
    GetAllBuild,//得到所有建筑
    GetAllPlayer,//得到所有玩家
    GetAllMonster,//得到所有怪物
    DestoryBuild,//删除建筑
    RoundEnd,//建筑回合结束
    RoundStart,//建筑开始回合
    BuildByAttack,//建筑被攻击
    PointerEnter,//反键点击建筑 
    SetBuildData,// 设置建筑存档数据
    maxValue,
}

/// <summary>
/// 关卡物品
/// </summary>
public enum AdventureItemEvent
{
    SaveItem = AdventureBuildEvent.maxValue + 1,//保存物品
    Init,//初始化关卡世界数据
    AddItemList,//添加物品List
    GetGoldArray,//获取资源 和 金币
    GetItemArray,//获取所有物品数据
    UpdateGoldItem,//获取金币 石材 木材
  
    maxValue,
}





