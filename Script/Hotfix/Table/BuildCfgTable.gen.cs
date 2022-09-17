/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class BuildCfgTable : TableBase<BuildCfgTable>
{
    internal readonly Dictionary<int, BuildCfgTable> data = new Dictionary<int, BuildCfgTable>();
    /// <summary>
    /// 物品Id
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 图标
    /// </summary>
    internal string icon_name { get; private set; }
    /// <summary>
    /// 石材消耗
    /// </summary>
    internal int stone { get; private set; }
    /// <summary>
    /// 木材消耗
    /// </summary>
    internal int wood { get; private set; }
    /// <summary>
    /// 物品描述
    /// </summary>
    internal int description { get; private set; }
    /// <summary>
    /// 物品类型
    /// </summary>
    internal int item_type { get; private set; }
    /// <summary>
    /// 物品名称
    /// </summary>
    internal int item_name { get; private set; }
    /// <summary>
    /// 物品路径
    /// </summary>
    internal string item_path { get; private set; }
    /// <summary>
    /// 建筑类型
    /// </summary>
    internal int buildType { get; private set; }
    /// <summary>
    /// 技能id
    /// </summary>
    internal int skillID { get; private set; }
    /// <summary>
    /// 是否增益
    /// </summary>
    internal bool isGain { get; private set; }
    /// <summary>
    /// boss建筑
    /// </summary>
    internal bool isBoss { get; private set; }
    /// <summary>
    /// 能否行动
    /// </summary>
    internal bool isAction { get; private set; }
    /// <summary>
    /// 建造CD
    /// </summary>
    internal int cdTime { get; private set; }
    /// <summary>
    /// 建筑血量
    /// </summary>
    internal int hp { get; private set; }
    /// <summary>
    /// 建筑奖励
    /// </summary>
    internal int[] rewardItemID { get; private set; }
    /// <summary>
    /// 建筑效果范围
    /// </summary>
    internal int range { get; private set; }
    /// <summary>
    /// 建筑升级所需经验
    /// </summary>
    internal int[] buildExp { get; private set; }
    /// <summary>
    /// 建筑升级所需物品id
    /// </summary>
    internal int[] itemID { get; private set; }

    internal IEnumerable<BuildCfgTable> GetList(Func<BuildCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal BuildCfgTable Get(Func<BuildCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal BuildCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.icon_name = bReader.ReadString();
        this.stone = bReader.ReadInt32();
        this.wood = bReader.ReadInt32();
        this.description = bReader.ReadInt32();
        this.item_type = bReader.ReadInt32();
        this.item_name = bReader.ReadInt32();
        this.item_path = bReader.ReadString();
        this.buildType = bReader.ReadInt32();
        this.skillID = bReader.ReadInt32();
        this.isGain = bReader.ReadBoolean();
        this.isBoss = bReader.ReadBoolean();
        this.isAction = bReader.ReadBoolean();
        this.cdTime = bReader.ReadInt32();
        this.hp = bReader.ReadInt32();
        this.rewardItemID = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.rewardItemID.Length; i++)
        {
            this.rewardItemID[i] = bReader.ReadInt32();
        }
        this.range = bReader.ReadInt32();
        this.buildExp = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.buildExp.Length; i++)
        {
            this.buildExp[i] = bReader.ReadInt32();
        }
        this.itemID = new int[(int)bReader.ReadByte()];
        for (int i = 0; i < this.itemID.Length; i++)
        {
            this.itemID[i] = bReader.ReadInt32();
        }
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
