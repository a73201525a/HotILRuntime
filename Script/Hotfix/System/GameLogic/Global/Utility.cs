using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Utility
{
    /// <summary>
    /// 角色层级
    /// </summary>
    public const int PlayerLayer = 10;
    public const int MonsterLayer = 30;
    public const int enterFightlayer = 23;//进入对战模式
    public const int BuildLayer = 31;//建筑层级
    public static int MaxFightMonster = 3;//单次战斗最大参战怪物
    public static int MaxFightPlayer = 1;//单次战斗最大参战玩家
    public const int OutLineLayer = 25;//高光层级
    public const int DefaultLayer = 0;//默认层级
    public const int GridLayer = 24;//格子层级 
    public const int Ball = 26;//弹球层级 
    public const int WallBall = 27;//弹球层级 
    public const int BallDeath = 28;//弹球层级 

    public const float StandardScreenWidth = 1920;//标准宽
    public const float StandardScreenHeight = 1080;//标准高

   
    //设置数据
    public static LangeuageType Language;//语种类型

    public static SolderType GetSolder(SolderType type)
    {
        switch (type)
        {
            case SolderType.infantry:
                return SolderType.archer;
            case SolderType.cavalry:
                return SolderType.infantry;
            case SolderType.archer:
                return SolderType.cavalry;
            case SolderType.enchanter:
                return SolderType.infantry;
            default:
                Debug.LogError("没有此兵种");
                return SolderType.Null;
        }
    }


    public static string TablePath
    {
        get
        {

            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
            {
                return Application.dataPath + "/Art/TableData/";
            }
            else
            {
                return Application.persistentDataPath + "/Windows/TableData/";
            }

        }
    }


    public static List<T> HotToDynList<T>(this IEnumerable<T> theList)
    {
        return new List<T>(theList);
    }

    public static BulletJson DeserializationBullet(int bulletID)
    {
        //bulletCfgTable bulletCfg = bulletCfgTable.Instance.Get(bulletID);
        //int bullet = bulletCfg.bulletXlData;
        string path = Application.dataPath + "/Art/BulletJson/";
        string temPath = Path.Combine(path, bulletID + ".txt");
        string str = File.ReadAllText(temPath);
        BulletJson bulletJson = LitJson.JsonMapper.ToObject<BulletJson>(str);
        return bulletJson;

    }
    public static string[] StringSplit(char punctuation, string str)
    {
        return str.Split(punctuation);
    }

    public static string GetLanguage(int id)
    {
       
        switch (Language)
        {
            case LangeuageType.Chinese:
                //Debug.Log("ID:::" + id);Debug.Log("Count:::" + LanguageCfgTable.Instance.data.Count);
                //Debug.Log("LanguageCfgTable" + LanguageCfgTable.Instance.Get(id).Chinese);
                return LanguageCfgTable.Instance.Get(id).Chinese;
            case LangeuageType.ChineseTraditional:
                return LanguageCfgTable.Instance.Get(id).ChineseTraditional;

            case LangeuageType.English:
                return LanguageCfgTable.Instance.Get(id).English;

            case LangeuageType.Japanese:
                return LanguageCfgTable.Instance.Get(id).Japanese;

            case LangeuageType.Korean:
                return LanguageCfgTable.Instance.Get(id).Korean;
            case LangeuageType.German:
                return LanguageCfgTable.Instance.Get(id).German;

            default:
                return LanguageCfgTable.Instance.Get(id).English;

        }
    }
}
