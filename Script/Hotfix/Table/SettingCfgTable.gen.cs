/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class SettingCfgTable : TableBase<SettingCfgTable>
{
    internal readonly Dictionary<int, SettingCfgTable> data = new Dictionary<int, SettingCfgTable>();
    /// <summary>
    /// 设置ID
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 音量
    /// </summary>
    internal int volume { get; private set; }

    internal IEnumerable<SettingCfgTable> GetList(Func<SettingCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal SettingCfgTable Get(Func<SettingCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal SettingCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.volume = bReader.ReadInt32();
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
