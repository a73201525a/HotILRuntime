using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum SetingType
{

}
/// <summary>
/// ��������
/// </summary>
public enum LangeuageType
{

    Chinese,//���ļ���
    ChineseTraditional,//���ķ���
    English,//Ӣ��
    Japanese,//����
    Korean,//����
    German,//����

}

public class SetingJson
{
    public SetingJson()
    {
        Langeuage =(int)Utility.Language;
        Autio = 100;
    }


    /// <summary>
    /// ����
    /// </summary>
    public int Langeuage;

    /// <summary>
    /// ����
    /// </summary>
    public int Autio;

    /// <summary>
    /// ��ǰ��������
    /// </summary>
    public int BackIndex;
    /// <summary>
    /// ��ǰ�����������ʾ����
    /// </summary>
    public int CurPlayerIndex;

}
