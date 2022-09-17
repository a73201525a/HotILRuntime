using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ILNpcEvent
{
    LoadPlayer = ILManagerID.NpcManager + 1,
    //加载子弹
    LoadBullet,
    LoadBulletEf,


    CreartBullet,//创建子弹
    BulletInit,//子弹初始化

    UnLoadAllBullet,//卸载子弹
    UnLoadSceneBullet,//卸载小场景子弹

    OnFireAreaDownEvent,
    OnFireAreaMoveEvent,
    OnFireAreaUpEvent,
    LoadCircle,
    LoadCube,
    LoadSector60,
    LoadSector120,
    LoadEquipment,//加载装备
    LoadEquipmentBody,//加载装备配件
    LoadSingleEquipment,//加载单个装备
    LoadGunPart,//加载枪部件
    //移动控制
    CreatPlayer,
    PlayerInitEnd,//玩家加载结束
    PlayerMove,
    PlayerBenginMove,
    PlayerEndMove,
    PlayerIsStopMove,
    PlayerIsRunState,
    PlayerTurn,
    PlayerFireTurn,
    PlayerFire,
    RemoverMainPlayer,//进入战斗 删除所有main场景角色
    GetAllMainPlayer,//获取main场景的角色
    RemoverPlayerSingle,//删除单个玩家
    ChangeEquipment,//切换武器装备
    GetFireDownPointCube,//获取开火点
    GetFireDownPointCircle,
    GetFireDownPointBack,


    GetPlayerObjectBack,//获取玩家回调
    GetPlayerGameObjectBack,//怪物获取玩家回调

    GetPlayerDataBack,//获取玩家本地数据
    PlayerDataBack,//获取玩家Json数据回调
    PlayerEquipmentBack,//玩家获取所有穿戴物品
    PlayerGunDataBack,//枪数据
    UpdatePlayer,//更新玩家
    ShowPlayerUI,//显示玩家操作UI
    GetPlayerMaxRound,//得到玩家最大回合数
    //怪物
    LoadMonster,
    LoadMonserComb,//加载怪物组合
    CreatMonster,//创建怪物

    PointEntrer,//反键点击事件
    RemoverDeathMonster,//删除死亡的怪物
    GetALlMonster,//得到所有怪物
    GetALlMonsterAndDeath,//得到所有怪物(包括已经死了的)
    GetAllMonsterBack,//玩家获取所有怪物
    GetAllBuildBack,//怪物获取所有建筑
    LoadMainPlayer,//主界面玩家
    PlayerUseProp,//玩家使用道具 
    SkillBall,//玩家跳过弹球战斗

    PlayerUpdatePlayData,//玩家数据更新
    PlayerByAttack,//玩家被攻击
    PlayerBySkill,//玩家被技能攻击
    MonsterStartRound,//怪物回合继续或者开始
    MonsterByAttack,//怪物被攻击 
    MonsterCounterAttack,//怪物反击
    MonsterBySkill,//怪物被技能命中
    GetAllPlayer,//得到所有玩家
    GetALlPlayerAndDeath,//得到所有玩家(包括已经死了的)
    GetAllPlayerBack,//得到所有玩家回调
    GetPlayerAllBuildBack,//玩家得到所有建筑
    DestoryAllPlayer,//删除所有玩家


    GetPlayerByID,//由id得到玩家
    PlayerEnterFight,//玩家进入战斗
    GetClickPlayerRound,//得到玩家是否全部结束回合
    PlayerCounterAttack,//玩家反击
    UpdatePlayerOperation,//更新玩家操作步骤

    PlayerRoundStart,//玩家回合开始
    PlayerRoundEnd,//玩家当前操作结束
    PlayerRoundAllEnd,//玩家回合结束
    PlayerCancel,//取消玩家操作  
    PlayerUseSkillID,//玩家点击使用的技能id
    PlayerForcedToCancel,//强制取消

    SavePlayerjson,//战斗胜利保存玩家数据

    InitFitgt,//进入关卡，初始化
    GameOver,//游戏结束
    GetRound,//获取回合
    LoadGetRoundBack,//读档获取回合
    LoadSaveMapDataEnd,//读档完成设置读档数据
    maxValue,

}

public enum ILNpcEventMap
{
    CreatMap = ILNpcEvent.maxValue + 1,

    HideMapTip,//隐藏提示
               //  ShowSkillTip,//显示技能
    SetObs,//设置障碍
    HideAttckTip,//隐藏攻击提示
    ShowMapTip,//显示移动提示
    Rest,//重置
    GetMonsterTipHex,//得到怪物显示提示
    ShowAttackTip,//显示怪物攻击提示
    ShowAttackTargetTip,//显示寻找到AI的提示
    UpdateMap,//更新地型
    maxValue,
}

public enum ILNpcSKill
{
    LoadSkill = ILNpcEventMap.maxValue + 1,//加载技能
    CreatSkill,//使用技能
    LoadSingleSkill,//加载单个技能
    InitSkill,//初始化玩家所有技能
    maxValue,
}

public enum ILNpcBall
{
    LoadBallBack = ILNpcSKill.maxValue + 1,//加载弹球
    LoadWallBallBack,//加载墙体球
    EnterFight,//进入战斗
    GetBallItem,//获取弹球
    maxValue,
}

public enum ILNpcBuff
{
    ActiveBuff = ILNpcBall.maxValue + 1,//激活buff
    RemoveCounter,//解控技能
    RoundEnd,//回合结束
    RoundStart,//回合开始
    GetAllBuff,//获取所以buff
    GetDealtailAll,//获取所有buff详情
    NpcDeath,//Npc死亡
    LoadBuff,//加载buff
    maxValue,
}

public enum ILNpcSaveMap
{
    GetMonsterBack = ILNpcBuff.maxValue + 1,//获取怪物数据
    GetPlayerBack,//获取玩家数据
    GetBuildBack,//获取建筑数据
    GetBuffBack,//获取buff
    GetRewardItem,//获取已经获得的奖励物品
    GetAdventureIDBack,//获取当前关卡ID
    SetPlayerData,//设置玩家 数据
    SetMonsterData,//设置怪物 数据
    SetBuildData,//设置建筑 数据
    maxValue,
}

