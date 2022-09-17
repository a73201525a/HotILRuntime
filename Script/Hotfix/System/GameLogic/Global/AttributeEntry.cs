using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 属性词条
/// </summary>
public class AttributeEntry
{
    /// <summary>
    ///怪物属性
    /// </summary>
    /// <param name="monsterData"></param>
    /// <returns></returns>
    public static string MonsterAttribute(MonsterData monsterData)
    {
        string data = $"{Utility.GetLanguage(10227)}:{monsterData.monsterCfg.level}\n";
        data += $"{Utility.GetLanguage(10104)}:{monsterData.CurHp}/{monsterData.MaxHp}\n";
        data += $"{"技能等级"}:{monsterData.SkillLevel}\n";
        if (monsterData.attackData.AddAttack > 1)
        {
            int tmpAttack = Mathf.RoundToInt(monsterData.attackData.Attack * (monsterData.attackData.AddAttack-1));
            data += $"\n{Utility.GetLanguage(10098)}:{monsterData.attackData.Attack}+{tmpAttack}";
        }
        else if (monsterData.attackData.AddAttack < 1)
        {
            int tmpAttack = Mathf.RoundToInt(monsterData.attackData.Attack * (1-monsterData.attackData.AddAttack));
            data += $"\n{Utility.GetLanguage(10098)}:{monsterData.attackData.Attack}-{tmpAttack}";
        }
        else
        {
            data += $"\n{Utility.GetLanguage(10098)}:{monsterData.attackData.Attack}";
        }
        if (monsterData.attackData.AddDefence>1)
        {
            int tmpDef = Mathf.RoundToInt(monsterData.attackData.Attack * (monsterData.attackData.AddAttack - 1));
            data += $"{Utility.GetLanguage(10103)}:{monsterData.Defence}+{tmpDef}\n";
        }
        else if (monsterData.attackData.AddDefence <1)
        {
            int tmpDef = Mathf.RoundToInt(monsterData.attackData.Attack * (1-monsterData.attackData.AddAttack));
            data += $"{Utility.GetLanguage(10103)}:{monsterData.Defence}-{tmpDef}\n";
        }
        else
        {
            data += $"{Utility.GetLanguage(10103)}:{monsterData.Defence}\n";
        }
       


        data += $"{Utility.GetLanguage(10115)}:{monsterData.attackRange}\n";


        //for (int i = 0; i < monsterData.attackData.elementAttack.Length; i++)
        //{
        //    if (monsterData.attackData.elementAttack[i] > 0)
        //    {
        //        if (i == 0)
        //        {
        //            data += $"{Utility.GetLanguage(10109)}:{ monsterData.attackData.elementAttack[i]}\n";

        //        }
        //        else if (i == 1)
        //        {
        //            data += $"{Utility.GetLanguage(10110)}:{monsterData.attackData.elementAttack[i]}\n";
        //            ;
        //        }
        //        else if (i == 2)
        //        {
        //            data += $"{Utility.GetLanguage(10111)}:{monsterData.attackData.elementAttack[i]}\n";

        //        }
        //        else if (i == 3)
        //        {
        //            data += $"{Utility.GetLanguage(10112)}:{monsterData.attackData.elementAttack[i]}\n";

        //        }
        //        else if (i == 4)
        //        {
        //            data += $"{Utility.GetLanguage(10113)}:{monsterData.attackData.elementAttack[i]}\n";

        //        }
        //    }
        //}
        for (int i = 0; i < monsterData.ElementDefence.Length; i++)
        {
            if (monsterData.ElementDefence[i] > 0)
            {
                if (i == 0)
                {
                    data += $"{Utility.GetLanguage(10109)}{Utility.GetLanguage(10103)}:{monsterData.ElementDefence[i]}\n";

                }
                else if (i == 1)
                {
                    data += $"{Utility.GetLanguage(10110)}{Utility.GetLanguage(10103)}:{monsterData.ElementDefence[i]}\n";

                }
                else if (i == 2)
                {
                    data += $"{Utility.GetLanguage(10111)}{Utility.GetLanguage(10103)}:{monsterData.ElementDefence[i]}\n";

                }
                else if (i == 3)
                {
                    data += $"{Utility.GetLanguage(10112)}{Utility.GetLanguage(10103)}:{monsterData.ElementDefence[i]}\n";

                }
                else if (i == 4)
                {
                    data += $"{Utility.GetLanguage(10112)}{Utility.GetLanguage(10103)}:{monsterData.ElementDefence[i]}\n";

                }
            }
        }
        return data;
    }
    /// <summary>
    ///玩家属性数据词条
    /// </summary>
    public static string PlayerAttribute(PlayerData PlayerData, bool isMain = false)
    {
        PlayerJson playerJson = PlayerData.playerJson;
        string tmpItem = $"{Utility.GetLanguage(10227)}:{playerJson.level}";
        int levelID = PlayerData.playerJson.experienceLevel + PlayerData.playerJson.level;
        //升级所需经验
        PlayerLevelCfgTable levelCfgTable = PlayerLevelCfgTable.Instance.Get(levelID);
        tmpItem += $"\n{Utility.GetLanguage(10179)}:{playerJson.experience}/{levelCfgTable.exp}";
        if (playerJson.curHp > 0) tmpItem += $"\n{Utility.GetLanguage(10104)}:{playerJson.curHp}/{playerJson.maxHp}";
        if (!isMain)
        {
            int tmpAttack = Mathf.RoundToInt(playerJson.attack *Mathf.Abs(1-PlayerData.attackData.AddAttack));
            int tmpdef = Mathf.RoundToInt(playerJson.defense * Mathf.Abs(1- PlayerData.attackData.AddDefence));
          
            if (PlayerData.attackData.AddAttack > 1)
            {
                tmpItem += $"\n{Utility.GetLanguage(10098)}:{playerJson.attack}+{tmpAttack}";
            }
            else if (PlayerData.attackData.AddAttack < 1)
            {
                tmpItem += $"\n{Utility.GetLanguage(10098)}:{playerJson.attack}-{tmpAttack}";
            }
            else
            {
                tmpItem += $"\n{Utility.GetLanguage(10098)}:{playerJson.attack}";
            }

            if (PlayerData.attackData.AddDefence > 1)
            {
              
                tmpItem += $"\n{Utility.GetLanguage(10103)}:{playerJson.defense}+{tmpdef}";
            }
            else if (PlayerData.attackData.AddDefence < 1)
            {
                tmpItem += $"\n{Utility.GetLanguage(10103)}:{playerJson.defense}-{tmpdef}";
            }
            else
            {
                tmpItem += $"\n{Utility.GetLanguage(10103)}:{playerJson.defense}";
            }
          
        }
        else
        {
            if (playerJson.attack > 0) tmpItem += $"\n{Utility.GetLanguage(10098)}:{playerJson.attack}";

            if (playerJson.defense > 0) tmpItem += $"\n{Utility.GetLanguage(10103)}:{playerJson.defense}";
            if (playerJson.curMp > 0) tmpItem += $"\n{Utility.GetLanguage(10205)}:{playerJson.curMp}";
            if (playerJson.elementAttack != null) //元素攻击:风 | 雷 | 冰 | 火 | 草
            {
                int[] elementAttack = playerJson.elementAttack;
                for (int i = 0; i < elementAttack.Length; i++)
                {
                    if (elementAttack[i] > 0)
                    {
                        if (i == 0)
                        {
                            tmpItem += $"\n{Utility.GetLanguage(10109)}:{elementAttack[i]}";
                        }
                        else if (i == 1)
                        {
                            tmpItem += $"{Utility.GetLanguage(10110)}:{elementAttack[i]}";

                        }
                        else if (i == 2)
                        {
                            tmpItem += $"{Utility.GetLanguage(10111)}:{elementAttack[i]}";

                        }
                        else if (i == 3)
                        {
                            tmpItem += $"{Utility.GetLanguage(10112)}:{elementAttack[i]}";

                        }
                        else if (i == 4)
                        {
                            tmpItem += $"{Utility.GetLanguage(10113)}:{elementAttack[i]}";

                        }
                    }

                }

            }
            if (playerJson.elementDefense != null) //元素防御:风 | 雷 | 冰 | 火 | 草
            {
                int[] elementDefence = playerJson.elementDefense;
                for (int i = 0; i < elementDefence.Length; i++)
                {
                    if (elementDefence[i] > 0)
                    {
                        if (i == 0)
                        {
                            tmpItem += $"\n{Utility.GetLanguage(10109)}{Utility.GetLanguage(10103)}:{elementDefence[i]}";

                        }
                        else if (i == 1)
                        {
                            tmpItem += $"{Utility.GetLanguage(10110)}{Utility.GetLanguage(10103)}:{elementDefence[i]}";

                        }
                        else if (i == 2)
                        {
                            tmpItem += $"{Utility.GetLanguage(10111)}{Utility.GetLanguage(10103)}:{elementDefence[i]}";

                        }
                        else if (i == 3)
                        {
                            tmpItem += $"{Utility.GetLanguage(10112)}{Utility.GetLanguage(10103)}:{elementDefence[i]}";

                        }
                        else if (i == 4)
                        {
                            tmpItem += $"{Utility.GetLanguage(10113)}{Utility.GetLanguage(10103)}:{elementDefence[i]}";

                        }
                    }

                }
            }

        }


        return tmpItem;
    }

