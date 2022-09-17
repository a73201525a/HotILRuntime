using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Buff
{
    /// <summary>
    /// 持续几回合
    /// </summary>
    public int time;
    /// <summary>
    /// 击退距离
    /// </summary>
    public int repelDis;
    /// <summary>
    /// 效果名称
    /// </summary>
    public string name;
    /// <summary>
    /// 资源名称
    /// </summary>
    public string resName;
    /// <summary>
    /// 当前buff类型
    /// </summary>
    public GameBuffType buff;
    /// <summary>
    /// 攻击力
    /// </summary>
    public int attack;
    /// <summary>
    /// 防御力
    /// </summary>
    public int defence;
    /// <summary>
    /// buff索引
    /// </summary>
    public int buffIndex;
    /// <summary>
    /// buff持续掉血伤害
    /// </summary>
    public int durDamage;
    /// <summary>
    /// 当前buff效果
    /// </summary>
    public NPCBase npcBase;

    public int Level;//buff等级


    public int CasterID;//施加buff的ID


    public Buff(GameBuffType buff,int Level, NPCBase npcBase, int CasterID)
    {
        this.buff = buff;
        //读表
        this.npcBase = npcBase;
        this.Level = Level;
        this.CasterID = CasterID;
        this.buffIndex = (int)buff;
        BuffCfgTable buffCfg = BuffCfgTable.Instance.Get((int)buff);
        time = buffCfg.time;
        name = Utility.GetLanguage(buffCfg.name);
        resName = buffCfg.resName;
        //=====================TODO::::根据等级来实现======================
        attack = buffCfg.attack[Level];
        defence = buffCfg.defence[Level];
        durDamage = buffCfg.durDamage[Level];

    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(time);
        writer.Write((int)buff);
        writer.Write(npcBase.NpcID);
        writer.Write(CasterID);
        writer.Write(Level);
    }

    public void Load()
    {

    }
}
