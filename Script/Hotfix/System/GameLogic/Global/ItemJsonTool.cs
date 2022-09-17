using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemJsonTool
{
    int ConfigID;
    public ItemJsonTool(int ConfigID)
    {
        this.ConfigID = ConfigID;
    }

    #region string
    internal string GetString(EquipmentCfgTable table, ExcelItmeType type)
    {

        switch (type)
        {
            case ExcelItmeType.icon:
                return table.data[ConfigID].icon_name;
            case ExcelItmeType.itemName:
                return table.data[ConfigID].item_name;

            case ExcelItmeType.bigIcon:
                return table.data[ConfigID].big_icon;
            //case ExcelItmeType.description:
            //    return table.data[ConfigID].description;

            default:
                Debug.Log("没有该数据：" + type);
                break;
        }
        return "";
    }

    internal string GetString(EquipmentPartCfgTable table, ExcelItmeType type)
    {

        switch (type)
        {
            case ExcelItmeType.icon:
                return table.data[ConfigID].icon_name;
            case ExcelItmeType.itemName:
                return table.data[ConfigID].item_name;

            case ExcelItmeType.bigIcon:
                return table.data[ConfigID].big_icon;
            //case ExcelItmeType.description:
            //    return table.data[ConfigID].description;

            default:
                Debug.Log("没有该数据：" + type);
                break;
        }
        return "";
    }


    internal string GetString(BallCfgTable table, ExcelItmeType type)
    {

        switch (type)
        {
            case ExcelItmeType.icon:
                // Debug.Log(table.data[ConfigID].icon_name);
                return table.data[ConfigID].icon_name;
            case ExcelItmeType.bigIcon:
                return null;
         

            default:
                Debug.Log("没有该数据：" + type);
                break;
        }
        return null;
    }

    internal string GetString(ItemCfgTable table, ExcelItmeType type)
    {
        switch (type)
        {
            case ExcelItmeType.icon:
                return table.data[ConfigID].icon_name;
            case ExcelItmeType.itemName:
                return table.data[ConfigID].item_name;

            case ExcelItmeType.bigIcon:
                return table.data[ConfigID].big_icon;
            //case ExcelItmeType.description:
               // return table.data[ConfigID].description;

            default:
                Debug.Log("没有该数据：" + type);
                break;
        }
        return "";
    }
    #endregion

    #region float



    #endregion
    #region int
    internal int GetInt(EquipmentCfgTable table, ExcelItmeType type)
    {
        switch (type)
        {
            case ExcelItmeType.item_rarity:
                return table.data[ConfigID].item_rarity; 
            case ExcelItmeType.id:
                return table.data[ConfigID].id;
            case ExcelItmeType.itemWeaponType:
                return table.data[ConfigID].weapon_type;
            case ExcelItmeType.description:
                return table.data[ConfigID].description;

            case ExcelItmeType.attack:
                return table.data[ConfigID].attack;
            case ExcelItmeType.attackSpeed:
                return table.data[ConfigID].attackSpeed;
            case ExcelItmeType.vertigo:
                return table.data[ConfigID].vertigo;
            case ExcelItmeType.speedCut:
                return table.data[ConfigID].speedCut;
            case ExcelItmeType.repel:
                return table.data[ConfigID].repel;
            case ExcelItmeType.crit:
                return table.data[ConfigID].crit;
            case ExcelItmeType.defense:
                return table.data[ConfigID].defense;
            case ExcelItmeType.hp:
                return table.data[ConfigID].hp;
            case ExcelItmeType.speed:
                return table.data[ConfigID].speed;
            case ExcelItmeType.turnSpeed:
                return table.data[ConfigID].turnSpeed;
            case ExcelItmeType.accSpeed:
                return table.data[ConfigID].accSpeed;
            case ExcelItmeType.sprintSpeed:
                return table.data[ConfigID].sprintSpeed;
            case ExcelItmeType.item_Priority:
                return table.data[ConfigID].item_priority; 
            case ExcelItmeType.itemName:
                return table.data[ConfigID].name;
            default:
                Debug.Log("没有该数据：：" + type);
                break;
        }
        return -1;
    }

    internal int GetInt(ItemCfgTable table, ExcelItmeType type)
    {
        switch (type)
        {
            case ExcelItmeType.itemType:
                return table.data[ConfigID].item_type; 
            case ExcelItmeType.repair_hp:
                return table.data[ConfigID].repair_hp;
            case ExcelItmeType.item_rarity:
                return table.data[ConfigID].item_rarity;
            case ExcelItmeType.id:
                return table.data[ConfigID].id;
          
            case ExcelItmeType.item_Priority:
                return table.data[ConfigID].item_priority;
            case ExcelItmeType.MaxLimit:
                return table.data[ConfigID].maxLimit;
            case ExcelItmeType.description:
                return table.data[ConfigID].description; 
            case ExcelItmeType.itemName:
                return table.data[ConfigID].name;
            case ExcelItmeType.ballType:
            case ExcelItmeType.attack:
            case ExcelItmeType.attackSpeed:
            case ExcelItmeType.vertigo:
            case ExcelItmeType.speedCut:
            case ExcelItmeType.repel:
            case ExcelItmeType.crit:
            case ExcelItmeType.defense:
            case ExcelItmeType.hp:
            case ExcelItmeType.speed:
            case ExcelItmeType.turnSpeed:
            case ExcelItmeType.accSpeed:
            case ExcelItmeType.sprintSpeed:
                return -1;
            default:
                 Debug.Log("没有该数据：：" + type);
                break;
        }
        return -1;
    }

    internal int GetInt(BallCfgTable table, ExcelItmeType type)
    {
        switch (type)
        {
            case ExcelItmeType.itemType:
                return table.data[ConfigID].item_type;
            case ExcelItmeType.id:
                return table.data[ConfigID].id;
            case ExcelItmeType.attack:
                return table.data[ConfigID].Attack;
            case ExcelItmeType.ballType:
                return table.data[ConfigID].ballType;
            case ExcelItmeType.item_Priority:
                return table.data[ConfigID].item_priority;
            case ExcelItmeType.description:
                return table.data[ConfigID].item_name; 
            case ExcelItmeType.itemName:
                return table.data[ConfigID].item_name;
            case ExcelItmeType.MaxLimit:
            case ExcelItmeType.attackSpeed:
            case ExcelItmeType.vertigo:
            case ExcelItmeType.speedCut:
            case ExcelItmeType.repel:
            case ExcelItmeType.crit:
            case ExcelItmeType.defense:
            case ExcelItmeType.hp:
            case ExcelItmeType.speed:
            case ExcelItmeType.turnSpeed:
            case ExcelItmeType.accSpeed:
            case ExcelItmeType.sprintSpeed:
                return -1;
                return -1;
            default:
                Debug.Log("没有该数据：：" + type);
                break;
        }
        return -1;
    }

    internal int GetInt(EquipmentPartCfgTable table, ExcelItmeType type)
    {
        switch (type)
        {
            case ExcelItmeType.reinforce_Index:
                return table.data[ConfigID].reinforce_Index;//强化类型
             case ExcelItmeType.reinforceByEquipmentID:
                return table.data[ConfigID].equipment_ID;//
            case ExcelItmeType.item_rarity:
                return table.data[ConfigID].item_rarity;
            case ExcelItmeType.id:
                return table.data[ConfigID].id;
            case ExcelItmeType.description:
                return table.data[ConfigID].description;
            //case ExcelItmeType.itemNum:
            //    return table.data[ConfigID].item_count;
            case ExcelItmeType.item_Priority:
                return table.data[ConfigID].item_priority; 
            case ExcelItmeType.itemName:
                return table.data[ConfigID].name;
            case ExcelItmeType.ballType:
            case ExcelItmeType.attack:
            case ExcelItmeType.attackSpeed:
            case ExcelItmeType.vertigo:
            case ExcelItmeType.speedCut:
            case ExcelItmeType.repel:
            case ExcelItmeType.crit:
            case ExcelItmeType.defense:
            case ExcelItmeType.hp:
            case ExcelItmeType.speed:
            case ExcelItmeType.turnSpeed:
            case ExcelItmeType.accSpeed:
            case ExcelItmeType.sprintSpeed: 
            case ExcelItmeType.elementAttack:
            case ExcelItmeType.elementDefence:
                return -1;
            default:
                Debug.Log("没有该数据：：" + type);
                break;
        }
        return -1;
    }

    #endregion

    #region Int[]
    internal int[] GetIntArray(EquipmentCfgTable table, ExcelItmeType type)
    {

        switch (type)
        {
            case ExcelItmeType.elementAttack:
                return table.data[ConfigID].elementAttack;
            case ExcelItmeType.elementDefence:
                return table.data[ConfigID].elementDefence;
            case ExcelItmeType.ballType:
                Debug.Log("=================table.data[ConfigID].bulletData==========================" + table.data[ConfigID].bulletData[0]);
                return table.data[ConfigID].bulletData;
            default:
                Debug.Log("没有该数据：：" + type);
                break;
        }
        return null;
    }
    internal int[] GetIntArray(ItemCfgTable table, ExcelItmeType type)
    {
        switch (type)
        {
            case ExcelItmeType.elementDefence:
            case ExcelItmeType.elementAttack:
                return null;
            default:
                Debug.Log("没有该数据：：" + type);
                break;
        }
        return null;
    }
    internal int[] GetIntArray(BallCfgTable table, ExcelItmeType type)
    {
        switch (type)
        {

            default:
                // Debug.Log("没有该数据：：" + type);
                break;
        }
        return null;
    }
    internal int[] GetIntArray(EquipmentPartCfgTable table, ExcelItmeType type)
    {
        switch (type)
        {
            case ExcelItmeType.elementAttack:
            case ExcelItmeType.elementDefence:
                return null;
            default:
                Debug.Log("没有该数据：：" + type);
                break;
        }
        return null;
    }
    #endregion
    #region bool
    internal bool GetBool(EquipmentPartCfgTable table, ExcelItmeType type)
    {
        switch (type)
        {
          
            case ExcelItmeType.is_equip:
                return table.data[ConfigID].is_equip;
            case ExcelItmeType.is_pile:
                return table.data[ConfigID].is_pile;
            case ExcelItmeType.is_ball:
                return false;
            default:
                Debug.Log("没有该类型" + type);
                break;
        }
        return false;
    }
    internal bool GetBool(EquipmentCfgTable table, ExcelItmeType type)
    {
        switch (type)
        {
            //case ExcelItmeType.is_bulletproof:
            //    return table.data[ConfigID].is_bulletproof;
            case ExcelItmeType.is_equip:
                return table.data[ConfigID].is_equip;
            case ExcelItmeType.is_pile:
                return table.data[ConfigID].is_pile;
            case ExcelItmeType.is_bagProp:
                return table.data[ConfigID].is_bagProp;
            case ExcelItmeType.is_ball:
                return table.data[ConfigID].is_ball;
            default:
                Debug.Log("没有该类型" + type);
                break;
        }
        return false;
    }
    internal bool GetBool(BallCfgTable table, ExcelItmeType type)
    {
        switch (type)
        {
            //case ExcelItmeType.is_bulletproof:
            //    return table.data[ConfigID].is_bulletproof;
            case ExcelItmeType.is_equip:
                return false;
            case ExcelItmeType.is_pile:
                return table.data[ConfigID].is_pile;
            case ExcelItmeType.is_ball:
                return table.data[ConfigID].is_ball; 
            default:
                Debug.Log("没有该类型" + type);
                break;
        }
        return false;
    }
    internal bool GetBool(ItemCfgTable table, ExcelItmeType type)
    {
        switch (type)
        {
            //case ExcelItmeType.is_bulletproof:
            //    return table.data[ConfigID].is_bulletproof;
            case ExcelItmeType.is_equip:
                return table.data[ConfigID].is_equip;
            case ExcelItmeType.is_pile:
                return table.data[ConfigID].is_pile;
            case ExcelItmeType.is_bagProp:
               return table.data[ConfigID].is_bagProp; 
            case ExcelItmeType.is_ball:
               return false;
            default:
                Debug.Log("没有该类型" + type);
                break;
        }
        return false;
    }
    #endregion

    #region String[]
    internal string[] GetStringArr(EquipmentCfgTable table, ExcelItmeType type)
    {
        switch (type)
        {
            default:
                Debug.Log("没有该类型" + type);
                break;
        }
        return null;
    }
    #endregion
}