    /// <summary>
    /// 建筑属性
    /// </summary>
    /// <param name="buildItem"></param>
    /// <returns></returns>
    public static string BuildItemAttribute(BuildItemBase buildItem)
    {
        string data = $"{Utility.GetLanguage(10098)}:{buildItem.GetConfig().range}";
        data += $"\n{Utility.GetLanguage(10104)}:{buildItem.Hp}";
        data += $"\n {"等级"}:{buildItem.Level}";
        switch (buildItem.buildAttType)
        {
            case BuildAttType.neutral:
                data += $"\n{Utility.GetLanguage(10208)}";
                break;
            case BuildAttType.monster:
                data += $"\n{Utility.GetLanguage(10190)}";
                break;
            case BuildAttType.player:
                data += $"\n{Utility.GetLanguage(10188)}";
                break;
            default:
                break;
        }
        return data;
    }
    /// <summary>
    /// 物品属性
    /// </summary>
    /// <param name="tmp"></param>
    /// <returns></returns>
    public static List<string> ItemAttribute(ItemJson tmp)
    {
        List<string> str = new List<string>();
        string tmpItem = $"{Utility.GetLanguage(10213)}:{Utility.GetLanguage(tmp.GetConfigInt(ExcelItmeType.itemName))}";
        str.Add(tmpItem);
        if (tmp.GetConfigInt(ExcelItmeType.attack) > 0)
        {
            tmpItem = $"{Utility.GetLanguage(10098)}:{tmp.GetConfigInt(ExcelItmeType.attack)}";
            str.Add(tmpItem);
        }
        
        if (tmp.GetConfigInt(ExcelItmeType.vertigo) > 0)
        {
            tmpItem = $"{Utility.GetLanguage(10099)}:{tmp.GetConfigInt(ExcelItmeType.vertigo)}{Utility.GetLanguage(10175)}";
            str.Add(tmpItem);
        }
        if (tmp.GetConfigInt(ExcelItmeType.hp) > 0)
        {
            tmpItem = $"{Utility.GetLanguage(10104)}:{tmp.GetConfigInt(ExcelItmeType.hp)}";
            str.Add(tmpItem);
        }
        if (tmp.GetConfigInt(ExcelItmeType.turnSpeed) > 0)
        {
            tmpItem = $"{Utility.GetLanguage(10106)}:{tmp.GetConfigInt(ExcelItmeType.turnSpeed)}";
            str.Add(tmpItem);
        }
        if (tmp.GetConfigInt(ExcelItmeType.sprintSpeed) > 0)
        {
            tmpItem = $"{Utility.GetLanguage(10108)}:{tmp.GetConfigInt(ExcelItmeType.sprintSpeed)}";
            str.Add(tmpItem);
        }
        if (tmp.GetConfigInt(ExcelItmeType.speedCut) > 0)
        {
            tmpItem = $"{Utility.GetLanguage(10100)}:{tmp.GetConfigInt(ExcelItmeType.speedCut)}";
            str.Add(tmpItem);
        }
        if (tmp.GetConfigInt(ExcelItmeType.accSpeed) > 0)
        {
            tmpItem = $"{Utility.GetLanguage(10107)}:{tmp.GetConfigInt(ExcelItmeType.accSpeed)}";
            str.Add(tmpItem);
        }
        if (tmp.GetConfigInt(ExcelItmeType.attackSpeed) > 0)
        {
            tmpItem = $"{Utility.GetLanguage(10097)}:{tmp.GetConfigInt(ExcelItmeType.attackSpeed)}";
            str.Add(tmpItem);
        }
        if (tmp.GetConfigInt(ExcelItmeType.speed) > 0)
        {
            tmpItem = $"{Utility.GetLanguage(10105)}:{tmp.GetConfigInt(ExcelItmeType.speed)}";
            str.Add(tmpItem);
        }
        if (tmp.GetConfigInt(ExcelItmeType.repel) > 0)
        {
            tmpItem = $"{Utility.GetLanguage(10101)}:{tmp.GetConfigInt(ExcelItmeType.repel)}";
            str.Add(tmpItem);
        }
        if (tmp.GetConfigInt(ExcelItmeType.crit) > 0)
        {
            tmpItem = $"{Utility.GetLanguage(10102)}:{tmp.GetConfigInt(ExcelItmeType.repel)}%";
            str.Add(tmpItem);
        }
        if (tmp.GetConfigInt(ExcelItmeType.defense) > 0)
        {
            tmpItem = $"{Utility.GetLanguage(10103)}:{tmp.GetConfigInt(ExcelItmeType.defense)}";
            str.Add(tmpItem);
        }
        if (tmp.GetConfigIntArray(ExcelItmeType.elementAttack) != null) //元素攻击:风 | 雷 | 冰 | 火 | 草
        {
            int[] elementAttack = tmp.GetConfigIntArray(ExcelItmeType.elementAttack);
            for (int i = 0; i < elementAttack.Length; i++)
            {
                if (elementAttack[i] > 0)
                {
                    if (i == 0)
                    {
                        tmpItem = $"{Utility.GetLanguage(10109)}:{elementAttack[i]}";
                        str.Add(tmpItem);
                    }
                    else if (i == 1)
                    {
                        tmpItem = $"{Utility.GetLanguage(10110)}:{elementAttack[i]}";
                        str.Add(tmpItem);
                    }
                    else if (i == 2)
                    {
                        tmpItem = $"{Utility.GetLanguage(10111)}:{elementAttack[i]}";
                        str.Add(tmpItem);
                    }
                    else if (i == 3)
                    {
                        tmpItem = $"{Utility.GetLanguage(10112)}:{elementAttack[i]}";
                        str.Add(tmpItem);
                    }
                    else if (i == 4)
                    {
                        tmpItem = $"{Utility.GetLanguage(10113)}:{elementAttack[i]}";
                        str.Add(tmpItem);
                    }
                }

            }

        }
        if (tmp.GetConfigIntArray(ExcelItmeType.elementDefence) != null) //元素防御:风 | 雷 | 冰 | 火 | 草
        {
            int[] elementDefence = tmp.GetConfigIntArray(ExcelItmeType.elementDefence);
            for (int i = 0; i < elementDefence.Length; i++)
            {
                if (elementDefence[i] > 0)
                {
                    if (i == 0)
                    {
                        tmpItem = $"{Utility.GetLanguage(10109)}{Utility.GetLanguage(10103)}:{elementDefence[i]}";
                        str.Add(tmpItem);
                    }
                    else if (i == 1)
                    {
                        tmpItem = $"{Utility.GetLanguage(10110)}{Utility.GetLanguage(10103)}:{elementDefence[i]}";
                        str.Add(tmpItem);
                    }
                    else if (i == 2)
                    {
                        tmpItem = $"{Utility.GetLanguage(10111)}{Utility.GetLanguage(10103)}:{elementDefence[i]}";
                        str.Add(tmpItem);
                    }
                    else if (i == 3)
                    {
                        tmpItem = $"{Utility.GetLanguage(10112)}{Utility.GetLanguage(10103)}:{elementDefence[i]}";
                        str.Add(tmpItem);
                    }
                    else if (i == 4)
                    {
                        tmpItem = $"{Utility.GetLanguage(10113)}{Utility.GetLanguage(10103)}:{elementDefence[i]}";
                        str.Add(tmpItem);
                    }
                }

            }

        }
        return str;
    }
}
