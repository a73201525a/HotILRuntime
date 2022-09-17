using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 对应UI功能模块
/// </summary>
/// 新增UI模块需手动添加
public enum ILUIEventPanel
{
    LoadPanel = ILManagerID.UIManager + 1,

    MainScenePanel,
    TipPanel,//消息提示

    HUDPanel,//HUD页面
    DetailPanel,//显示详情页面
    TroopPanel,//出兵选择界面
    LoadingPanel,//加载页面
    AdventureMainPanel,//选择关卡UI
    RoundPanel,//回合提示界面
    HealthPanel,//健康界面
    BagPanel,//背包界面
    UseBagPanel,//UseBag
    ModifiedPanel,//改枪
    SettingPanel,//设置界面
    //EquipmentPanel,//

    FightMainPanel,//战斗主界面
    FightBagPanel,//战斗背包
    // FightEquipmentPanel,
    FightRewardPanel,//战斗奖励领取界面
    FightSettlementPanel,//战斗结算界面

    MapPanel,//地图界面
    BuildPanel,//建筑界面
    ArchivePanel,//存档界面
    SpoilsPanel,//战利品界面
    
    maxValue,

}

public class UIPanelPath
{
    public static string MainScenePanel = "scenestwo/ui/MainScenePanel";
    public static string LevelMainPanel = "scenestwo/ui/AdventurePanel";
    public static string StorageMainPanel = "scenestwo/ui/StoragePanel";
    public static string HealthPanel = "scenestwo/ui/HealthPanel";

    public static string FightMainPanel = "scenestwo/ui/FightMainPanel";
    public static string FightBagPanel = "scenestwo/ui/FightBagPanel";
    public static string FightEquipmentPanel = "scenestwo/ui/FightEquipmentPanel";
    public static string FightSettlementPanel = "scenestwo/ui/FightSettlementPanel";
    public static string ModifiedPanel = "scenestwo/ui/ModifiedPanel";
    public static string LoadingPanel = "scenestwo/ui/LoadingPanel";
    public static string BagPanel = "scenestwo/ui/BagPanel";
    public static string UsePanel = "scenestwo/ui/UseBagPanel";
    public static string BuildPanel = "scenestwo/ui/BuildPanel";
    public static string TipPanel = "scenestwo/ui/TipPanel";
    public static string HUDPanel = "scenestwo/ui/HUDPanel";
    public static string DetailPanel = "scenestwo/ui/DetailPanel"; 
    public static string TroopPanel = "scenestwo/ui/TroopPanel"; 
    public static string FightRewardPanel = "scenestwo/ui/FightRewardPanel";  
    public static string SettingPanel = "scenestwo/ui/SettingPanel"; 
    public static string RoundPanel = "scenestwo/ui/RoundPanel";
    public static string ArchivePanel = "scenestwo/ui/ArchivePanel"; 
    public static string SpoilsPanel = "scenestwo/ui/SpoilsPanel"; 
   

}
public enum ILUIEvent
{
    LoadHero = ILUIEventPanel.maxValue + 1,
    CardFinish,
    CardClick,
    TipGridFinish,
    ItmeGridFinish,
    UpdateSelectPanel,
    ShowDragItem,
    HideDragItem,
    RaodFinish,
    SetObjectParent,
    GetMonsterMovePath,

    LoadMainScene,//Mian场景 
    LoadSaveMainScene,//加载Mian场景存档

    CreatDetailBuildItem,//创建建筑详情
    CreatDetailMosnterItem,//创建怪物属性详情
    CreatDetailPlayerItem,//创建玩家属性详情
    RemoveDetailItem,//移除掉详情Item
    DetailBuff,//显示buff

    LoadingSceneStart,//Lodaing界面
    SetLoadingProgress,//设置进度条进度
    LoadingComplete,//加载完成


    LoadAllUseSprite,//加载所有使用界面图片
    LoadGunPart,//加载使用枪部件
    UsedItem,//使用物品
    UpdateEquipmentItemData,//更新页面数据
    UsedFightItem,//使用战斗背包数据
    RemoveFightItem,//移除战斗数据


