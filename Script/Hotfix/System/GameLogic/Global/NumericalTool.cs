using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using ProtoMsg;
using UnityEngine.UI;






/// <summary>
/// 数值计算
/// </summary>
public class NumericalTool
{

    /// <summary>
    /// 怪物初始唯一标识界限
    /// </summary>
    static int monsterID = 0;

    public static int AddMonster()
    {
       
        monsterID++;
        return monsterID;
    }
    static int mainPlayerID = 0;
    public static int AddMainPlayer()
    {
        mainPlayerID++;
        return mainPlayerID;
    }

    /// <summary>
    /// 添加装备属性
    /// </summary>
    /// <param name="player">player属性</param>
    /// <param name="equipment">装备</param>
    public static void AddEquipmentAttribute(PlayerJson player, ItemJson equipment)
    {
        player.attackSpeed += equipment.GetConfigInt(ExcelItmeType.attackSpeed);
        player.attack += equipment.GetConfigInt(ExcelItmeType.attack);
        player.vertigo += equipment.GetConfigInt(ExcelItmeType.vertigo);
        player.speedCut += equipment.GetConfigInt(ExcelItmeType.speedCut);
        player.repel += equipment.GetConfigInt(ExcelItmeType.repel);
        player.crit += equipment.GetConfigInt(ExcelItmeType.crit);
        player.defense += equipment.GetConfigInt(ExcelItmeType.defense);
        player.maxHp += equipment.GetConfigInt(ExcelItmeType.hp);
        player.speed += equipment.GetConfigInt(ExcelItmeType.speed);
        player.turnSpeed += equipment.GetConfigInt(ExcelItmeType.turnSpeed);
        player.accSpeed += equipment.GetConfigInt(ExcelItmeType.accSpeed);
        player.sprintSpeed += equipment.GetConfigInt(ExcelItmeType.sprintSpeed);

        int[] tmpElementAttack = equipment.GetConfigIntArray(ExcelItmeType.elementAttack);
        for (int i = 0; i < player.elementAttack.Length; i++)
        {
            player.elementAttack[i] += tmpElementAttack[i];
        }
        int[] tmpElementDefense = equipment.GetConfigIntArray(ExcelItmeType.elementDefence);
        for (int i = 0; i < player.elementAttack.Length; i++)
        {
            player.elementDefense[i] += tmpElementDefense[i];
        }
    }
    /// <summary>
    /// 移除装备属性
    /// </summary>
    /// <param name="player">player属性</param>
    /// <param name="equipment">装备</param>
    public static void RemoveEquipmentAttribute(PlayerJson player, ItemJson equipment)
    {

        player.attackSpeed -= equipment.GetConfigInt(ExcelItmeType.attackSpeed);
        player.attack -= equipment.GetConfigInt(ExcelItmeType.attack);
        player.vertigo -= equipment.GetConfigInt(ExcelItmeType.vertigo);
        player.speedCut -= equipment.GetConfigInt(ExcelItmeType.speedCut);
        player.repel -= equipment.GetConfigInt(ExcelItmeType.repel);
        player.crit -= equipment.GetConfigInt(ExcelItmeType.crit);
        player.defense -= equipment.GetConfigInt(ExcelItmeType.defense);
        player.maxHp -= equipment.GetConfigInt(ExcelItmeType.hp);
        player.speed -= equipment.GetConfigInt(ExcelItmeType.speed);
        player.turnSpeed -= equipment.GetConfigInt(ExcelItmeType.turnSpeed);
        player.accSpeed -= equipment.GetConfigInt(ExcelItmeType.accSpeed);
        player.sprintSpeed -= equipment.GetConfigInt(ExcelItmeType.sprintSpeed);

        int[] tmpElementAttack = equipment.GetConfigIntArray(ExcelItmeType.elementAttack);
        for (int i = 0; i < player.elementAttack.Length; i++)
        {
            player.elementAttack[i] -= tmpElementAttack[i];
        }
        int[] tmpElementDefense = equipment.GetConfigIntArray(ExcelItmeType.elementDefence);
        for (int i = 0; i < player.elementAttack.Length; i++)
        {
            player.elementDefense[i] -= tmpElementDefense[i];
        }


    }



    /// <summary>
    /// 伤害计算
    /// </summary>
    /// <param name="attackData"></param>
    /// <param name="defence"></param>
    /// <param name="elementDefence"></param>
    public static int DamageCalculations(AttackData attackData, int defence, int[] elementDefence)
    {

        //        净伤害 = 攻击伤害 - (防御力 * 0.5)
        //+ 属性攻击 - (属性防御力 * 0.85)
        int Rdamage = 0;
        float damage = 0;
        float damage1 = 0;
        float coe = 1;
        float tmpAttack = attackData.Attack * attackData.AddAttack;
        float tmpDefence = defence * attackData.AddAttack;
        if (tmpAttack < tmpDefence) coe = 0.8f;
        else if (tmpAttack > (tmpDefence * 2)) coe = 1.2f;

        damage = Mathf.Clamp(tmpAttack - (tmpDefence * 0.5f) * coe, 1, 99999);

        //for (int i = 0; i < attackData.elementAttack.Length; i++)
        //{
        //    damage1 += Mathf.Clamp(attackData.elementAttack[i] - (elementDefence[i] * 0.85f), 0, 99999);
        //}
        Rdamage = (int)Mathf.Ceil((damage + damage1) * Random.Range(0.9f, 1.1f));

        return Rdamage;
    }



    /// <summary>
    /// DeBuff触发计算
    /// </summary>
    public static GameBuffType BuffCalculations(AttackData attackData)
    {

        int Vertigo = Random.Range(0, 100);

        int DeformationSheep = Random.Range(0, 100);
        int Disarm = Random.Range(0, 100);
        int Frozen = Random.Range(0, 100);



        if (attackData.Vertigo > Vertigo)
        {
            return GameBuffType.Vertigo;
        }
        if (attackData.DeformationSheep > DeformationSheep)
        {
            return GameBuffType.DeformationSheep;
        }
        if (attackData.Disarm > Disarm)
        {
            return GameBuffType.Disarm;
        }
        if (attackData.Frozen > Frozen)
        {
            return GameBuffType.Frozen;
        }

        return GameBuffType.Null;
    }


}
