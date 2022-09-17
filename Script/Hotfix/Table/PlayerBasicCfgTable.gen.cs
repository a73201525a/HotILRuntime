/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class PlayerBasicCfgTable : TableBase<PlayerBasicCfgTable>
{
    internal readonly Dictionary<int, PlayerBasicCfgTable> data = new Dictionary<int, PlayerBasicCfgTable>();
    /// <summary>
    /// 玩家id
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 资源名称
    /// </summary>
    internal string resName { get; private set; }
    /// <summary>
    /// 图标名称
    /// </summary>
    internal string iconName { get; private set; }
    /// <summary>
    /// 角色名称
    /// </summary>
    internal int name { get; private set; }
    /// <summary>
    /// 角色类型
    /// </summary>
    internal int roleType { get; private set; }
    /// <summary>
    /// 角色品质
    /// </summary>
    internal int quality { get; private set; }
    /// <summary>
    /// 玩家最高等级
    /// </summary>
    internal int maxlevel { get; private set; }
    /// <summary>
    /// 玩家起始等级
    /// </summary>
    internal int level { get; private set; }
    /// <summary>
    /// 玩家升级所需经验
    /// </summary>
    internal int explevel { get; private set; }
    /// <summary>
    /// 攻速
    /// </summary>
    internal int attackSpeed { get; private set; }
    /// <summary>
    /// 攻击力
    /// </summary>
    internal int attack { get; private set; }
    /// <summary>
    /// 眩晕
    /// </summary>
    internal int vertigo { get; private set; }
    /// <summary>
    /// 减速
    /// </summary>
    internal int speedCut { get; private set; }
    /// <summary>
    /// 击退
    /// </summary>
    internal int repel { get; private set; }
    /// <summary>
    /// 暴击
    /// </summary>
    internal int crit { get; private set; }
    /// <summary>
    /// 防御
    /// </summary>
    internal int defense { get; private set; }
    /// <summary>
    /// 蓝量
    /// </summary>
    internal int mp { get; private set; }
    /// <summary>
    /// 血量
    /// </summary>
    internal int hp { get; private set; }
    /// <summary>
    /// 移速
    /// </summary>
    internal int speed { get; private set; }
    /// <summary>
    /// 转身速度
    /// </summary>
    internal int turnSpeed { get; private set; }
    /// <summary>
    /// 加速度
    /// </summary>
    internal int accSpeed { get; private set; }
    /// <summary>
    /// 冲刺速度
    /// </summary>
    internal int sprintSpeed { get; private set; }
    /// <summary>
    /// 风|雷|冰|火|草
    /// </summary>
    internal int[] elementAttack { get; private set; }
    /// <summary>
    /// 风|雷|冰|火|草
    /// </summary>
    internal int[] elementDefence { get; private set; }
    /// <summary>
    /// 移动范围
    /// </summary>
    internal int speedRange { get; private set; }
    /// <summary>
    /// 攻击范围
    /// </summary>
    internal int attackRange { get; private set; }
    /// <summary>
    /// 建筑id
    /// </summary>
    internal int[] buildID { get; private set; }
    /// <summary>
    /// 技能id
    /// </summary>
    internal int[] skillID { get; private set; }
    /// <summary>
    /// 兵种
    /// </summary>
    internal int solderType { get; private set; }
    /// <summary>
    /// 必出球id
    /// </summary>
    internal int ballID { get; private set; }

    internal IEnumerable<PlayerBasicCfgTable> GetList(Func<PlayerBasicCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal PlayerBasicCfgTable Get(Func<PlayerBasicCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal PlayerBasicCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.resName = bReader.ReadString();
        this.iconName = bReader.ReadString();
        this.name = bReader.ReadInt32();
        this.roleType = bReader.ReadInt32();
        this.quality = bReader.ReadInt32();
        this.maxlevel = bReader.ReadInt32();
        this.level = bReader.ReadInt32();
        this.explevel = bReader.ReadInt32();
        this.attackSpeed = bReader.ReadInt32();
        this.attack = bReader.ReadInt32();
        this.vertigo = bReader.ReadInt32();
        this.speedCut = bReader.ReadInt32();
        this.repel = bReader.ReadInt32();
        this.crit = bReader.ReadInt32();
        this.defense = bReader.ReadInt32();
        this.mp = bReader.ReadInt32();
        this.hp = bReader.ReadInt32();
        this.speed = bReader.ReadInt32();
        this.turnSpeed = bReader.ReadInt32();
        this.accSpeed = bReader.ReadInt32();
        this.sprintSpeed = bReader.ReadInt32();
        this.elementAttack = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.elementAttack.Length; i++)
        {
            this.elementAttack[i] = bReader.ReadInt32();
        }
        this.elementDefence = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.elementDefence.Length; i++)
        {
            this.elementDefence[i] = bReader.ReadInt32();
        }
        this.speedRange = bReader.ReadInt32();
        this.attackRange = bReader.ReadInt32();
        this.buildID = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.buildID.Length; i++)
        {
            this.buildID[i] = bReader.ReadInt32();
        }
        this.skillID = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.skillID.Length; i++)
        {
            this.skillID[i] = bReader.ReadInt32();
        }
        this.solderType = bReader.ReadInt32();
        this.ballID = bReader.ReadInt32();
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