    LoadFightSettlementItem,//加载结算物品
    LoadFightSettlementIcon,//结算物品图标
    GetFightNewAllItemBack,//得到战斗所有物品数据回调

    PlayerLoadEndBack,//玩家加载完成

    SaveItemMap,//存档
    LoadItemMap,//读档

    AddItem,//添加物品
    UpdateItemAll,//更新背包数据
    UpdatePlayerData,//更新玩家数据
    UpdateFightDataBack,//更新战斗数据
   // UpdateWoodDataBack,//木材更新分发给所有需要更新木材的位置
   // UpdateStoneData,//更新石材数据
    UpdateVolume,//更新音量
 //   UpdateStoneDataBack,//石材更新分发给所有需要更新石材的位置
    UpdateSetting,//更新系统设置
    SaveItem,//保存物品数据

    GetPlayerData,//得到玩家数据
    GetAllPlayerJson,//得到所有玩家数据
    AddPlayerJson,//添加玩家数据
    GetItemAll,//获取所有道具
    GetGoldItem,//获取金币
    //GetWoodItem,//获取木材
    //GetStoneItem,//获取石材
    GetItemByType,//获取所有相应类型物品
    GetItemByUniqueIDArray,//根据唯一id数组,获取相应部分物品
    GetItemByUniqueID,//根据唯一id获取相应物品数据
    GetSettingData,//获取设置数据
    GetSettingDataBack,//获取设置数据回调
    GetMainSettingData,//获取主界面数据

    InitGunModified,//设置强化的枪械

    LoadModifeGun,//加载基础强化物品
    LoadModifedIcon,//加载图片
    MosdifedItemData,//强化装备数据
    LoadMosdifedItem,//打开开使用界面
    ModifiedGunJson,//GunJson数据
    LoadOneMosdifedModel,//加载一个强化物品模型



    LoadFightMainSprite,//加载战斗主界面图集

    LoadHealthPulsSprite,//加载健康血量图集

  
    PlayerHealthInit,//玩家健康界面初始数据获取
    PlayerHealthBack,//玩家健康界面获取数据
    ShowHealthItem,//显示升级材料
    PlayerRoundBack,//玩家战斗回合是否结束
    FightMainItemBack,//玩家战斗界面数据
    PlayerFightEquimentBack,//玩家装备获取玩家属性

    FightMainInit,//初始化战斗主界面
    FightMainInitWoodBack,//初始化木材数据 
    FightMainInitStoneBack,//初始化石材数据
    FightMainUpdateItem,//更新数据Item
    FightMianProhibitionRound,//禁用回合
    ShowRoundNumber,//显示回合数



    LoadHealthSprite,//技能升级,建筑升级图集加载
    LoadMapTexture,//地图图片
    GetMapPlayer,//地图跟随的玩家
    MapInit,//地图初始
    PlayerBack,
    PlayerMainPanelCreatBack,//主界面创建玩家完成消息回调
    //tip消息
    ShowTipText,//显示消息文本

    //HUDPanel消息
    CreatHUDBlood,//创建血条
    RemoveHUDBlood,//移除
    ReduceBlood,//HUD扣血
    AddBlood,//HUD加血
    HideHUDBlood,//隐藏血条
    ShowHUDMpTip,//显示HUD 蓝量文本
    HideHUDTip,//隐藏HUD提示文本
    ShowHUDTip,//显示HUD提示文本
    ShowHUDBlood,//显示HUD所有血条
    ShowHUDFightDamage,//显示战斗伤害文本
    ShowHUDDynamicTip,//显示动态文字
    maxValue,
}



public enum ILUILevel
{
    Dwon = 0,
    Two,
    Three,
    Four,
    Five,
    Six,
    Top,
}
/// <summary>
/// 加载消息回调
/// </summary>
public class LoadingMsg : ILMsgBase
{
    public ushort callBack;//加载回调
    public string loadingName;//加载场景名字
    public Action endAction;//加载完成执行
    public float curValue;//当前进度
    public float maxValue;//最大进度
    /// <summary>
    /// 设置当前进度
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="curValue"></param>
    public LoadingMsg(ushort msgid, float curValue, float maxValue = 1f)
    {
        this.msgID = msgid;
        this.curValue = curValue;
        this.maxValue = maxValue;
    }
    /// <summary>
    /// 初始化进度
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="loadingValue"></param>
    public LoadingMsg(ushort msgid, string loadingName, Action endAction)
    {

        this.loadingName = loadingName;
        this.msgID = msgid;
        this.endAction = endAction;
    }
    /// <summary>
    /// 进度加载回调
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="callBack"></param>
    public LoadingMsg(ushort msgID, ushort callBack)
    {
        this.msgID = msgID;
        this.callBack = callBack;
    }
}

