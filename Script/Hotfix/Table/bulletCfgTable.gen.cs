/*
	生成代码,禁止修改
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

internal partial class bulletCfgTable : TableBase<bulletCfgTable>
{
    internal readonly Dictionary<int, bulletCfgTable> data = new Dictionary<int, bulletCfgTable>();
    /// <summary>
    /// 掉落物品Id
    /// </summary>
    internal int id { get; private set; }
    /// <summary>
    /// 资源路径
    /// </summary>
    internal string path { get; private set; }
    /// <summary>
    /// 子弹图标
    /// </summary>
    internal string icon_name { get; private set; }
    /// <summary>
    /// 子弹伤害
    /// </summary>
    internal int damage { get; private set; }
    /// <summary>
    /// 子弹穿透
    /// </summary>
    internal int through { get; private set; }
    /// <summary>
    /// 子弹附带效果
    /// </summary>
    internal int buff { get; private set; }
    /// <summary>
    /// 触发几率
    /// </summary>
    internal int buffChance { get; private set; }
    /// <summary>
    /// 子弹弹道
    /// </summary>
    internal int bulletXlData { get; private set; }
    /// <summary>
    /// 子弹最大堆叠数
    /// </summary>
    internal int maxLimit { get; private set; }
    /// <summary>
    /// 子弹类型
    /// </summary>
    internal int bullet_type { get; private set; }
    /// <summary>
    /// 物品优先级
    /// </summary>
    internal int item_priority { get; private set; }
    /// <summary>
    /// 子弹数量
    /// </summary>
    internal int item_count { get; private set; }
    /// <summary>
    /// 物品类型
    /// </summary>
    internal int item_type { get; private set; }
    /// <summary>
    /// 子弹名字
    /// </summary>
    internal string item_name { get; private set; }
    /// <summary>
    /// 能否叠加
    /// </summary>
    internal bool is_pile { get; private set; }
    /// <summary>
    /// 物品能否放置口袋
    /// </summary>
    internal bool is_pocket { get; private set; }
    /// <summary>
    /// 能否置放到防弹衣
    /// </summary>
    internal bool is_bulletproof { get; private set; }
    /// <summary>
    /// 该物品能否穿戴
    /// </summary>
    internal bool is_equip { get; private set; }
    /// <summary>
    /// 子弹特效
    /// </summary>
    internal string bulletEff { get; private set; }
    /// <summary>
    /// 模型
    /// </summary>
    internal string model { get; private set; }
    /// <summary>
    /// 子弹大图
    /// </summary>
    internal string big_icon { get; private set; }
    /// <summary>
    /// 子弹描述
    /// </summary>
    internal int description { get; private set; }

    internal IEnumerable<bulletCfgTable> GetList(Func<bulletCfgTable, bool> predicate)
    {
        return data.Values.Where(predicate);
    }

    internal bulletCfgTable Get(Func<bulletCfgTable, bool> predicate)
    {
        return data.Values.FirstOrDefault(predicate);
    }

    internal bulletCfgTable Get(int id)
    {
        if (data.ContainsKey(id))
            return data[id];
        return null;
    }

    public override int Parse(BinaryReader bReader)
    {
        this.id = bReader.ReadInt32();
        this.path = bReader.ReadString();
        this.icon_name = bReader.ReadString();
        this.damage = bReader.ReadInt32();
        this.through = bReader.ReadInt32();
        this.buff = bReader.ReadInt32();
        this.buffChance = bReader.ReadInt32();
        this.bulletXlData = bReader.ReadInt32();
        this.maxLimit = bReader.ReadInt32();
        this.bullet_type = bReader.ReadInt32();
        this.item_priority = bReader.ReadInt32();
        this.item_count = bReader.ReadInt32();
        this.item_type = bReader.ReadInt32();
        this.item_name = bReader.ReadString();
        this.is_pile = bReader.ReadBoolean();
        this.is_pocket = bReader.ReadBoolean();
        this.is_bulletproof = bReader.ReadBoolean();
        this.is_equip = bReader.ReadBoolean();
        this.bulletEff = bReader.ReadString();
        this.model = bReader.ReadString();
        this.big_icon = bReader.ReadString();
        this.description = bReader.ReadInt32();
        Instance.data.Add(this.id, this);
        return this.id;
    }
}
