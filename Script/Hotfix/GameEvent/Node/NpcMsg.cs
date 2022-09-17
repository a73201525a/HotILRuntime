using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCreatMsg : ILMsgBase
{
    public PlayerType type;
    public Vector3 startPonit;//出生点
    public int playerID;//id
    public ushort CallBack;//玩家加载完成回调

    /// <summary>
    /// 创建玩家
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="type"></param>
    /// <param name="startPonit"></param>
    /// <param name="playerID"></param>
    public PlayerCreatMsg(ushort msgid, PlayerType type, Vector3 startPonit, int playerID)
    {
        this.msgID = msgid;
        this.type = type;
        this.startPonit = startPonit;
        this.playerID = playerID;
    }
    /// <summary>
    /// 创建玩家并返回玩家
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="type"></param>
    /// <param name="startPonit"></param>
    /// <param name="playerID"></param>
    public PlayerCreatMsg(ushort msgid, PlayerType type, Vector3 startPonit, int playerID, ushort CallBack)
    {
        this.msgID = msgid;
        this.type = type;
        this.startPonit = startPonit;
        this.playerID = playerID;
        this.CallBack = CallBack;
    }

}

/// <summary>
///玩家战斗数据处理
/// </summary>
public class PlayerDataMsg : ILMsgBase
{
    public int Exp;//经验
    public int playerID;//玩家id
    public int maxRoundNum;//玩家最大回合数
    public Vector2 rechTarget;//到达目标的

    public Player player;//玩家
    public MonsterBase monsterTarget;//玩家反击目标

    public SaveMapData[] saveMapDatas;//存档数据

    public int curRoundNum;//存档读取玩家回合数
    public int roundType;//存档读取回合类型
    /// <summary>
    /// 获取存档数据
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="saveMapDatas">存档数据</param>
    public PlayerDataMsg(ushort msgID, SaveMapData[] saveMapDatas, int curRoundNum,int roundType)
    {
        this.msgID = msgID;
        this.saveMapDatas = saveMapDatas;
        this.roundType = roundType;
        this.curRoundNum = curRoundNum;
    }
    public PlayerDataMsg(ushort msgID)
    {
        this.msgID = msgID;

    }

    /// <summary>
    /// 获取玩家
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="player">玩家</param>
    public PlayerDataMsg(ushort msgID, Player player)
    {
        this.player = player;
        this.msgID = msgID;

    }


    /// <summary>
    /// 玩家反击消息
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="playerID">被攻击玩家</param>
    /// <param name="monsterTarget">反击目标</param>
    public PlayerDataMsg(ushort msgID, int playerID, MonsterBase monsterTarget)
    {
        this.playerID = playerID;
        this.msgID = msgID;
        this.monsterTarget = monsterTarget;
    }
    /// <summary>
    ///玩家经验处理
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="Exp"></param>
    public PlayerDataMsg(ushort msgID, int PlayerID, int Exp)
    {
        this.Exp = Exp;
        this.msgID = msgID;
        this.playerID = PlayerID;
    }

    /// <summary>
    ///获取玩家初始通关条件
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="maxRoundNum"></param> 
    /// <param name="rechTarget">到达目标点</param>
    public PlayerDataMsg(ushort msgID, int maxRoundNum, Vector2 rechTarget)
    {
        this.msgID = msgID;
        this.maxRoundNum = maxRoundNum;
        this.rechTarget = rechTarget;
    }
}
/// <summary>
/// 当前玩家玩家操作消息
/// </summary>
public class PlayerOperationMsg : ILMsgBase
{/// <summary>
/// 操作步骤
/// </summary>
    public PlayerOperationType operationType;
    public bool SkipBall;//跳过弹球战斗

    /// <summary>
    /// 更新玩家操作消息
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="operationType">操作类型</param>
    public PlayerOperationMsg(ushort msgID, PlayerOperationType operationType)
    {
        this.operationType = operationType;
        this.msgID = msgID;
    }
    /// <summary>
    /// 跳过弹球
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="SkipBall">能否跳过弹球战斗</param>
    public PlayerOperationMsg(ushort msgID, bool SkipBall)
    {
        this.SkipBall = SkipBall;
        this.msgID = msgID;
    }
}
public class PlayerMsg : ILMsgBase
{
    public int playerID;//玩家ID
    public UpdatePlayerType updateType;//更新玩家功能状态
    public CameraMode cameraMode;//相机模式
    public ushort CallBack;//回调
    public PlayerJson playerJson;//玩家数据

    public ItemJson tmpItem;//当前玩家装备数据
    public PlayeMoveType autoMoveType;//玩家移动
    public int curindex;//当前装备索引
    public AttackTipType tipType;//提示类型
    public int curMapIndexX;//当前位置X
    public int curMapIndexY;//当前位置Y