public class ItemMsg : ILMsgBase
{
    public ChangeItem changeItemType;//改变类型
    public ItemJson item;//物品
    public List<ItemJson> itemList;//物品
    public int index;//索引
    public int newIndex;//新索引
    public int Type;//物品类型
    public ushort CallBack;
    public int[] UniqueIDArray;//唯一id数组
    public int UniqueID;//物品唯一表示

    public string SavePath;//存档路径
    public ItemMsg(ushort msgid, string SavePath)
    {
        this.msgID = msgid;
        this.SavePath = SavePath;

    }

    /// <summary>
    /// 修改物品数据
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="type"></param>
    /// <param name="item"></param>
    /// <param name="index">空格子索引</param>
    public ItemMsg(ushort msgid, ChangeItem type, ItemJson item, int index)
    {
        this.msgID = msgid;
        this.item = item;
        this.index = index;
        this.changeItemType = type;
    }

    /// <summary>
    /// 删除Item
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="type"></param>
    /// <param name="UniqueID"></param>
    public ItemMsg(ushort msgid, ChangeItem type, int UniqueID)
    {
        this.msgID = msgid;
        this.UniqueID = UniqueID;
        this.changeItemType = type;

    }
    /// <summary>
    /// 背包添加单个消息
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="item"></param>
    public ItemMsg(ushort msgid, ItemJson item, ChangeItem type)
    {
        this.msgID = msgid;
        this.item = item;
        this.changeItemType = type;
    }
    /// <summary>
    /// 根据唯一id数组获取物品数据
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="UniqueID"></param>
    /// <param name="CallBack"></param>
    public ItemMsg(ushort msgid, int[] UniqueIDArray, ushort CallBack)
    {
        this.msgID = (ushort)msgid;
        this.UniqueIDArray = UniqueIDArray;
        this.CallBack = CallBack;
    }
    /// <summary>
    /// 得到所有数据
    /// </summary>
    /// <param name="items"></param>
    /// <param name="msgid"></param>
    public ItemMsg(ushort msgid, ushort CallBack)
    {
        this.msgID = msgid;
        this.CallBack = CallBack;
    }
    /// <summary>
    ///根据唯一id获取物品
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="UniqueID"></param>
    /// <param name="CallBack"></param>
    public ItemMsg(ushort msgid,int UniqueID,  ushort CallBack)
    {
        this.msgID = msgid;
        this.UniqueID = UniqueID;
        this.CallBack = CallBack;
    }

    public ItemMsg(List<ItemJson> itemJsons,  ushort msgid)
    {
        this.msgID = msgid;
        this.itemList = itemJsons;
    }

}

public class ItemMsgBack : ILMsgBase
{
    public List<ItemJson> ItemData;//物品数据

    public ItemJson tmpItem;//单个数据
    /// <summary>
    /// 请求同类型物品数据
    /// </summary>
    public ItemMsgBack(List<ItemJson> ItemData, ushort CallBack)
    {
        this.msgID = CallBack;
        this.ItemData = ItemData;
    }

    /// <summary>
    /// 请求单个物品
    /// </summary>
    /// <param name="tmpItem"></param>
    /// <param name="CallBack"></param>
    public ItemMsgBack(ItemJson tmpItem, ushort CallBack)
    {
        this.msgID = CallBack;
        this.tmpItem = tmpItem;
    }
}
public class ModifiedMsg : ILMsgBase
{
    public ItemJson item;//物品数据
    public ushort CallBack;
    /// <summary>
    /// 得到改枪部位数据
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="item">枪物品</param>
    public ModifiedMsg(ushort msgid, ItemJson item)
    {
        this.msgID = msgid;
        this.item = item;
    }
}

