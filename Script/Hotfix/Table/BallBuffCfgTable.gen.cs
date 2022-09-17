/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class BallBuffCfgTable : TableBase<BallBuffCfgTable>
{
    internal readonly Dictionary<int, BallBuffCfgTable> data = new Dictionary<int, BallBuffCfgTable>();
    /// <summary>
    /// 弹球buff
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 弹球类型
    /// </summary>
    internal int buffType { get; private set; }
    /// <summary>
    /// 弹球攻击
    /// </summary>
    internal int Number { get; private set; }

    internal IEnumerable<BallBuffCfgTable> GetList(Func<BallBuffCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal BallBuffCfgTable Get(Func<BallBuffCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal BallBuffCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.buffType = bReader.ReadInt32();
        this.Number = bReader.ReadInt32();
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
