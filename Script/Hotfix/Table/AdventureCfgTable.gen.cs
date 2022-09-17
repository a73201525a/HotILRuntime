/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class AdventureCfgTable : TableBase<AdventureCfgTable>
{
    internal readonly Dictionary<int, AdventureCfgTable> data = new Dictionary<int, AdventureCfgTable>();
    /// <summary>
    /// 冒险关卡Id
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 胜利条件
    /// </summary>
    internal int adventureWinType { get; private set; }
    /// <summary>
    /// 回合数量
    /// </summary>
    internal int round_num { get; private set; }
    /// <summary>
    /// 冒险奖励
    /// </summary>
    internal int[] reward { get; private set; }
    /// <summary>
    /// 角色ID
    /// </summary>
    internal int[] PlayerID { get; private set; }
    /// <summary>
    /// 关卡怪物
    /// </summary>
    internal string[] MonsterID { get; private set; }
    /// <summary>
    /// 关卡必出征角色
    /// </summary>
    internal int[] PlayerArray { get; private set; }
    /// <summary>
    /// 到达目的地
    /// </summary>
    internal int[] ReachPoint { get; private set; }
    /// <summary>
    /// 关卡建筑ID
    /// </summary>
    internal string[] buildArray { get; private set; }
    /// <summary>
    /// 当前关卡所有用到的球
    /// </summary>
    internal int[] WallBallID { get; private set; }

    internal IEnumerable<AdventureCfgTable> GetList(Func<AdventureCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal AdventureCfgTable Get(Func<AdventureCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal AdventureCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.adventureWinType = bReader.ReadInt32();
        this.round_num = bReader.ReadInt32();
        this.reward = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.reward.Length; i++)
        {
            this.reward[i] = bReader.ReadInt32();
        }
        this.PlayerID = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.PlayerID.Length; i++)
        {
            this.PlayerID[i] = bReader.ReadInt32();
        }
        this.MonsterID = new string[(int)bReader.ReadByte()];
        for (int i = 0; i < this.MonsterID.Length; i++)
        {
            this.MonsterID[i] = bReader.ReadString();
        }
        this.PlayerArray = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.PlayerArray.Length; i++)
        {
            this.PlayerArray[i] = bReader.ReadInt32();
        }
        this.ReachPoint = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.ReachPoint.Length; i++)
        {
            this.ReachPoint[i] = bReader.ReadInt32();
        }
        this.buildArray = new string[(int)bReader.ReadByte()];
        for (int i = 0; i < this.buildArray.Length; i++)
        {
            this.buildArray[i] = bReader.ReadString();
        }
        this.WallBallID = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.WallBallID.Length; i++)
        {
            this.WallBallID[i] = bReader.ReadInt32();
        }
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