/// <summary>
/// 战斗主界面消息
/// </summary>
public class FightMainMsg : ILMsgBase
{
    public ItemJson GunItem;//主界面物品

    public ushort CallBack;//CallBack
    public int currentItemIndex;//当前物品索引
    public bool prohibitionRound;//禁止回合
    public int[] PlayerID;//玩家id
    public int Exp;//经验
    public int Gold;//金币
    public int RoundNumber;//回合数
    /// <summary>
    /// 更新回合数
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="RoundNumber"></param>
    public FightMainMsg(ushort msgID, int RoundNumber)
    {
        this.msgID = msgID;
        this.RoundNumber = RoundNumber;
    }
    /// <summary>
    /// 回调
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="currentItemIndex"></param>
    public FightMainMsg(ushort msgID, ushort CallBack)
    {
        this.msgID = msgID;
        this.CallBack = CallBack;

    } 
    /// <summary>
    /// 禁用按钮
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="prohibitionRound"></param>
    public FightMainMsg(ushort msgID, bool prohibitionRound)
    {
        this.msgID = msgID;
        this.prohibitionRound = prohibitionRound;

    }


}
public class FightMainMsgBack : ILMsgBase
{
    public int[] PlayerID;//玩家id
    /// <summary>
    /// 返回玩家id
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="PlayerID">玩家id</param>
    public FightMainMsgBack(ushort msgID, int[] PlayerID)
    {
        this.PlayerID = PlayerID;
        this.msgID = msgID;
    }
}
public class MainMsg:ILMsgBase
{
   
    public ushort CallBack;//回调
    /// <summary>
    /// 消息回调
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="CallBack"></param>
    public MainMsg(ushort msgID, ushort CallBack)
    {
        this.msgID = msgID;
        this.CallBack = CallBack;

    }
}
public class MainMsgBack:ILMsgBase
{
    public int[] playerUniqueID;//玩家唯一id
    public List<MainScenePlayer> PlayerList;//主界面所有玩家
    /// <summary>
    /// 获取主界面玩家
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="playerUniqueID">玩家唯一id</param>
    public MainMsgBack(ushort msgID, List<MainScenePlayer> PlayerList)
    {
        this.msgID = msgID;
        this.PlayerList = PlayerList;

    }
}

/// <summary>
/// 健康消息
/// </summary>
public class HealthMsg : ILMsgBase
{
    public PlayerBase  Player;//玩家
    public ItemJson  ItemJson;//物品
  
    /// <summary>
    /// 健康获取玩家id
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="PlayerID">玩家id</param>
    public HealthMsg(ushort msgID, PlayerBase Player)
    {
        this.msgID = msgID;
        this.Player = Player;
    } 
    /// <summary>
    /// 获取物品
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="ItemJson"></param>
     public HealthMsg(ushort msgID, ItemJson ItemJson)
    {
        this.msgID = msgID;
        this.ItemJson = ItemJson;
    } 
    
}

public class SettingMsg:ILMsgBase
{
    public SetingJson setingJson;//设置数据 
    public ushort CallBack;//回调
    /// <summary>
    /// 更新设置数据
    /// </summary>
    /// <param name="setingJson"></param>
    /// <param name="msgID"></param>
    public SettingMsg(SetingJson setingJson,ushort msgID)
    {
        this.setingJson = setingJson;
        this.msgID = msgID;
    }
    /// <summary>
    /// 获取系统设置数据
    /// </summary>
    /// <param name="setingJson"></param>
    /// <param name="msgID"></param>
    /// <param name="CallBack"></param>
    public SettingMsg(ushort msgID, ushort CallBack)
    {
        this.msgID = msgID; 
        this.CallBack = CallBack;
    }
}
public class SettingBackMsg:ILMsgBase
{
    public SetingJson setingJson;//设置数据
    /// <summary>
    /// 更新设置数据
    /// </summary>
    /// <param name="setingJson"></param>
    /// <param name="msgID"></param>
    public SettingBackMsg(SetingJson setingJson,ushort msgID)
    {
        this.setingJson = setingJson;
        this.msgID = msgID;
    }
}


