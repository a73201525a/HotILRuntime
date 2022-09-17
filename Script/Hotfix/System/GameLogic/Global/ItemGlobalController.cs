using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using ProtoMsg;
using UnityEngine.UI;
using System.IO;
using System;


/// <summary>
/// 物品类型
/// </summary>
public enum RewardType
{
    wood = 10002,//木材
    stone = 10003,//石材
    gold = 10005,//金币
    exp = 10006,//经验
}

public enum ChangeItem
{
    add,
    change,
    changeByItem,
    remove,
}
/// <summary>
///改枪
/// </summary>
public enum ChangeGun
{
    add,
    change,
    remove,
}


public class ItemGlobalController : ILUIBase
{
    private string itemPath = Path.Combine(Application.persistentDataPath, "Item.txt");

    private string playerPath = Path.Combine(Application.persistentDataPath, "Player.txt");

    private string SetingPath = Path.Combine(Application.persistentDataPath, "Seting.txt");

    // private string gunPath = Path.Combine(Application.persistentDataPath, "Gunjson.txt");
    public override void ProcessEvent(ILMsgBase tmpMsg)
    {
        //监听事件
        switch (tmpMsg.msgID)
        {
            case (ushort)ILUIEvent.SaveItemMap:
                {
                    ItemMsg itemMsg = (ItemMsg)tmpMsg;
                    Save(itemMsg.SavePath);
                }
                break;
            case (ushort)ILUIEvent.LoadItemMap:
                {
                    ItemMsg itemMsg = (ItemMsg)tmpMsg;
                    Load(itemMsg.SavePath);
                }
                break;
            case (ushort)ILUIEvent.AddItem:
                {
                    ItemMsg itemMsg = (ItemMsg)tmpMsg;
                    AddItem(itemMsg.itemList);
                }
                break;
            case (ushort)ILUIEvent.UpdateItemAll://更新所有物品数据
                {
                    ItemMsg itemMsg = (ItemMsg)tmpMsg;
                    UpdateItem(itemMsg);

                }
                break;
            case (ushort)ILUIEvent.GetItemAll://得到所有物品数据
                {
                    ItemMsg msg = (ItemMsg)tmpMsg;
                    ItemMsgBack tmp = new ItemMsgBack(GetItemAll(), msg.CallBack);
                    SendMsg(tmp);

                }
                break;
            case (ushort)ILUIEvent.GetItemByType://根据物品类型获取物品
                {
                    ItemMsg msg = (ItemMsg)tmpMsg;
                    ItemMsgBack tmp = new ItemMsgBack(GetItemByType(msg.Type), msg.CallBack);
                    SendMsg(tmp);
                }
                break;
            case (ushort)ILUIEvent.GetPlayerData://得到玩家数据
                {
                    PlayerMsg msg = (PlayerMsg)tmpMsg;

                    PlayerMsgBack tmp = new PlayerMsgBack(msg.CallBack, GetPlayer(msg.playerID));

                    SendMsg(tmp);
                }
                break;
            case (ushort)ILUIEvent.GetAllPlayerJson://得到所有玩家数据
                {
                    PlayerMsg msg = (PlayerMsg)tmpMsg;
                    PlayerMsgBack tmp = new PlayerMsgBack(msg.CallBack, allPlayerJson);

                    SendMsg(tmp);
                }
                break;
            case (ushort)ILUIEvent.UpdatePlayerData://更新玩家数据
                {
                    PlayerMsg playerJsonDataMsg = (PlayerMsg)tmpMsg;
                    ChangePlayer(playerJsonDataMsg.playerJson);
                }
                break;
            case (ushort)ILUIEvent.AddPlayerJson://添加玩家数据
                {
                    PlayerMsg playerMsg = (PlayerMsg)tmpMsg;
                    PlayerJson playerJson = new PlayerJson();
                    playerJson.ID = playerMsg.playerID;
                    AddPlayerJson(playerJson);
                    AddPlayer(playerJson);
                    SaveItem();
                }
                break;
            case (ushort)ILUIEvent.GetGoldItem://得到金币数据
                {
                    ItemMsg msg = (ItemMsg)tmpMsg;
                    ItemMsgBack msgBack = new ItemMsgBack(allItem[0], msg.CallBack);
                    SendMsg(msgBack);
                }
                break;
            case (ushort)ILUIEvent.GetItemByUniqueID://根据唯一iD获取数据
                {
                    ItemMsg itemMsg = (ItemMsg)tmpMsg;
                    ItemMsgBack tmp = new ItemMsgBack(GetItemByUniqueID(itemMsg.UniqueID), itemMsg.CallBack);
                    SendMsg(tmp);
                }
                break;
            case (ushort)ILUIEvent.GetItemByUniqueIDArray://根据部分唯一id获取物品数据
                {
                    ItemMsg itemMsg = (ItemMsg)tmpMsg;
                    ItemMsgBack tmp = new ItemMsgBack(GetItemByUniqueID(itemMsg.UniqueIDArray), itemMsg.CallBack);
                    SendMsg(tmp);
                }
                break;
            case (ushort)ILUIEvent.SaveItem://保存物品
                {
                    SaveItem();
                }
                break;
            case (ushort)ILUIEvent.UpdateSetting://更新设置数据
                {
                    SettingMsg msg = (SettingMsg)tmpMsg;
                    ChangeSeting(msg.setingJson);
                }
                break;
            case (ushort)ILUIEvent.UpdateVolume://更新音量
                {
                    SettingMsg msg = (SettingMsg)tmpMsg;
                    ChangeSeting(msg.setingJson);
                }
                break;
            case (ushort)ILUIEvent.GetSettingData://获取系统设置数据
                {
                    SettingMsg msg = (SettingMsg)tmpMsg;
                    SettingBackMsg settingBackMsg = new SettingBackMsg(setingJson, msg.CallBack);
                    SendMsg(settingBackMsg);
                }
                break;
        }
    }
    public override void Awake()
    {
        //注册消息
        msgIds = new ushort[]
        {
            (ushort)ILUIEvent.UpdateItemAll,
            (ushort)ILUIEvent.GetItemAll,
            (ushort)ILUIEvent.UpdatePlayerData,
            (ushort)ILUIEvent.GetPlayerData,
            (ushort)ILUIEvent.AddPlayerJson,
            (ushort)ILUIEvent.GetItemByType,
            (ushort)ILUIEvent.GetItemByUniqueIDArray,//得到整个唯一id数组
            (ushort)ILUIEvent.GetItemByUniqueID,
            (ushort)ILUIEvent.GetAllPlayerJson,
            (ushort)ILUIEvent.GetGoldItem,
            (ushort)ILUIEvent.UpdateVolume,
            (ushort)ILUIEvent.GetSettingData,
            (ushort)ILUIEvent.UpdateSetting,
            (ushort)ILUIEvent.AddItem,
            (ushort)ILUIEvent.SaveItem,
            (ushort)ILUIEvent.SaveItemMap,
            (ushort)ILUIEvent.LoadItemMap

        };
        RegistSelf(this, msgIds);

        PlayerBasicCfgTable.Initialize(Utility.TablePath + "PlayerBasicCfgData");//玩家基础属性配置初始化
        //物品配置初始化
        TableBase<ItemCfgTable>.Initialize(Utility.TablePath + "ItemCfgData");
        //穿戴配置初始化
        TableBase<EquipmentCfgTable>.Initialize(Utility.TablePath + "EquipmentCfgData");
        TableBase<EquipmentPartCfgTable>.Initialize(Utility.TablePath + "EquipmentPartCfgData");//枪配件配置
        TableBase<BallCfgTable>.Initialize(Utility.TablePath + "BallCfgData");//球配置
        MonsterCfgTable.Initialize(Utility.TablePath + "MonsterCfgData");//怪物配置
    

        SettingCfgTable.Initialize(Utility.TablePath + "SettingCfgData");//系統配置
        RewardItemCfgTable.Initialize(Utility.TablePath + "RewardItemCfgData");//奖励
        TableBase<LanguageCfgTable>.Initialize(Utility.TablePath + "LanguageCfgData");//技能配置

        allItem = LoadFile<ItemJson>(itemPath);
        List<PlayerJson> playerTmp = LoadFile<PlayerJson>(playerPath);
        allPlayerJson = playerTmp;

        if (playerTmp.Count == 0)
        {
            ItemJson gold = new ItemJson((int)RewardType.gold, 0, 0, null);//创建必带球
            allItem.Add(gold);

            List<int> allPlayerList = Utility.HotToDynList<int>(PlayerBasicCfgTable.Instance.data.Keys);
            for (int i = 0; i < 2; i++)
            {
                PlayerJson playerJson = new PlayerJson();
                playerJson.ID = allPlayerList[i];
                playerJson.RiskLevels = 0;
                AddPlayerJson(playerJson);
                AddPlayer(playerJson);
            }

            SaveItem();
        }
        List<SetingJson> tmpSet = LoadFile<SetingJson>(SetingPath);
        if (tmpSet.Count == 0)
        {
            Utility.Language = LangeuageType.Chinese;
            setingJson = new SetingJson();
            SaveSeting();
        }
        else
        {
            setingJson = tmpSet[0];
        }


    }

