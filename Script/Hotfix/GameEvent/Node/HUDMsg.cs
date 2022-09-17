using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDMsg:ILMsgBase
{
    public Transform followTarget;//����Ŀ��
    public Vector3 Pos;//��ʾλ��
    public string  TipConnet;//��ǩ����
    public int curHp;//��ǰѪ��
    public int maxHp;//Ѫ������
    public float Value;//��ǰ����ֵ
    public int ID;//����id����HUD
  //  public int TipTextIndex;//��̬��������
    /// <summary>
    /// ����HUD ���� ��ʾ
    /// </summary>
    /// <param name="followTarget">����Ŀ��</param>
    /// <param name="curHp">��ǰѪ��</param>
    /// <param name="offset">ƫ����</param>
    public HUDMsg(ushort msgID,Transform followTarget, int curHp,int maxHp, int ID)
    {
        this.curHp = curHp;  
        this.maxHp = maxHp;
        this.ID = ID; 
        this.followTarget = followTarget;
        this.msgID = msgID;
    }
    /// <summary>
    /// ��ʾDamage�ı�
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="Pos"></param>
    /// <param name="TipConnet"></param>
    
    public HUDMsg(ushort msgID,Vector3 Pos, string TipConnet)
    {
        this.TipConnet = TipConnet;
        this.Pos = Pos;
        this.msgID = msgID;
    }
  

    /// <summary>
    /// ����Ѫ��
    /// </summary>
    /// <param name="ID">id</param>
    /// <param name="Damage">�˺�ֵ</param>
    /// <param name="percent">�ٷֱ�</param>
    public HUDMsg(ushort msgID,int ID, float Value)
    {
        this.Value = Value;
        this.ID = ID;
        this.msgID = msgID;
    }  
    /// <summary>
    /// ��ʾ��ǩ
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="ID"></param>
    /// <param name="TipConnet"></param>
    public HUDMsg(ushort msgID,int ID, string TipConnet)
    {
        this.TipConnet = TipConnet;
        this.ID = ID;
        this.msgID = msgID;
    } 
    /// <summary>
    /// ��̬����Ʈ��
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="ID"></param>
    /// <param name="TipConnet"></param>
    /// <param name="TipTextIndex"></param>
    //public HUDMsg(ushort msgID,int ID, string TipConnet,int TipTextIndex=0)
    //{
    //    this.TipConnet = TipConnet;
    //    this.ID = ID;
    //    this.msgID = msgID;
    //    this.TipTextIndex = TipTextIndex;
    //}
    /// <summary>
    /// ����Ѫ��
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="ID"></param>
    public HUDMsg(ushort msgID)
    {
        this.msgID = msgID;
    }
    /// <summary>
    /// ���ر�ǩ
    /// </summary>
    /// <param name="msgID"></param>
    public HUDMsg(ushort msgID,int ID)
    {
        this.msgID = msgID;
        this.ID = ID;
    }
}
