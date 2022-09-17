using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipMsg:ILMsgBase
{
    /// <summary>
    /// 提示内容
    /// </summary>
    public string TipConnet;
    /// <summary>
    /// 提示内容
    /// </summary>
    /// <param name="TipConnet"></param>
    /// <param name="msgID"></param>
    public TipMsg(string TipConnet,ushort msgID)
    {
        this.msgID = msgID;
        this.TipConnet = TipConnet;
    }
}