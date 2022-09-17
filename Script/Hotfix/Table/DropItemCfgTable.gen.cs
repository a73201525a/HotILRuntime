/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class DropItemCfgTable : TableBase<DropItemCfgTable>
{
    internal readonly Dictionary<int, DropItemCfgTable> data = new Dictionary<int, DropItemCfgTable>();
    /// <summary>
    /// 掉落物品Id
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 资源路径
    /// </summary>
    internal string Path { get; private set; }
    /// <summary>
    /// 物品数量
    /// </summary>
    internal int item_count { get; private set; }
    /// <summary>
    /// 配置表ID
    /// </summary>
    internal int item_id { get; private set; }
    /// <summary>
    /// 物体层级
    /// </summary>
    internal int item_Layer { get; private set; }

    internal IEnumerable<DropItemCfgTable> GetList(Func<DropItemCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal DropItemCfgTable Get(Func<DropItemCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal DropItemCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.Path = bReader.ReadString();
        this.item_count = bReader.ReadInt32();
        this.item_id = bReader.ReadInt32();
        this.item_Layer = bReader.ReadInt32();
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
