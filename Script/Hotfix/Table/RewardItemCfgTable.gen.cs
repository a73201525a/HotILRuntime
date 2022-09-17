/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class RewardItemCfgTable : TableBase<RewardItemCfgTable>
{
    internal readonly Dictionary<int, RewardItemCfgTable> data = new Dictionary<int, RewardItemCfgTable>();
    /// <summary>
    /// 奖励物品Id
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 物品数量
    /// </summary>
    internal int item_count { get; private set; }
    /// <summary>
    /// 配置表ID
    /// </summary>
    internal int item_id { get; private set; }

    internal IEnumerable<RewardItemCfgTable> GetList(Func<RewardItemCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal RewardItemCfgTable Get(Func<RewardItemCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal RewardItemCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.item_count = bReader.ReadInt32();
        this.item_id = bReader.ReadInt32();
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
