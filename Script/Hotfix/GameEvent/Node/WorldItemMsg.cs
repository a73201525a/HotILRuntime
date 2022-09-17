using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ʒ��Ϣ
/// </summary>
public class WorldItemMsg : ILMsgBase
{
    public ushort CallBack;//�ص���Ϣ
    public List<ItemJson> ItemList;//��ȡ�����Ʒ����

    /// <summary>
    /// ��Ϣ�ص�
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="CallBack"></param>
    public WorldItemMsg(ushort msgID, ushort CallBack)
    {
        this.msgID = msgID;
        this.CallBack = CallBack;
    }
    /// <summary>
    /// ��ȡ������Ʒ
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="ItemList">���н�����Ʒ</param>
    public WorldItemMsg(ushort msgID, List<ItemJson> ItemList)
    {
        this.msgID = msgID;
        this.ItemList = ItemList;
    }
 
    /// <summary>
    /// �޲���Ϣ
    /// </summary>
    /// <param name="msgID"></param>
    public WorldItemMsg(ushort msgID)
    {
        this.msgID = msgID;
        
    }

}
/// <summary>
/// ������Ʒ�ص���ȡ����
/// </summary>
public class WorldItemBackMsg : ILMsgBase
{


    public int[] ItemArray;//���,ľ��,ʯ������

    public List<ItemJson> allItem;
    /// <summary>
    /// ���绺�������������ȡ
    /// </summary>
    /// <param name="ItemArray">���,ľ��,ʯ������</param>
    /// <param name="msgID"></param>
    public WorldItemBackMsg(int[] ItemArray, ushort msgID)
    {
        this.ItemArray = ItemArray;
        this.msgID = msgID;
    }

    public WorldItemBackMsg(List<ItemJson> allItem, ushort msgID)
    {
        this.allItem = allItem;
        this.msgID = msgID;
    }
}
