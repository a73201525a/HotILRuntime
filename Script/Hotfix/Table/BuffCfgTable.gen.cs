/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class BuffCfgTable : TableBase<BuffCfgTable>
{
    internal readonly Dictionary<int, BuffCfgTable> data = new Dictionary<int, BuffCfgTable>();
    /// <summary>
    /// buffID
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 持续时间(秒)
    /// </summary>
    internal int time { get; private set; }
    /// <summary>
    /// 效果名称
    /// </summary>
    internal int name { get; private set; }
    /// <summary>
    /// 持续伤害
    /// </summary>
    internal int[] durDamage { get; private set; }
    /// <summary>
    /// 攻击力
    /// </summary>
    internal int[] attack { get; private set; }
    /// <summary>
    /// 防御力
    /// </summary>
    internal int[] defence { get; private set; }
    /// <summary>
    /// 资源名称
    /// </summary>
    internal string resName { get; private set; }

    internal IEnumerable<BuffCfgTable> GetList(Func<BuffCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal BuffCfgTable Get(Func<BuffCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal BuffCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.time = bReader.ReadInt32();
        this.name = bReader.ReadInt32();
        this.durDamage = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.durDamage.Length; i++)
        {
            this.durDamage[i] = bReader.ReadInt32();
        }
        this.attack = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.attack.Length; i++)
        {
            this.attack[i] = bReader.ReadInt32();
        }
        this.defence = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.defence.Length; i++)
        {
            this.defence[i] = bReader.ReadInt32();
        }
        this.resName = bReader.ReadString();
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
