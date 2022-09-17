/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class EquipmentCfgTable : TableBase<EquipmentCfgTable>
{
    internal readonly Dictionary<int, EquipmentCfgTable> data = new Dictionary<int, EquipmentCfgTable>();
    /// <summary>
    /// 物品Id
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 物品名字
    /// </summary>
    internal int name { get; private set; }
    /// <summary>
    /// 图标
    /// </summary>
    internal string icon_name { get; private set; }
    /// <summary>
    /// 物品大图
    /// </summary>
    internal string big_icon { get; private set; }
    /// <summary>
    /// 物品描述
    /// </summary>
    internal int description { get; private set; }
    /// <summary>
    /// 物品优先级
    /// </summary>
    internal int item_priority { get; private set; }
    /// <summary>
    /// 武器类型
    /// </summary>
    internal int weapon_type { get; private set; }
    /// <summary>
    /// 召唤数量
    /// </summary>
    internal int summon_count { get; private set; }
    /// <summary>
    /// 物品稀有度
    /// </summary>
    internal int item_rarity { get; private set; }
    /// <summary>
    /// 资源路径
    /// </summary>
    internal string item_name { get; private set; }
    /// <summary>
    /// 物品能否放置口袋
    /// </summary>
    internal bool is_bagProp { get; private set; }
    /// <summary>
    /// 能否叠加
    /// </summary>
    internal bool is_pile { get; private set; }
    /// <summary>
    /// 是否是球
    /// </summary>
    internal bool is_ball { get; private set; }
    /// <summary>
    /// 能否穿戴
    /// </summary>
    internal bool is_equip { get; private set; }
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
    /// 子弹id
    /// </summary>
    internal int[] bulletData { get; private set; }

    internal IEnumerable<EquipmentCfgTable> GetList(Func<EquipmentCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal EquipmentCfgTable Get(Func<EquipmentCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal EquipmentCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.name = bReader.ReadInt32();
        this.icon_name = bReader.ReadString();
        this.big_icon = bReader.ReadString();
        this.description = bReader.ReadInt32();
        this.item_priority = bReader.ReadInt32();
        this.weapon_type = bReader.ReadInt32();
        this.summon_count = bReader.ReadInt32();
        this.item_rarity = bReader.ReadInt32();
        this.item_name = bReader.ReadString();
        this.is_bagProp = bReader.ReadBoolean();
        this.is_pile = bReader.ReadBoolean();
        this.is_ball = bReader.ReadBoolean();
        this.is_equip = bReader.ReadBoolean();
        this.attackSpeed = bReader.ReadInt32();
        this.attack = bReader.ReadInt32();
        this.vertigo = bReader.ReadInt32();
        this.speedCut = bReader.ReadInt32();
        this.repel = bReader.ReadInt32();
        this.crit = bReader.ReadInt32();
        this.defense = bReader.ReadInt32();
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
        this.bulletData = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.bulletData.Length; i++)
        {
            this.bulletData[i] = bReader.ReadInt32();
        }
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
