/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class EquipmentPartCfgTable : TableBase<EquipmentPartCfgTable>
{
    internal readonly Dictionary<int, EquipmentPartCfgTable> data = new Dictionary<int, EquipmentPartCfgTable>();
    /// <summary>
    /// 枪部件物品Id
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 部件大图标
    /// </summary>
    internal string big_icon { get; private set; }
    /// <summary>
    /// 部件图标
    /// </summary>
    internal string icon_name { get; private set; }
    /// <summary>
    /// 部件描述
    /// </summary>
    internal int description { get; private set; }
    /// <summary>
    /// 物品优先级
    /// </summary>
    internal int item_priority { get; private set; }
    /// <summary>
    /// 物品数量
    /// </summary>
    internal int item_count { get; private set; }
    /// <summary>
    /// 强化孔位置
    /// </summary>
    internal int reinforce_Index { get; private set; }
    /// <summary>
    /// 物品名称
    /// </summary>
    internal string item_name { get; private set; }
    /// <summary>
    /// 物品稀有度
    /// </summary>
    internal int item_rarity { get; private set; }
    /// <summary>
    /// 能否叠加
    /// </summary>
    internal bool is_pile { get; private set; }
    /// <summary>
    /// 该物品能否穿戴
    /// </summary>
    internal bool is_equip { get; private set; }
    /// <summary>
    /// 部件归属物品ID
    /// </summary>
    internal int equipment_ID { get; private set; }
    /// <summary>
    /// 名称
    /// </summary>
    internal int name { get; private set; }

    internal IEnumerable<EquipmentPartCfgTable> GetList(Func<EquipmentPartCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal EquipmentPartCfgTable Get(Func<EquipmentPartCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal EquipmentPartCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.big_icon = bReader.ReadString();
        this.icon_name = bReader.ReadString();
        this.description = bReader.ReadInt32();
        this.item_priority = bReader.ReadInt32();
        this.item_count = bReader.ReadInt32();
        this.reinforce_Index = bReader.ReadInt32();
        this.item_name = bReader.ReadString();
        this.item_rarity = bReader.ReadInt32();
        this.is_pile = bReader.ReadBoolean();
        this.is_equip = bReader.ReadBoolean();
        this.equipment_ID = bReader.ReadInt32();
        this.name = bReader.ReadInt32();
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