    public bool isAllRoundEnd;//是否结束
    public GameObject MonsterObj;//进入战斗怪物对象
    public int SkillID;//技能id 
    public int SkillLevel;//技能等级
    public Vector3 pos;//相机坐标
    /// <summary>
    /// 进入战斗
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="playerID">玩家id</param>
    /// <param name="MonsterObj">怪物对象</param>
    public PlayerMsg(ushort msgid, int playerID, GameObject MonsterObj)
    {
        this.playerID = playerID;
        this.MonsterObj = MonsterObj;
        this.msgID = msgid;
    }
    /// <summary>
    /// 技能获取
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="playerID"></param>
    /// <param name="SkillID">技能id</param> 
    /// <param name="SkillLevel">技能等级</param>
    public PlayerMsg(ushort msgid, int playerID, int SkillID,int SkillLevel)
    {
        this.playerID = playerID;
        this.SkillID = SkillID; 
        this.SkillLevel = SkillLevel;
        this.msgID = msgid;
    }
    /// <summary>
    ///相机跟新位置
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="playerID"></param>
    /// <param name="updateType"></param>
    public PlayerMsg(ushort msgid, int curMapIndexX, int curMapIndexY, UpdatePlayerType updateType)
    {
        this.msgID = msgid;
        this.updateType = updateType;
        this.curMapIndexX = curMapIndexX;
        this.curMapIndexY = curMapIndexY;
    }

    /// <summary>
    ///相机跟新位置
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="playerID"></param>
    /// <param name="updateType"></param>
    public PlayerMsg(ushort msgid, Vector3 pos, UpdatePlayerType updateType)
    {
        this.msgID = msgid;
        this.updateType = updateType;
        this.pos = pos;
    }


    /// <summary>
    /// 切换装备
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="playerID">玩家id</param>
    /// <param name="curindex">当前装备索引</param>
    /// <param name="tmpItem">装备数据</param>
    public PlayerMsg(ushort msgid, int playerID, int curindex, ItemJson tmpItem)
    {
        this.playerID = playerID;
        this.curindex = curindex;
        this.tmpItem = tmpItem;
        this.msgID = msgid;
    }


    /// <summary>
    /// 更新移动
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="playerID"></param>
    /// <param name="autoMoveType"></param>
    /// <param name="updatePlayer"></param>
    public PlayerMsg(ushort msgID, int playerID, PlayeMoveType autoMoveType, UpdatePlayerType updatePlayer)
    {
        this.msgID = msgID;
        this.playerID = playerID;
        this.updateType = updatePlayer;
        this.autoMoveType = autoMoveType;
    }

    /// <summary>
    /// 更新全部状态
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="updatePlayer"></param>
    public PlayerMsg(ushort msgID, UpdatePlayerType updatePlayer)
    {
        this.msgID = msgID;

        this.updateType = updatePlayer;

    }
    /// <summary>
    /// 玩家消息回调
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="callback"></param>
    public PlayerMsg(ushort msgid, ushort callback)
    {
        this.msgID = msgid;
        this.CallBack = callback;
    }
    /// <summary>
    /// 返回哪个玩家消息回调
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="playerID"></param>
    /// <param name="callback"></param>
    public PlayerMsg(ushort msgid, int playerID, ushort callback)
    {
        this.msgID = msgid;
        this.playerID = playerID;
        this.CallBack = callback;
    }
    /// <summary>
    /// 获取玩家属性数据
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="playerJson"></param>
    public PlayerMsg(ushort msgid, PlayerJson playerJson)
    {
        this.msgID = msgid;
        this.playerJson = playerJson;
    }
    /// <summary>
    /// 获取道具物品数据
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="itemJson"></param>
    public PlayerMsg(ushort msgid, ItemJson itemJson)
    {
        this.msgID = msgid;
        this.tmpItem = itemJson;
    }

    /// <summary>
    ///角色ID
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="playerID"></param>
    public PlayerMsg(ushort msgid, int playerID)
    {
        this.msgID = msgid;
        this.playerID = playerID;
    }
    /// <summary>
    /// 根据提示类型显示提示
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="playerID"></param>
    /// <param name="tipType"></param>
    public PlayerMsg(ushort msgid, int playerID, AttackTipType tipType)
    {
        this.msgID = msgid;
        this.playerID = playerID;
        this.tipType = tipType;
    }
    /// <summary>
    /// 无参消息
    /// </summary>
    /// <param name="msgid"></param>
    public PlayerMsg(ushort msgid)
    {
        this.msgID = msgid;
    }
    /// <summary>
    /// 回合结束
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="isAllRoundEnd"></param>
    public PlayerMsg(ushort msgid, bool isAllRoundEnd)
    {
        this.msgID = msgid;
        this.isAllRoundEnd = isAllRoundEnd;
    }

}

