using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ͼ��Ϣ
/// </summary>
public class MapMsg : ILMsgBase
{
    /// <summary>
    /// �ص�
    /// </summary>
    public ushort CallBack;
    public AttackTipType attackArea;//������������
    public int CenterX;//���ĵ�
    public int CenterY;//���ĵ�
    public int MoveToX;//����·��Xֵ
    public int MoveToY;//����·��Yֵ
    public int Range;//����
    public bool isObs;//�ϰ�����
    public int PlayerID;//���id
    /// <summary>
    /// ��ȡ��ͼ����
    /// </summary>
    /// <param name="CenterX">���ĵ�</param>
    /// <param name="CenterY">���ĵ�</param>
    /// <param name="msgID"></param>
    public MapMsg(int CenterX, int CenterY, ushort msgID)
    {
        this.msgID = msgID;
        this.CenterX = CenterX;
        this.CenterY = CenterY;
    }
    /// <summary>
    /// �����ϰ�
    /// </summary>
    /// <param name="CenterX"></param>
    /// <param name="CenterY"></param>
    /// <param name="isObs"></param>
    /// <param name="msgID"></param>
    public MapMsg(int CenterX, int CenterY,bool isObs,ushort msgID)
    {
        this.msgID = msgID;
        this.CenterX = CenterX;
        this.CenterY = CenterY;
        this.isObs = isObs;
    }
    /// <summary>
    /// ������ʾ
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="callback">������Χ</param> 
    /// <param name="Range">������Χ</param>
    /// <param name="CenterX">�����������</param>
    /// <param name="CenterY"></param>
    /// <param name="attackArea">������������</param>
    public MapMsg(ushort msgID,int Range, int CenterX, int CenterY, AttackTipType attackArea, ushort CallBack=0)
    {
        this.msgID = msgID;
        if (CallBack!=0)
        {
            this.CallBack = CallBack;
        }
        this.Range = Range;
        this.CenterX = CenterX;
        this.CenterY = CenterY; 
        this.attackArea = attackArea;
    } 
    /// <summary>
    /// Ĭ�ϻ�ȡ��ʾ
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="Range"></param>
    /// <param name="CenterX"></param>
    /// <param name="CenterY"></param>
    public MapMsg(ushort msgID,int Range, int CenterX, int CenterY)
    {
        this.msgID = msgID;
        this.Range = Range;
        this.CenterX = CenterX;
        this.CenterY = CenterY; 
    }
    /// <summary>
    /// ��ȡ·��
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="CallBack">�ص�</param>
    /// <param name="CenterX">���</param>
    /// <param name="CenterY"></param>
    /// <param name="MoveToX">�յ�</param>
    /// <param name="MoveToY"></param>
    public MapMsg(ushort msgID,ushort CallBack,int CenterX, int CenterY,int MoveToX,int MoveToY)
    {
        this.msgID = msgID;
      
        this.CenterX = CenterX;
        this.CenterY = CenterY; 
        this.MoveToX = MoveToX;
        this.MoveToY = MoveToY;
        this.CallBack = CallBack;
    }  
    /// <summary>
    ///��ȡ����ƶ�·��
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="CallBack"></param>
    /// <param name="PlayerID">���id</param>
    /// <param name="CenterX">��ҵ�ǰ��������</param>
    /// <param name="CenterY"></param>
    /// <param name="MoveToX"></param>
    /// <param name="MoveToY">�ƶ�����������</param>
    public MapMsg(ushort msgID,ushort CallBack,int PlayerID,int CenterX, int CenterY,int MoveToX,int MoveToY)
    {
        this.msgID = msgID;
        this.PlayerID = PlayerID;
        this.CenterX = CenterX;
        this.CenterY = CenterY; 
        this.MoveToX = MoveToX;
        this.MoveToY = MoveToY;
        this.CallBack = CallBack;
    }
    /// <summary>
    /// �޲���Ϣ
    /// </summary>
    /// <param name="msgID"></param>
    public MapMsg(ushort msgID)
    {
        this.msgID = msgID;
    }
    
}
/// <summary>
/// ��ͼ��Ϣ�ص�
/// </summary>
public class MapBackMsg : ILMsgBase
{
    public List<Vector3> PathList;//·�߼���
    public  List<MapHex> HexList;//��ͼ���Ӽ���
    public int MoveToX;//����ĳ��
    public int MoveToY;//����ĳ��
    public bool isMapAttackTipRange;//�Ƿ��ڹ�����ʾ�ĵ�ͼ��Χ��
    /// <summary>
    /// /��ȡ·��·��
    /// </summary>
    /// <param name="PathList">·�߼���</param>
    /// <param name="msgID"></param>
    public MapBackMsg(List<Vector3> PathList, ushort msgID)
    {
        this.msgID = msgID;
        this.PathList = PathList;

    }
    /// <summary>
    /// ·���յ�
    /// </summary>
    /// <param name="MoveToX">�������λ������</param>
    /// <param name="MoveToY">�������λ������</param>
    /// <param name="msgID"></param>
    public MapBackMsg(int MoveToX, int MoveToY, ushort msgID)
    {
        this.msgID = msgID;
        this.MoveToX = MoveToX;
        this.MoveToY = MoveToY;
    }
    /// <summary>
    /// ��ȡ�ҵ����ͼ���Ӽ���
    /// </summary>
    /// <param name="HexList">��ͼ����</param>
    /// <param name="msgID"></param>
    public MapBackMsg(List<MapHex> HexList, ushort msgID)
    {
        this.msgID = msgID;
        this.HexList = HexList;

    }
    /// <summary>
    /// ��ⷶΧ
    /// </summary>
    /// <param name="isMapAttackTipRange">�Ƿ��ڵ�ͼ��ʾ������Χ��</param>
    /// <param name="msgID"></param>
    public MapBackMsg(bool isMapAttackTipRange, ushort msgID)
    {
        this.msgID = msgID;
        this.isMapAttackTipRange = isMapAttackTipRange;

    }
    


}


