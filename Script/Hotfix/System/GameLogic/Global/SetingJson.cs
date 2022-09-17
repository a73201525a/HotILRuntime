using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum SetingType
{

}
/// <summary>
/// 语言类型
/// </summary>
public enum LangeuageType
{

    Chinese,//中文简体
    ChineseTraditional,//中文繁体
    English,//英语
    Japanese,//日语
    Korean,//韩语
    German,//德语

}

public class SetingJson
{
    public SetingJson()
    {
        Langeuage =(int)Utility.Language;
        Autio = 100;
    }


    /// <summary>
    /// 语言
    /// </summary>
    public int Langeuage;

    /// <summary>
    /// 音量
    /// </summary>
    public int Autio;

    /// <summary>
    /// 当前背景索引
    /// </summary>
    public int BackIndex;
    /// <summary>
    /// 当前主界面玩家显示索引
    /// </summary>
    public int CurPlayerIndex;

}
