using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMsg : ILMsgBase
{
    public PlayerData playerData;//�������
    public ushort CallBack;//�ص��¼�
    /// <summary>
    /// ��ȡ�������
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="playerData"></param>
    public BallMsg(ushort msgID, PlayerData playerData)
    {
        this.msgID = msgID;
        this.playerData = playerData;
    }
    /// <summary>
    /// ��Ϣ�ص�
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="CallBack"></param>
    public BallMsg(ushort msgID, ushort CallBack)
    {
        this.msgID = msgID;
        this.CallBack = CallBack;
    }
}
