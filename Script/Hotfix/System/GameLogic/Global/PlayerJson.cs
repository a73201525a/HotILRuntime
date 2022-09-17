using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJson
{
    /// <summary>
    /// 玩家ID
    /// </summary>
    public int ID;

    /// <summary>
    /// 玩家模型
    /// </summary>
    public string modelName;

    /// <summary>
    /// 玩家佩戴装备
    /// </summary>
    public int[] equipment;
    /// <summary>
    /// 玩家背包物品
    /// </summary>
    public int[] bag;
    /// <summary>
    /// 玩家携带的球
    /// </summary>
    public int[] ball;
    /// <summary>
    /// 玩家等级
    /// </summary>
    public int level = -1;
    /// <summary>
    /// 玩家最高等级
    /// </summary>
    public int maxLevel;
    /// <summary>
    /// 玩家经验
    /// </summary>
    public int experience;
    /// <summary>
    /// 玩家升级经验id
    /// </summary>
    public int experienceLevel;

    /// <summary>
    /// 玩家当前血量
    /// </summary>
    public int curHp;
    /// <summary>
    /// 玩家名称
    /// </summary>
    public int name;


    public int RiskLevels;//冒险到那一关了


    public int attackRange;//攻击范围
    public int speedRange;//移动距离
    public int attackSpeed;  /// 攻速

    public int attack;  /// 攻击力

    public int vertigo;//眩晕

    public int speedCut;//减速

    public int repel;//击退

    public int crit;  /// 暴击

    public int defense; /// 防御力

    public int maxHp; /// 玩家血量上限

    public int speed;//移速

    public int turnSpeed;//转身速度

    public int accSpeed;//加速度

    public int sprintSpeed;//冲刺速度

    public int[] elementAttack;//元素攻击

    public int[] elementDefense;//元素防御

    public int[] skillID;//技能id
    public int[] skillExp;//技能当前经验
    public int[] skillLevel;//技能当前等级

    public int[] buildID;//建筑id
    public int[] buildExp;//建筑当前经验
    public int[] buildLevel;//建筑当前等级

    public int solderType;//兵种 
    public int quality;//品质

    public int curMp;//当前蓝量
    public int maxMp;//最大蓝量
    public int roleType;//角色类型

}
