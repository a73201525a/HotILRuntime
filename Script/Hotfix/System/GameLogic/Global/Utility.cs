using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Utility
{
    /// <summary>
    /// ��ɫ�㼶
    /// </summary>
    public const int PlayerLayer = 10;
    public const int MonsterLayer = 30;
    public const int enterFightlayer = 23;//�����սģʽ
    public const int BuildLayer = 31;//�����㼶
    public static int MaxFightMonster = 3;//����ս������ս����
    public static int MaxFightPlayer = 1;//����ս������ս���
    public const int OutLineLayer = 25;//�߹�㼶
    public const int DefaultLayer = 0;//Ĭ�ϲ㼶
    public const int GridLayer = 24;//���Ӳ㼶 
    public const int Ball = 26;//����㼶 
    public const int WallBall = 27;//����㼶 
    public const int BallDeath = 28;//����㼶 

    public const float StandardScreenWidth = 1920;//��׼��
    public const float StandardScreenHeight = 1080;//��׼��

   
    //��������
    public static LangeuageType Language;//��������

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
                Debug.LogError("û�д˱���");
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
