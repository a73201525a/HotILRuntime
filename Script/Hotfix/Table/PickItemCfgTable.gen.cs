/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class PickItemCfgTable : TableBase<PickItemCfgTable>
{
    internal readonly Dictionary<int, PickItemCfgTable> data = new Dictionary<int, PickItemCfgTable>();
    /// <summary>
    /// 采集物品id
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 资源名称
    /// </summary>
    internal string resName { get; private set; }
    /// <summary>
    /// 物品数量
    /// </summary>
    internal int item_count { get; private set; }
    /// <summary>
    /// 对应配置表ID
    /// </summary>
    internal int item_id { get; private set; }

    internal IEnumerable<PickItemCfgTable> GetList(Func<PickItemCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal PickItemCfgTable Get(Func<PickItemCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal PickItemCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.resName = bReader.ReadString();
        this.item_count = bReader.ReadInt32();
        this.item_id = bReader.ReadInt32();
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
