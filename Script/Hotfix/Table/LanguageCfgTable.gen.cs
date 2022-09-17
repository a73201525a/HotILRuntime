/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class LanguageCfgTable : TableBase<LanguageCfgTable>
{
    internal readonly Dictionary<int, LanguageCfgTable> data = new Dictionary<int, LanguageCfgTable>();
    /// <summary>
    /// 语言ID
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 中文
    /// </summary>
    internal string 
Chinese
 { get; private set; }
    /// <summary>
    /// 繁体中文
    /// </summary>
    internal string ChineseTraditional { get; private set; }
    /// <summary>
    /// 英语
    /// </summary>
    internal string English { get; private set; }
    /// <summary>
    /// 日语
    /// </summary>
    internal string Japanese { get; private set; }
    /// <summary>
    /// 韩语
    /// </summary>
    internal string Korean { get; private set; }
    /// <summary>
    /// 德语
    /// </summary>
    internal string German { get; private set; }

    internal IEnumerable<LanguageCfgTable> GetList(Func<LanguageCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal LanguageCfgTable Get(Func<LanguageCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal LanguageCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.
Chinese
 = bReader.ReadString();
        this.ChineseTraditional = bReader.ReadString();
        this.English = bReader.ReadString();
        this.Japanese = bReader.ReadString();
        this.Korean = bReader.ReadString();
        this.German = bReader.ReadString();
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
