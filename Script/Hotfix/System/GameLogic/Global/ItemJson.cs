using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// 物品类型
/// </summary>
public enum ItemType
{

    Bullet,//子弹
    Medkit,//医疗包
    Material,//采集材料
}



public enum ExcelItmeType
{
    icon = 1,//图标
    itemType = 2,//物品类型
    itemName = 3,//物品名字
    itemNum,//物品数量
    item_Priority,//物品优先级
                  // magazineNum,//弹夹子弹容量

    itemWeaponType,//装备类型
    MaxLimit,//最大堆叠数
    ballType,//球类型
    path,//物品路径
    bigIcon,//大图标
    description,//物品描述
    is_bagProp,// 是否是背包道具
    is_ball,// 是否是球
    is_pile,// 物品能否叠加

    is_equip,// 该物品能否穿戴
    part_type,//部件类型
    id,//配置id
    item_rarity,//物品稀有度
    reinforce_Index,//强化孔索引
    reinforceByEquipmentID,//强化物品的归属

    /// <summary>
    ///属性
    /// </summary>
    repair_hp,//回复量
    attackSpeed,//攻速
    attack,//攻击
    vertigo,//眩晕
    speedCut,//减速
    repel,//击退
    crit,//暴击

    defense,//防御
    hp,//血量
    speed,//移速
    turnSpeed,//转身速度
    accSpeed,//加速度
    sprintSpeed,//冲刺速度
    elementAttack,//属性攻击
    elementDefence,//属性防御
}

public enum ExcelType
{
    item = 1,//物品配置
    equipment = 2,//装备物品配置
    ball = 3,//球物品配置
    gunPart,//枪配件配置
}
public class ItemJson
{
    /// <summary>
    /// 物品唯一ID
    /// </summary>
    public int UniqueID;

    /// <summary>
    /// 物品配置表ID
    /// </summary>
    public int ConfigID;

    /// <summary>
    /// 物品拥有数量
    /// </summary>
    public int Number;

    /// <summary>
    /// 物品仓库位置
    /// </summary>
    public int Index;
    /// <summary>
    /// 装备数据
    /// </summary>
    // public EquipmentJson equipmentJson;


    /// <summary>
    /// 配置表类型
    /// </summary>
    public ExcelType excleType;

    /// <summary>
    /// 物品装备了那些强化部件
    /// </summary>
    public int[] bodyConfigID;


    private ItemJsonTool _tool;
    public ItemJsonTool tool
    {
        get
        {
            if (_tool == null)
            {
                _tool = new ItemJsonTool(ConfigID);
            }
            return _tool;
        }
    }
    public ItemJson(int ConfigID, int Number, int Index, int[] bodyConfigID)
    {
        this.UniqueID = CreatUniqueID();
        this.ConfigID = ConfigID;
        this.Number = Number;
        this.Index = Index;
        this.bodyConfigID = bodyConfigID;
        //  Debug.Log("===============" + excleType);
        int excleType = ConfigID / 10000;

        this.excleType = (ExcelType)excleType;

        // equipmentJson = new EquipmentJson(0, new int[(int)PlayerSubtractBloodState.LegRBlood], new int[(int)PlayerSubtractBloodState.LegRBlood]);
    }
    /// <summary>
    /// 装备物品
    /// </summary>
    /// <returns></returns>
    internal EquipmentCfgTable GetEquipmentConfig()
    {
        return EquipmentCfgTable.Instance.data[ConfigID];
    }
 
 
    /// <summary>
    /// 物品
    /// </summary>
    /// <returns></returns>
    internal ItemCfgTable GetItemConfig()
    {
        return ItemCfgTable.Instance.data[ConfigID];
    }
    /// <summary>
    /// 球
    /// </summary>
    /// <returns></returns>
    internal BallCfgTable GetBallConfig()
    {
        return BallCfgTable.Instance.data[ConfigID];
    }
    public int CreatUniqueID()
    {
        int tmpUnique = PlayerPrefs.GetInt("UniqueID", 0);
        tmpUnique++;
        PlayerPrefs.SetInt("UniqueID", tmpUnique);

        Debug.Log("tmpUnique:::" + tmpUnique);
        return tmpUnique;
    }


    public string GetConfigString(ExcelItmeType type)
    {
        //Debug.Log("type:::::" + type);
        // Debug.Log("excleType:::::" + excleType);
        string tmp = "";
        switch (excleType)
        {
            case ExcelType.item:
                tmp = tool.GetString(ItemCfgTable.Instance, type);
                break;
            case ExcelType.equipment:
                tmp = tool.GetString(EquipmentCfgTable.Instance, type);
                break;
            case ExcelType.ball:
                tmp = tool.GetString(BallCfgTable.Instance, type);
                break;
            case ExcelType.gunPart:
                tmp = tool.GetString(EquipmentPartCfgTable.Instance, type);
                break;
        }
        return tmp;
    }


    public int GetConfigInt(ExcelItmeType type)
    {

        int tmp = 0;
        switch (excleType)
        {
            case ExcelType.item:
                //Debug.Log("ItemCfgTable:::" + type);
                tmp = tool.GetInt(ItemCfgTable.Instance, type);
                break;
            case ExcelType.equipment:
                //Debug.Log("EquipmentCfgTable:::" + type);
                tmp = tool.GetInt(EquipmentCfgTable.Instance, type);
                break;
            case ExcelType.ball:
                //Debug.Log("bulletCfgTable:::" + type);
                tmp = tool.GetInt(BallCfgTable.Instance, type);
                break;
            case ExcelType.gunPart:
                //Debug.Log("GunPartCfgTable:::" + type);
                tmp = tool.GetInt(EquipmentPartCfgTable.Instance, type);
                break;

            default:
                Debug.Log("没有该类型：：" + excleType);
                break;
        }
        return tmp;
    }
    public int[] GetConfigIntArray(ExcelItmeType type)
    {

        switch (excleType)
        {
            case ExcelType.item:
                return tool.GetIntArray(ItemCfgTable.Instance, type);
            case ExcelType.equipment:
                return tool.GetIntArray(EquipmentCfgTable.Instance, type);
            case ExcelType.ball:
                return tool.GetIntArray(BallCfgTable.Instance, type);
            case ExcelType.gunPart:
                return tool.GetIntArray(EquipmentPartCfgTable.Instance, type);

            default:
                Debug.Log("没有该类型：：" + excleType);
                break;
        }
        return null;
    }
    public bool GetConfigBool(ExcelItmeType type)
    {
        bool tmp = false;
        switch (excleType)
        {
            case ExcelType.item:
                tmp = tool.GetBool(ItemCfgTable.Instance, type);
                break;
            case ExcelType.equipment:
                tmp = tool.GetBool(EquipmentCfgTable.Instance, type);
                break;
            case ExcelType.ball:
                tmp = tool.GetBool(BallCfgTable.Instance, type);
                break;
            case ExcelType.gunPart:
                tmp = tool.GetBool(EquipmentPartCfgTable.Instance, type);
                break;
        }
        return tmp;
    }


    public void Save(BinaryWriter writer)
    {
        writer.Write(ConfigID);
        writer.Write(Number);
        writer.Write((int)excleType);
    }
}
