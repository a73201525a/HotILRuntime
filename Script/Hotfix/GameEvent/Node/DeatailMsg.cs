using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeatailMsg : ILMsgBase
{
    public string connet;//��������
    public MonsterBase monsterData;//��������
    public Player player;//�������
    public BuildItemBase buildItem;//������Ʒ
    public List<Buff> buffList;//buffList
    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="connet"></param>
    public DeatailMsg(ushort msgID, string connet)
    {
        this.msgID = msgID;
        this.connet = connet;
    } 
    /// <summary>
    /// ��ʾ��������
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="monsterData"></param>
    public DeatailMsg(ushort msgID, MonsterBase monsterData)
    {
        this.msgID = msgID;
        this.monsterData = monsterData;
    } 
    /// <summary>
    /// ���������ʾ
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="player"></param>
    public DeatailMsg(ushort msgID, Player player)
    {
        this.msgID = msgID;
        this.player = player;
    } 
    /// <summary>
    /// ������Ʒ����
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="buildItem">������Ʒ</param>
    public DeatailMsg(ushort msgID, BuildItemBase buildItem)
    {
        this.msgID = msgID;
        this.buildItem = buildItem;
    } 
  
    /// <summary>
    /// �Ƴ���������
    /// </summary>
    /// <param name="msgID"></param>
    public DeatailMsg(ushort msgID)
    {
        this.msgID = msgID;
    }
    /// <summary>
    /// ��ʾ����buff����
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="buffList"></param>

    public DeatailMsg(ushort msgID,List<Buff> buffList)
    {
        this.msgID = msgID;
        this.buffList = buffList;
      
    }
}

