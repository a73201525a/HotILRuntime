using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class NetMsgBase : ILMsgBase
{
    public byte[] buffer;
    public byte[] body;
    public NetMsgBase(byte[] arr)
    {
        buffer = arr;//全部包体，包含头部
        this.msgID = BitConverter.ToUInt16(arr, 4);//网络通信ID
    }
    /// <summary>
    /// 获取消息体
    /// </summary> 
    /// <returns></returns>
    public byte[] GetBody()
    {
        return ProtoBufTool.Deserialize(buffer);
        //Buffer.BlockCopy(buffer, 6, this.body, 0, buffer.Length - 6);
        //return this.body;
    }

    public virtual byte[] GetNetBytes()
    {
        return buffer;
    }
}
