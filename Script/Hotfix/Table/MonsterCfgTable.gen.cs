/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class MonsterCfgTable : TableBase<MonsterCfgTable>
{
    internal readonly Dictionary<int, MonsterCfgTable> data = new Dictionary<int, MonsterCfgTable>();
    /// <summary>
    /// 怪物
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 名称
    /// </summary>
    internal string name { get; private set; }
    /// <summary>
    /// 怪物图标
    /// </summary>
    internal string icon { get; private set; }
    /// <summary>
    /// 等级
    /// </summary>
    internal int level { get; private set; }
    /// <summary>
    /// 怪物血量
    /// </summary>
    internal int hp { get; private set; }
    /// <summary>
    /// 怪物防御
    /// </summary>
    internal int defence { get; private set; }
    /// <summary>
    /// 怪物攻击
    /// </summary>
    internal int attack { get; private set; }
    /// <summary>
    /// 怪物攻击距离
    /// </summary>
    internal int attackRange { get; private set; }
    /// <summary>
    /// 品质系数
    /// </summary>
    internal float quality { get; private set; }
    /// <summary>
    /// 怪物增益技能
    /// </summary>
    internal int[] gainSkillID { get; private set; }
    /// <summary>
    /// 怪物减益技能
    /// </summary>
    internal int[] skillID { get; private set; }
    /// <summary>
    /// 怪物类型
    /// </summary>
    internal int monsterType { get; private set; }
    /// <summary>
    /// 描述
    /// </summary>
    internal string description { get; private set; }
    /// <summary>
    /// 巡逻范围
    /// </summary>
    internal int patrolRange { get; private set; }
    /// <summary>
    /// 每回合最大移动距离
    /// </summary>
    internal int speedRange { get; private set; }
    /// <summary>
    /// 怪物类型
    /// </summary>
    internal int aiType { get; private set; }
    /// <summary>
    /// 兵种
    /// </summary>
    internal int solderType { get; private set; }
    /// <summary>
    /// 优先攻击
    /// </summary>
    internal int aiAttackType { get; private set; }
    /// <summary>
    /// 施法范围
    /// </summary>
    internal int castingRange { get; private set; }
    /// <summary>
    /// 攻击提示类型
    /// </summary>
    internal int attackTipType { get; private set; }
    /// <summary>
    /// 爆出奖励物品
    /// </summary>
    internal int[] rewardItemArray { get; private set; }
    /// <summary>
    /// 攻击提示类型
    /// </summary>
    internal int skillLevel { get; private set; }

    internal IEnumerable<MonsterCfgTable> GetList(Func<MonsterCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal MonsterCfgTable Get(Func<MonsterCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal MonsterCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.name = bReader.ReadString();
        this.icon = bReader.ReadString();
        this.level = bReader.ReadInt32();
        this.hp = bReader.ReadInt32();
        this.defence = bReader.ReadInt32();
        this.attack = bReader.ReadInt32();
        this.attackRange = bReader.ReadInt32();
        this.quality = bReader.ReadSingle();
        this.gainSkillID = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.gainSkillID.Length; i++)
        {
            this.gainSkillID[i] = bReader.ReadInt32();
        }
        this.skillID = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.skillID.Length; i++)
        {
            this.skillID[i] = bReader.ReadInt32();
        }
        this.monsterType = bReader.ReadInt32();
        this.description = bReader.ReadString();
        this.patrolRange = bReader.ReadInt32();
        this.speedRange = bReader.ReadInt32();
        this.aiType = bReader.ReadInt32();
        this.solderType = bReader.ReadInt32();
        this.aiAttackType = bReader.ReadInt32();
        this.castingRange = bReader.ReadInt32();
        this.attackTipType = bReader.ReadInt32();
        this.rewardItemArray = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.rewardItemArray.Length; i++)
        {
            this.rewardItemArray[i] = bReader.ReadInt32();
        }
        this.skillLevel = bReader.ReadInt32();
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
