using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipMsg:ILMsgBase
{
    /// <summary>
    /// ��ʾ����
    /// </summary>
    public string TipConnet;
    /// <summary>
    /// ��ʾ����
    /// </summary>
    /// <param name="TipConnet"></param>
    /// <param name="msgID"></param>
    public TipMsg(string TipConnet,ushort msgID)
    {
        this.msgID = msgID;
        this.TipConnet = TipConnet;
    }
}