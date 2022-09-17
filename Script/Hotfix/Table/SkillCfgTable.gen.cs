/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class SkillCfgTable : TableBase<SkillCfgTable>
{
    internal readonly Dictionary<int, SkillCfgTable> data = new Dictionary<int, SkillCfgTable>();
    /// <summary>
    /// 技能效果id
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 名称
    /// </summary>
    internal int name { get; private set; }
    /// <summary>
    /// 消耗蓝量
    /// </summary>
    internal int mp { get; private set; }
    /// <summary>
    /// 伤害
    /// </summary>
    internal int[] damage { get; private set; }
    /// <summary>
    /// 技能CD
    /// </summary>
    internal int cdTime { get; private set; }
    /// <summary>
    /// 技能持续时间
    /// </summary>
    internal int[] durationTime { get; private set; }
    /// <summary>
    /// buff效果ID
    /// </summary>
    internal int buffID { get; private set; }
    /// <summary>
    /// 是否是Buff
    /// </summary>
    internal bool isBuff { get; private set; }
    /// <summary>
    /// 技能释放距离
    /// </summary>
    internal int skill_distance { get; private set; }
    /// <summary>
    /// 回复量
    /// </summary>
    internal int[] revert { get; private set; }
    /// <summary>
    /// 技能效果
    /// </summary>
    internal int skilleffect { get; private set; }
    /// <summary>
    /// 资源路径
    /// </summary>
    internal string res_path { get; private set; }
    /// <summary>
    /// 技能图标
    /// </summary>
    internal string skill_icon { get; private set; }
    /// <summary>
    /// 技能描述
    /// </summary>
    internal string skill_des { get; private set; }
    /// <summary>
    /// 技能升级
    /// </summary>
    internal int[] skillLevelExp { get; private set; }
    /// <summary>
    /// 技能升级所需材料
    /// </summary>
    internal int[] skillItemID { get; private set; }
    /// <summary>
    /// 技能效果
    /// </summary>
    internal int[] skillItemType { get; private set; }

    internal IEnumerable<SkillCfgTable> GetList(Func<SkillCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal SkillCfgTable Get(Func<SkillCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal SkillCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.name = bReader.ReadInt32();
        this.mp = bReader.ReadInt32();
        this.damage = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.damage.Length; i++)
        {
            this.damage[i] = bReader.ReadInt32();
        }
        this.cdTime = bReader.ReadInt32();
        this.durationTime = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.durationTime.Length; i++)
        {
            this.durationTime[i] = bReader.ReadInt32();
        }
        this.buffID = bReader.ReadInt32();
        this.isBuff = bReader.ReadBoolean();
        this.skill_distance = bReader.ReadInt32();
        this.revert = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.revert.Length; i++)
        {
            this.revert[i] = bReader.ReadInt32();
        }
        this.skilleffect = bReader.ReadInt32();
        this.res_path = bReader.ReadString();
        this.skill_icon = bReader.ReadString();
        this.skill_des = bReader.ReadString();
        this.skillLevelExp = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.skillLevelExp.Length; i++)
        {
            this.skillLevelExp[i] = bReader.ReadInt32();
        }
        this.skillItemID = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.skillItemID.Length; i++)
        {
            this.skillItemID[i] = bReader.ReadInt32();
        }
        this.skillItemType = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.skillItemType.Length; i++)
        {
            this.skillItemType[i] = bReader.ReadInt32();
        }
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
