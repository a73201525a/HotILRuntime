using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// bag��Ϣ
/// </summary>
public class BagMsg:ILMsgBase 
{
    public List<ItemJson> ItemList;//��Ʒ����
    /// <summary>
    /// ��Ʒ
    /// </summary>
    public ItemJson item;
    /// <summary>
    /// �Ƴ�bag����
    /// </summary>
    /// <param name="item">��Ʒ����</param>
    /// <param name="msgID"></param>
    public BagMsg(ItemJson item,ushort msgID)
    {
        this.msgID = msgID;
        this.item = item;
    }

    /// <summary>
    ///������Ʒ����
    /// </summary>
    /// <param name="ItemList">��������Ʒ����</param>
    /// <param name="msgID"></param>
    public BagMsg(List<ItemJson> ItemList, ushort msgID)
    {
        this.msgID = msgID;
        this.ItemList = ItemList;
    }
}
/// Bagʹ�ü�����
/// </summary>
public class UsePanelMsg : ILMsgBase
{
    public Grid useGrid;//ʹ�õĸ���
    /// <summary>
    /// ����ĸ���
    /// </summary>
    public int enterIndex;
    /// <summary>
    /// ��ǰ����
    /// </summary>
    public int curindex;
    /// <summary>
    /// ����ʹ�ý���
    /// </summary>
    /// <param name="useGrid"></param>
    /// <param name="msgID"></param>
    public UsePanelMsg(Grid useGrid, ushort msgID)
    {
        this.useGrid = useGrid;
        this.msgID = msgID;
    }
    /// <summary>
    ///  ʹ��װ����Ʒ
    /// </summary>
    /// <param name="enterIndex">����װ������</param>
    /// <param name="curindex">���ʹ����Ʒ��������</param>
    /// <param name="msgID"></param>
    public UsePanelMsg(int enterIndex, int curindex, ushort msgID)
    {
        this.enterIndex = enterIndex;
        this.curindex = curindex;
        this.msgID = msgID;
    }
}
public class FightBagMsg : ILMsgBase
{
    //ʰȡ��Ʒ��ID
    public int ItemID;
    //��Ʒ������
    public int ItemCount;
    public ushort CallBack;//�ص�
    public int bulletType;//�ӵ�����
    public int itemType;//��Ʒ����
    public ItemJson item;//������Ʒ
    /// <summary>
    /// ʰȡ��Ʒ
    /// </summary>
    /// <param name="ItemID">��Ʒid</param>
    /// <param name="ItemCount">��Ʒ����</param>
    /// <param name="msgID"></param>
    public FightBagMsg(int ItemID, int ItemCount, ushort msgID)
    {
        this.ItemID = ItemID;
        this.ItemCount = ItemCount;
        this.msgID = msgID;
    } 
    public FightBagMsg(int ItemID,ushort msgID,ushort CallBack)
    {
        this.ItemID = ItemID;
        this.CallBack = CallBack;
        this.msgID = msgID;
    }
    
    /// <summary>
    /// �������ݻص�
    /// </summary>
    /// <param name="CallBack"></param>
    /// <param name="msgID"></param>
    public FightBagMsg(ushort CallBack, ushort msgID)
    {
        this.CallBack = CallBack;
        this.msgID = msgID;
    } 
    /// <summary>
    /// ������Ʒ���ݴ���
    /// </summary>
    /// <param name="item">������Ʒ����</param>
    /// <param name="msgID"></param>
    public FightBagMsg(ItemJson item, ushort msgID)
    {
        this.item = item;
        this.msgID = msgID;
    }
    /// <summary>
    /// �õ�ͬ��������Ʒ
    /// </summary>
    /// <param name="CallBack"></param>
    /// <param name="itemType"></param>
    /// <param name="msgID"></param>
    public FightBagMsg(ushort CallBack,int itemType, ushort msgID)
    {
        this.itemType = itemType;
        this.CallBack = CallBack;
        this.msgID = msgID;
    }
}
public class FightBagMsgBack : ILMsgBase
{
    public List<ItemJson> items;//��ȡ��Ʒ��������
    public ItemJson itemJson;//��ȡ������Ʒ��������
    /// <summary>
    /// ��ȡItem��������
    /// </summary>
    /// <param name="items">item����</param>
    /// <param name="msgID">��Ϣid</param>
    public FightBagMsgBack(List<ItemJson> items, ushort msgID)
    {
        this.items = items;
        this.msgID = msgID;
    }
    /// <summary>
    /// ��ȡ������������
    /// </summary>
    /// <param name="itemJson"></param>
    /// <param name="msgID"></param>
    public FightBagMsgBack(ItemJson itemJson, ushort msgID)
    {
        this.itemJson = itemJson;
        this.msgID = msgID;
    }
}
