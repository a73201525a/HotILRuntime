/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class PlayerLevelCfgTable : TableBase<PlayerLevelCfgTable>
{
    internal readonly Dictionary<int, PlayerLevelCfgTable> data = new Dictionary<int, PlayerLevelCfgTable>();
    /// <summary>
    /// 玩家id
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 玩家起始等级
    /// </summary>
    internal int level { get; private set; }
    /// <summary>
    /// 玩家升级所需经验
    /// </summary>
    internal int exp { get; private set; }
    /// <summary>
    /// 攻击力
    /// </summary>
    internal int attack { get; private set; }
    /// <summary>
    /// 防御
    /// </summary>
    internal int defense { get; private set; }
    /// <summary>
    /// 血量
    /// </summary>
    internal int hp { get; private set; }

    internal IEnumerable<PlayerLevelCfgTable> GetList(Func<PlayerLevelCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal PlayerLevelCfgTable Get(Func<PlayerLevelCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal PlayerLevelCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.level = bReader.ReadInt32();
        this.exp = bReader.ReadInt32();
        this.attack = bReader.ReadInt32();
        this.defense = bReader.ReadInt32();
        this.hp = bReader.ReadInt32();
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
