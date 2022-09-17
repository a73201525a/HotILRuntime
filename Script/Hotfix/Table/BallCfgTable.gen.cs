/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class BallCfgTable : TableBase<BallCfgTable>
{
    internal readonly Dictionary<int, BallCfgTable> data = new Dictionary<int, BallCfgTable>();
    /// <summary>
    /// 弹球ID
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 弹球类型
    /// </summary>
    internal int ballType { get; private set; }
    /// <summary>
    /// 弹球攻击
    /// </summary>
    internal int Attack { get; private set; }
    /// <summary>
    /// 模型
    /// </summary>
    internal string model { get; private set; }
    /// <summary>
    /// 弹球图标
    /// </summary>
    internal string icon_name { get; private set; }
    /// <summary>
    /// 物品优先级
    /// </summary>
    internal int item_priority { get; private set; }
    /// <summary>
    /// 物品类型
    /// </summary>
    internal int item_type { get; private set; }
    /// <summary>
    /// 该物品是否是球
    /// </summary>
    internal bool is_ball { get; private set; }
    /// <summary>
    /// 能否叠加
    /// </summary>
    internal bool is_pile { get; private set; }
    /// <summary>
    /// 弹球名字
    /// </summary>
    internal int item_name { get; private set; }
    /// <summary>
    /// 弹球道具类型
    /// </summary>
    internal int ballPropType { get; private set; }

    internal IEnumerable<BallCfgTable> GetList(Func<BallCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal BallCfgTable Get(Func<BallCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal BallCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.ballType = bReader.ReadInt32();
        this.Attack = bReader.ReadInt32();
        this.model = bReader.ReadString();
        this.icon_name = bReader.ReadString();
        this.item_priority = bReader.ReadInt32();
        this.item_type = bReader.ReadInt32();
        this.is_ball = bReader.ReadBoolean();
        this.is_pile = bReader.ReadBoolean();
        this.item_name = bReader.ReadInt32();
        this.ballPropType = bReader.ReadInt32();
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
