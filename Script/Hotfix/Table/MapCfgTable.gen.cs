/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class MapCfgTable : TableBase<MapCfgTable>
{
    internal readonly Dictionary<int, MapCfgTable> data = new Dictionary<int, MapCfgTable>();
    /// <summary>
    /// 地图Id
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 资源路径
    /// </summary>
    internal string path { get; private set; }
    /// <summary>
    /// 地图样式
    /// </summary>
    internal string[] itsStyles { get; private set; }

    internal IEnumerable<MapCfgTable> GetList(Func<MapCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal MapCfgTable Get(Func<MapCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal MapCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.path = bReader.ReadString();
        this.itsStyles = new string[(int)bReader.ReadByte()];
        for (int i = 0; i < this.itsStyles.Length; i++)
        {
            this.itsStyles[i] = bReader.ReadString();
        }
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