    /// <summary>
    /// 所有物品
    /// </summary>
    private List<ItemJson> allItem;

    private List<PlayerJson> allPlayerJson;

    private SetingJson setingJson;


    /// <summary>
    /// 加入背包数据
    /// </summary>
    void AddItem(List<ItemJson> itemList)
    {
        Debug.Log("==========================itemList===================================" + itemList.Count);

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].ConfigID == (int)RewardType.stone || itemList[i].ConfigID == (int)RewardType.wood)
            {
                continue;
            }

            if (itemList[i].GetConfigBool(ExcelItmeType.is_pile))
            {

                for (int j = 0; j < allItem.Count; j++)
                {

                    if (allItem[j].ConfigID == itemList[i].ConfigID)
                    {
                        int maxNumber = itemList[i].GetConfigInt(ExcelItmeType.MaxLimit);

                        if (allItem[j].Number == maxNumber)
                        {
                            continue;
                        }
                        int residue = maxNumber - allItem[j].Number;
                        if (itemList[i].Number <= residue)
                        {
                            allItem[j].Number += itemList[i].Number;
                            itemList[i].Number = 0;
                        }
                        else
                        {
                            allItem[j].Number = maxNumber;
                            itemList[i].Number -= residue;
                            i--;
                        }

                        ItemMsg itemMsg3 = new ItemMsg((ushort)ILUIEvent.UpdateItemAll, ChangeItem.change, allItem[j], allItem[j].Index);
                        UpdateItem(itemMsg3);
                        //SendMsg(itemMsg3);
                    }
                }
            }

        }

        List<ItemJson> tmpItme = new List<ItemJson>();
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].ConfigID == (int)RewardType.stone || itemList[i].ConfigID == (int)RewardType.wood)
            {
                continue;
            }

            if (itemList[i].Number > 0)
            {
                //Grid tmp = GetNullGrid(GridType.Storage);
                //if (tmp == null)
                //{
                //    Debug.Log("背包已满,无法添加物品");
                //    return;
                //}
                //itemList[i].Index = tmp.Index;
                // tmpItme.Add(itemList[i]);
                //tmp.ChangeItem(itemList[i], true);


                ItemMsg itemMsg2 = new ItemMsg((ushort)ILUIEvent.UpdateItemAll, ChangeItem.add, itemList[i], -1);
                UpdateItem(itemMsg2);

                //SendMsg(itemMsg2);
            }
        }
        SaveItem();
        //  SendMsg((ushort)ILUIEvent.SaveItem);

    }


    #region 系统设置处理
    /// <summary>
    /// 保存设置
    /// </summary>
    public void SaveSeting()
    {
        List<SetingJson> tmp = new List<SetingJson>();
        tmp.Add(setingJson);

        CreateFile(SetingPath, tmp);
    }
    /// <summary>
    /// 改变设置
    /// </summary>
    /// <param name="tmpJson"></param>
    public void ChangeSeting(SetingJson tmpJson)
    {
        List<SetingJson> tmp = new List<SetingJson>();
        setingJson = tmpJson;

        Utility.Language = (LangeuageType)setingJson.Langeuage;
        tmp.Add(setingJson);
        CreateFile(SetingPath, tmp);
    }

    #endregion


    #region 玩家数据处理

    public int GetPlayeIndexByID(int ID)
    {
        for (int i = 0; i < allPlayerJson.Count; i++)
        {
            if (allPlayerJson[i].ID == ID)
            {
                return i;
            }
        }
        Debug.LogError("未找到次角色" + ID);
        return -1;
    }

    public PlayerJson GetPlayer(int ID)
    {
        int index = GetPlayeIndexByID(ID);
        return allPlayerJson[index];
    }

    public void AddPlayer(PlayerJson Json)
    {
        allPlayerJson.Add(Json);
        CreateFile(playerPath, allPlayerJson);
    }

    public void RemovePlayer(int ID)
    {
        int index = GetPlayeIndexByID(ID);
        allPlayerJson.RemoveAt(index);
        CreateFile(playerPath, allPlayerJson);
    }

    public void ChangePlayer(PlayerJson tmpPlayerJson)
    {
        int index = GetPlayeIndexByID(tmpPlayerJson.ID);
        allPlayerJson[index] = tmpPlayerJson;
        //List<PlayerJson> tmp = new List<PlayerJson>();
        // tmp.Add(playerJson);
        CreateFile(playerPath, allPlayerJson);
    }
    /// <summary>
    /// //添加角色
    ///赋值
    /// </summary>

   
    public void AddPlayerJson(PlayerJson playerJson)
    {

        Debug.Log("playerJson.level:::" + playerJson.level);

        if (playerJson.level >= 0)
        {
            return;
        }
        playerJson.level = 1;
        Debug.Log("playerJson.level::" + playerJson.level);

        PlayerBasicCfgTable playercfg = PlayerBasicCfgTable.Instance.Get(playerJson.ID);
        if (playerJson.modelName == null)
        {
            playerJson.modelName = playercfg.resName;
        }
        playerJson.ID = playercfg.id;
        playerJson.experience = 0;
        playerJson.experienceLevel = playercfg.explevel;
        playerJson.maxLevel = playercfg.maxlevel;
        playerJson.attackSpeed = playercfg.accSpeed;
        playerJson.attack = playercfg.attack;
        playerJson.vertigo = playercfg.vertigo;
        playerJson.speedCut = playercfg.speedCut;
        playerJson.repel = playercfg.repel;
        playerJson.crit = playercfg.crit;
        playerJson.defense = playercfg.defense;
        playerJson.maxHp = playercfg.hp;
        playerJson.curHp = playercfg.hp;
        playerJson.curMp = playercfg.mp;
        playerJson.maxMp = playercfg.mp;
        playerJson.speed = playercfg.speed;
        playerJson.turnSpeed = playercfg.turnSpeed;
        playerJson.accSpeed = playercfg.accSpeed;
        playerJson.sprintSpeed = playercfg.sprintSpeed;
        playerJson.elementAttack = playercfg.elementAttack;
        playerJson.elementDefense = playercfg.elementDefence;
        playerJson.attackRange = playercfg.attackRange;
        playerJson.speedRange = playercfg.speedRange;
        playerJson.equipment = new int[7];
        playerJson.solderType = playercfg.solderType;
        playerJson.roleType = playercfg.roleType;
        playerJson.name = playercfg.name;
        playerJson.quality = playercfg.quality;
        playerJson.bag = new int[5];
        playerJson.ball = new int[5];
        ItemJson ballItem = new ItemJson(playercfg.ballID, 1, 12, null);//创建必带球
        allItem.Add(ballItem);
        playerJson.ball[0] = ballItem.UniqueID;
        playerJson.buildID = playercfg.buildID;
        //=============技能初始======================
        if (playercfg.skillID.Length>0)
        {
            playerJson.skillID = playercfg.skillID;
            playerJson.buildExp = new int[playercfg.skillID.Length];
            playerJson.buildLevel = new int[playercfg.skillID.Length];
            playerJson.skillExp = new int[playercfg.skillID.Length];
            playerJson.skillLevel = new int[playercfg.skillID.Length];
            for (int i = 0; i < playerJson.buildLevel.Length; i++)
            {
                playerJson.buildLevel[i] = 1;
            }
            playerJson.skillLevel[0] = 1;
        }
    }


    #endregion

    #region 物品处理
    /// <summary>
    /// 获取所有物品
    /// </summary>
    /// <returns></returns>
    public List<ItemJson> GetItemAll()
    {
        return allItem;
    }

    /// <summary>
    /// 更新物品数据
    /// </summary>
    /// <param name="itemMsg"></param>
    public void UpdateItem(ItemMsg itemMsg)
    {
        switch (itemMsg.changeItemType)
        {
            case ChangeItem.add:
                {
                    AddItem(itemMsg.item);
                }
                break;
            case ChangeItem.change:
                {
                    ItemChange(itemMsg.index, itemMsg.item);
                }
                break;
            //case ChangeItem.changeByItem:
            //    {
            //        ItemChange(itemMsg.index, itemMsg.item);
            //    }
            //    break;
            case ChangeItem.remove:
                {
                    Remove(itemMsg.UniqueID);
                }
                break;
            default:
                break;
        }
    }

    public void SaveItem()
    {
        CreateFile(itemPath, allItem);
    }

    /// <summary>
    /// 添加物品
    /// </summary>
    /// <param name="tmpItem"></param>
    public void AddItem(ItemJson tmpItem)
    {

        allItem.Add(tmpItem);
    }

    /// <summary>
    /// 修改物品数据索引
    /// </summary>
    /// <param name="index"></param>
    /// <param name="tmpItem"></param>
    public void ItemChange(int index, ItemJson tmpItem)
    {
        for (int i = 0; i < allItem.Count; i++)
        {
            if (allItem[i] == tmpItem)
            {
                allItem[i].Index = index;
                return;
            }
        }
        Debug.LogError("该格子没有物品index：" + index);
    }

    /// <summary>
    /// 移除物品
    /// </summary>
    /// <param name="UniqueID">根据唯一id删除物品</param>
    public void Remove(int UniqueID)
    {
        for (int i = 0; i < allItem.Count; i++)
        {
            if (allItem[i].UniqueID == UniqueID)
            {
                allItem.Remove(allItem[i]);

                return;
            }
        }
    }


    /// <summary>
    /// 获取相应类型物品
    /// </summary>
    /// <param name="type">物品配置类型</param>
    /// <returns></returns>
    public List<ItemJson> GetItemByType(int type)
    {
        List<ItemJson> tmp = new List<ItemJson>();
        for (int i = 0; i < allItem.Count; i++)
        {
            if (allItem[i].GetConfigInt(ExcelItmeType.itemType) == type)
            {
                tmp.Add(allItem[i]);
            }
        }
        return tmp;
    }
    /// <summary>
    /// 根据物品唯一ID获取物品
    /// </summary>
    public ItemJson GetItemByUniqueID(int UniqueID)
    {
        for (int i = 0; i < allItem.Count; i++)
        {
            if (allItem[i].UniqueID == UniqueID)
            {
                return allItem[i];
            }
        }
        Debug.LogError("没有UniqueID::" + UniqueID);
        return null;
    }
    /// <summary>
    /// 根据物品唯一ID获取部分物品
    /// </summary>
    public List<ItemJson> GetItemByUniqueID(int[] UniqueID)
    {
        List<ItemJson> tmp = new List<ItemJson>();
        for (int i = 0; i < allItem.Count; i++)
        {
            for (int j = 0; j < UniqueID.Length; j++)
            {
                if (allItem[i].UniqueID == UniqueID[j])
                {
                    tmp.Add(allItem[i]);
                }
            }
        }
        return tmp;
    }

    #endregion
    void CreateFile<T>(string path, List<T> allItem)
    {
        List<T> jsonObj = allItem;

        string json = LitJson.JsonMapper.ToJson(allItem);


        StreamWriter sw;
        FileInfo t = new FileInfo(path);

        if (!t.Exists)
        {
            sw = t.CreateText();
        }
        else
        {
            t.Delete();

            sw = t.CreateText();
        }
        //FileStream aFile = new FileStream(path, FileMode.OpenOrCreate);
        //StreamWriter sw = new StreamWriter(aFile);

        sw.Write(json);
        sw.Close();
        sw.Dispose();
        Debug.LogFormat("write data：path：{0} , data: {1}", path, json);
    }

    List<T> LoadFile<T>(string path)
    {
        List<T> tmpItem = new List<T>();
        //使用流的形式读取
        StreamReader sr = null;
        string url = path;
        string data = "";
        try
        {
            sr = File.OpenText(url);
            data = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
        }
        catch (Exception e)
        {
            //路径与名称未找到文件则直接返回空
            Debug.Log("path  Null :: " + e);
            CreateFile(path, tmpItem);
        }

        List<T> tmp = LitJson.JsonMapper.ToObject<List<T>>(data);
        if (tmp != null)
        {
            tmpItem = tmp;
        }
        Debug.LogFormat("read data ：path: {0} ，data：{1}", url, data);

        return tmpItem;
    }


    public static void Save(string path)
    {
        File.Copy(Path.Combine(Application.persistentDataPath, "Item.txt"), Path.Combine(path, "Item.txt"), true);
        File.Copy(Path.Combine(Application.persistentDataPath, "Player.txt"), Path.Combine(path, "Player.txt"), true);
        File.Copy(Path.Combine(Application.persistentDataPath, "Seting.txt"), Path.Combine(path, "Seting.txt"), true);
    }

    public void Load(string path)
    {

        File.Delete(Path.Combine(Application.persistentDataPath, "Player.txt"));
        File.Delete(Path.Combine(Application.persistentDataPath, "Item.txt"));
        File.Delete(Path.Combine(Application.persistentDataPath, "Seting.txt"));

        File.Copy(Path.Combine(path, "Item.txt"), Path.Combine(Application.persistentDataPath, "Item.txt"), true);
        File.Copy(Path.Combine(path, "Player.txt"), Path.Combine(Application.persistentDataPath, "Player.txt"), true);
        File.Copy(Path.Combine(path, "Seting.txt"), Path.Combine(Application.persistentDataPath, "Seting.txt"), true);

        allItem = LoadFile<ItemJson>(itemPath);
        List<PlayerJson> playerTmp = LoadFile<PlayerJson>(playerPath);
        allPlayerJson = playerTmp;
        List<SetingJson> tmpSet = LoadFile<SetingJson>(SetingPath);
        setingJson = tmpSet[0];
    }
}
