/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class ItemCfgTable : TableBase<ItemCfgTable>
{
    internal readonly Dictionary<int, ItemCfgTable> data = new Dictionary<int, ItemCfgTable>();
    /// <summary>
    /// 物品Id
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 格子储存最大数量
    /// </summary>
    internal int maxLimit { get; private set; }
    /// <summary>
    /// 图标
    /// </summary>
    internal string icon_name { get; private set; }
    /// <summary>
    /// 物品大图路径
    /// </summary>
    internal string big_icon { get; private set; }
    /// <summary>
    /// 回复值
    /// </summary>
    internal int repair_hp { get; private set; }
    /// <summary>
    /// 物品描述
    /// </summary>
    internal int description { get; private set; }
    /// <summary>
    /// 物品类型
    /// </summary>
    internal int item_type { get; private set; }
    /// <summary>
    /// 物品优先级
    /// </summary>
    internal int item_priority { get; private set; }
    /// <summary>
    /// 名称
    /// </summary>
    internal int name { get; private set; }
    /// <summary>
    /// 物品稀有度
    /// </summary>
    internal int item_rarity { get; private set; }
    /// <summary>
    /// 物品名称
    /// </summary>
    internal string item_name { get; private set; }
    /// <summary>
    /// 能否采集
    /// </summary>
    internal bool is_digging { get; private set; }
    /// <summary>
    /// 能否叠加
    /// </summary>
    internal bool is_pile { get; private set; }
    /// <summary>
    /// 物品能否放置口袋
    /// </summary>
    internal bool is_bagProp { get; private set; }
    /// <summary>
    /// 该物品能否穿戴
    /// </summary>
    internal bool is_equip { get; private set; }
    /// <summary>
    /// 道具使用效果
    /// </summary>
    internal int skillID { get; private set; }
    /// <summary>
    /// 升级经验
    /// </summary>
    internal int level { get; private set; }

    internal IEnumerable<ItemCfgTable> GetList(Func<ItemCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal ItemCfgTable Get(Func<ItemCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal ItemCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.maxLimit = bReader.ReadInt32();
        this.icon_name = bReader.ReadString();
        this.big_icon = bReader.ReadString();
        this.repair_hp = bReader.ReadInt32();
        this.description = bReader.ReadInt32();
        this.item_type = bReader.ReadInt32();
        this.item_priority = bReader.ReadInt32();
        this.name = bReader.ReadInt32();
        this.item_rarity = bReader.ReadInt32();
        this.item_name = bReader.ReadString();
        this.is_digging = bReader.ReadBoolean();
        this.is_pile = bReader.ReadBoolean();
        this.is_bagProp = bReader.ReadBoolean();
        this.is_equip = bReader.ReadBoolean();
        this.skillID = bReader.ReadInt32();
        this.level = bReader.ReadInt32();
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