public class PlayerMsgBack : ILMsgBase
{
    public Player player;//玩家 
    public MainScenePlayer mainPlayer;//主界面玩家 
    public PlayerJson playerJson;//玩家数据
    public GameObject playerObj;//玩家Obj
    public PlayerData playerData;//当前玩家数据
    public int lastPanelID;//上次页面id
    public bool playerLoadEnd;//玩家是否加载完成
    public List<Player> PlayerList;//玩家list 
    public List<PlayerJson> playerJsonList;//所有玩家数据
    public List<MainScenePlayer> mainsList;//主界面Player
    /// <summary>
    /// 获取主界面玩家List
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="mainsList"></param>
    public PlayerMsgBack(ushort msgid, List<MainScenePlayer> mainsList)
    {
        this.mainsList = mainsList;
        this.msgID = msgid;
    }
    /// <summary>
    /// 获取所有玩家
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="PlayerList"></param>
    public PlayerMsgBack(ushort msgid, List<Player> PlayerList)
    {
        this.msgID = msgid;
        this.PlayerList = PlayerList;

    }
    /// <summary>
    /// 获取玩家数据
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="JsonList"></param>
    public PlayerMsgBack(ushort msgid, PlayerJson playerJson)
    {
        this.msgID = msgid;
        this.playerJson = playerJson;
    }
    /// <summary>
    /// 获取所有玩家数据
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="playerJsonList">所有玩家data</param>
    public PlayerMsgBack(ushort msgid, List<PlayerJson> playerJsonList)
    {
        this.msgID = msgid;
        this.playerJsonList = playerJsonList;
    }
    /// <summary>
    /// 获取缓存数据
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="playerData"></param>
    public PlayerMsgBack(ushort msgid, PlayerData playerData)
    {
        this.msgID = msgid;
        this.playerData = playerData;
    }
    ///// <summary>
    ///// 获取单个玩家数据
    ///// </summary>
    ///// <param name="msgid"></param>
    ///// <param name="playerJson">玩家单个数据</param>
    //public PlayerMsgBack(ushort msgid, PlayerJson playerJson)
    //{
    //    this.msgID = msgid;
    //    this.playerJson = playerJson;
    //}
    /// <summary>
    /// 是否加载完成
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="playerLoadEnd"></param>
    public PlayerMsgBack(ushort msgid, bool playerLoadEnd)
    {
        this.msgID = msgid;
        this.playerLoadEnd = playerLoadEnd;
    }
    /// <summary>
    /// 获取玩家对象
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="playerObj"></param>
    public PlayerMsgBack(ushort msgid, Player player)
    {
        this.msgID = msgid;
        this.player = player;
    }
    /// <summary>
    /// 获取玩家对象
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="playerBase"></param>
    public PlayerMsgBack(ushort msgid, MainScenePlayer mainPlayer)
    {
        this.msgID = msgid;
        this.mainPlayer = mainPlayer;
    }

}

/// <summary>
/// 回合获取
/// </summary>
public class RoundMsg : ILMsgBase
{
    public ushort backMsg;
    public RoundMsg(ushort msgID, ushort backMsg)
    {
        this.backMsg = backMsg;
        this.msgID = msgID;
    }
}

public class RoundMsgBack : ILMsgBase
{
    public int roundNumber;
    public RoundType roundType;

    public RoundMsgBack(ushort msgID, int roundNumber, RoundType roundType)
    {
        this.roundNumber = roundNumber;
        this.roundType = roundType;
        this.msgID = msgID;
    }
}




#region 子弹数据消息处理


public class BulletMsg : ILMsgBase
{

    internal BulletJson data;//子弹数据
    public Vector3 tmpTarget;//目标点
    public Transform targetTransForm;//跟随目标物
    public Vector3 point;//开火点
    public BulletCampType type;//开火正营
    public AttackData attackData;
    public int PlayerID;

    /// <summary>
    /// 发射子弹
    /// </summary>
    /// <param name="msgId">消息头</param>
    /// <param name="data">弹道数据</param>
    /// <param name="tmpTarget">目标位置</param>
    /// <param name="targetTransForm"></param>
    /// <param name="point">开火位置</param>
    /// <param name="type">子弹阵营</param>
    /// <param name="attackData">攻击属性</param>
    public BulletMsg(ushort msgId, BulletJson data, Vector3 tmpTarget, Vector3 point, BulletCampType type, AttackData attackData)
    {
        this.data = data;
        this.tmpTarget = tmpTarget;
        this.point = point;
        this.type = type;
        this.attackData = attackData;
        this.msgID = msgId;
    }

    public GameObject attackObject;//攻击的对象

    /// <summary>
    /// 人物子弹攻击事件
    /// </summary>
    public BulletMsg(ushort msgId, GameObject attackObject, AttackData attackData)
    {
        this.attackData = attackData;
        this.attackObject = attackObject;
        this.msgID = msgId;
    }

    /// <summary>
    /// 怪物子弹攻击事件
    /// </summary>
    public BulletMsg(ushort msgId, int PlayerID, AttackData attackData)
    {
        this.attackData = attackData;
        this.PlayerID = PlayerID;
        this.msgID = msgId;
    }

}

#endregion



