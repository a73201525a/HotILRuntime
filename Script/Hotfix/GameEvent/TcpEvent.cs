using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using ProtoMsg;

public enum TCPEvent
{
    TcpConnect = ManagerID.NetManager + 1,//连接请求
    TcpSendMsg,//发送消息请求
    TcpRecvMsg,
    MsgTestCs = ProtoMsgType.MsgTestCs,
    MsgTestSc = ProtoMsgType.MsgTestSc,
}

public class NetEvent
{

    
}

public class TCPConnectMsg : ILMsgBase
{
    public string ip;

    public ushort port;

    public TCPConnectMsg(ushort tmpId, string ip, ushort port)
    {
        this.msgID = tmpId;
        this.ip = ip;
        this.port = port;
    }
}

public class TCPMsg : ILMsgBase
{
    public IMessage netMsg;
    public ProtoMsgType portoMsgId;
    public TCPMsg(ushort tmpId, IMessage message, ProtoMsgType tmpPortoMsgId)
    {
        this.msgID = tmpId;
        this.netMsg = message;
        this.portoMsgId = tmpPortoMsgId;
    }
}
